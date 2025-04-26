using System.Shared.BaseModel;

namespace System.Infrastructure.Abstraction
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T, TKey> Repository<T, TKey>()
                             where T : BaseEntity<TKey>
                             where TKey : IEquatable<TKey>;
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}