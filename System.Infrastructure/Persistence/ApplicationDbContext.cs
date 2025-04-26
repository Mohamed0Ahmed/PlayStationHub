using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Domain.Entities;

namespace System.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<HelpRequest> HelpRequests { get; set; }
        public DbSet<CustomerPoints> CustomerPoints { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<PointsSetting> PointsSettings { get; set; }
        public DbSet<Domain.Entities.UserStore> UserStores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // Seed Admin Role
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Owner", NormalizedName = "OWNER" },
                new IdentityRole { Id = "3", Name = "BranchManager", NormalizedName = "BRANCHMANAGER" }
            );

            // Seed Admin User
            var hasher = new PasswordHasher<IdentityUser>();
            var adminUser = new IdentityUser
            {
                Id = "1",
                UserName = "admin@system.com",
                NormalizedUserName = "ADMIN@SYSTEM.COM",
                Email = "admin@system.com",
                NormalizedEmail = "ADMIN@SYSTEM.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin123"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Seed Owner User
            var ownerUser = new IdentityUser
            {
                Id = "2",
                UserName = "owner@system.com",
                NormalizedUserName = "OWNER@SYSTEM.COM",
                Email = "owner@system.com",
                NormalizedEmail = "OWNER@SYSTEM.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Owner123"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            modelBuilder.Entity<IdentityUser>().HasData(adminUser, ownerUser);

            // Seed Admin and Owner Role Assignments
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "1",
                    RoleId = "1"
                },
                new IdentityUserRole<string>
                {
                    UserId = "2",
                    RoleId = "2"
                }
            );

            // Seed a Store
            modelBuilder.Entity<Store>().HasData(
                new Store
                {
                    Id = 1,
                    Name = "Main Store",
                    Address = "123 Main St"
                }
            );

            // Seed a Branch
            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    Id = 1,
                    BranchName = "Branch 1",
                    StoreId = 1
                }
            );

            // Seed UserStore (Link Owner to Store)
            modelBuilder.Entity<Domain.Entities.UserStore>().HasData(
                new Domain.Entities.UserStore
                {
                    Id = 1,
                    UserId = "2", // Owner's Id
                    StoreId = 1   // Store's Id
                }
            );
        }
    }
}