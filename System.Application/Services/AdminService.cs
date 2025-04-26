using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Application.Interfaces;
using System.Domain.Entities;
using System.Infrastructure.Persistence;
using System.Shared.BaseModel;

namespace System.Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminService(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<List<Store>> GetAllStoresAsync()
        {
            return await _context.Stores
                .Where(s => !s.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Store>> GetDeletedStoresAsync()
        {
            return await _context.Stores
                .Where(s => s.IsDeleted)
                .ToListAsync();
        }

        public async Task<Store> CreateStoreAsync(string storeName, string address)
        {
            var store = new Store
            {
                Name = storeName,
                Address = address
            };
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task<Store> UpdateStoreAsync(int id, string storeName, string address)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null || store.IsDeleted) throw new Exception("Store not found.");
            store.Name = storeName;
            store.Address = address;
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task<bool> DeleteStoreAsync(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null || store.IsDeleted) return false;
            store.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestoreStoreAsync(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null || !store.IsDeleted) return false;
            store.IsDeleted = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Store> GetStoreByIdAsync(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) throw new Exception("Store not found.");
            return store;
        }

        public async Task<List<Branch>> GetBranchesByStoreIdAsync(int storeId)
        {
            return await _context.Branches
                .Where(b => b.StoreId == storeId && !b.IsDeleted)
                .ToListAsync();
        }

        public async Task<Branch> CreateBranchAsync(int storeId, string branchName)
        {
            var branch = new Branch
            {
                StoreId = storeId,
                BranchName = branchName
            };
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();
            return branch;
        }

        public async Task<Branch> UpdateBranchAsync(int id, string branchName)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null || branch.IsDeleted) throw new Exception("Branch not found.");
            branch.BranchName = branchName;
            await _context.SaveChangesAsync();
            return branch;
        }

        public async Task<bool> DeleteBranchAsync(int id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null || branch.IsDeleted) return false;
            branch.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Room>> GetRoomsByBranchIdAsync(int branchId)
        {
            return await _context.Rooms
                .Where(r => r.BranchId == branchId && !r.IsDeleted)
                .ToListAsync();
        }

        public async Task<Room> CreateRoomAsync(int branchId, string roomName)
        {
            var room = new Room
            {
                BranchId = branchId,
                RoomName = roomName
            };
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room> UpdateRoomAsync(int id, string roomName)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null || room.IsDeleted) throw new Exception("Room not found.");
            room.RoomName = roomName;
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null || room.IsDeleted) return false;
            room.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Guest>> GetGuestsByRoomIdAsync(int roomId)
        {
            return await _context.Guests
                .Where(g => g.RoomId == roomId && !g.IsDeleted)
                .ToListAsync();
        }

        public async Task<Guest> CreateGuestAsync(int roomId, int storeId, string username, string password)
        {
            var existingGuest = await _context.Guests
                .FirstOrDefaultAsync(g => g.Username == username && g.StoreId == storeId && !g.IsDeleted);
            if (existingGuest != null) throw new Exception("Guest with this username already exists in this store.");

            var guest = new Guest
            {
                Id = Guid.NewGuid().ToString(),
                RoomId = roomId,
                StoreId = storeId,
                Username = username,
                Password = password // In a real app, hash the password
            };
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
            return guest;
        }

        public async Task<bool> DeleteGuestAsync(string id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null || guest.IsDeleted) return false;
            guest.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> GuestLoginAsync(string username, string password, string storeName)
        {
            var store = await _context.Stores
                .FirstOrDefaultAsync(s => s.Name == storeName && !s.IsDeleted);
            if (store == null) return false;

            var guest = await _context.Guests
                .FirstOrDefaultAsync(g => g.Username == username && g.Password == password && g.StoreId == store.Id && !g.IsDeleted);
            if (guest == null) return false;

            var user = new IdentityUser { UserName = username, Email = $"{username}@guest.com" };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) return false;

            await _userManager.AddToRoleAsync(user, "Guest");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return true;
        }

        public async Task<Customer> CreateCustomerAsync(string phoneNumber, int branchId)
        {
            var existingCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber && c.BranchId == branchId && !c.IsDeleted);
            if (existingCustomer != null) return existingCustomer;

            var customer = new Customer
            {
                PhoneNumber = phoneNumber,
                BranchId = branchId
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<List<UserDto>> GetAllMainOwnersAsync()
        {
            var owners = await _userManager.GetUsersInRoleAsync("Owner");
            return owners.Select(o => new UserDto
            {
                Id = o.Id,
                Email = o.Email
            }).ToList();
        }

        public async Task<UserDto> CreateMainOwnerAsync(string email, string password, int storeId)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) throw new Exception("Failed to create owner: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, "Owner");
            var userStore = new UserStore
            {
                UserId = user.Id,
                StoreId = storeId
            };
            _context.UserStores.Add(userStore);
            await _context.SaveChangesAsync();

            return new UserDto { Id = user.Id, Email = user.Email };
        }

        public async Task<UserDto> UpdateMainOwnerAsync(string id, string email)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new Exception("Owner not found.");
            user.Email = email;
            user.UserName = email;
            await _userManager.UpdateAsync(user);
            return new UserDto { Id = user.Id, Email = user.Email };
        }

        public async Task<bool> DeleteMainOwnerAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> AdminLoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin) return false;

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            return result.Succeeded;
        }

        public async Task<bool> OwnerLoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var isOwner = await _userManager.IsInRoleAsync(user, "Owner");
            if (!isOwner) return false;

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}