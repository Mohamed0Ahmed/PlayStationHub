﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Infrastructure.Abstraction;
using System.Infrastructure.GenericRepositories;
using System.Infrastructure.Persistence;
using System.Security.Claims;
using System.Shared.BaseModel;

namespace System.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Dictionary<Type, object> _repositories = [];
        private IDbContextTransaction? _transaction;
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IRepository<T, TKey> Repository<T, TKey>() where T : BaseEntity<TKey> where TKey : IEquatable<TKey>
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repo = new Repository<T, TKey>(_context, _httpContextAccessor);
                _repositories[type] = repo;
            }

            return (IRepository<T, TKey>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync()
        {
            var entries = _context.ChangeTracker.Entries<IEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    entry.Entity.CreatedBy = userId;
                }
                entry.Entity.LastModifiedOn = DateTime.UtcNow;
                entry.Entity.LastModifiedBy = userId;
            }

            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null) throw new InvalidOperationException("No transaction started.");
            await _transaction.CommitAsync();
            _transaction.Dispose();
            _transaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null) throw new InvalidOperationException("No transaction started.");
            await _transaction.RollbackAsync();
            _transaction.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}