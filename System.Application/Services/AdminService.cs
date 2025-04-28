using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Application.Interfaces;
using System.Domain.Entities;
using System.Infrastructure.Persistence;
using System.Shared;
using System.Shared.BaseModel;

namespace System.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHubContext<NotificationHub> _hubContext;

        public AdminService(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _hubContext = hubContext;
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
            var existingStore = await _context.Stores
                .FirstOrDefaultAsync(s => s.Name == storeName && !s.IsDeleted);
            if (existingStore != null) throw new Exception("Store already exists.");

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
            if (store == null || store.IsDeleted) throw new Exception("Store not found.");

            // Delete related branches
            var branches = await _context.Branches
                .Where(b => b.StoreId == id)
                .ToListAsync();
            foreach (var branch in branches)
            {
                await DeleteBranchAsync(branch.Id);
            }

            // Delete related user stores
            var userStores = await _context.UserStores
                .Where(us => us.StoreId == id)
                .ToListAsync();
            foreach (var userStore in userStores)
            {
                userStore.IsDeleted = true;
            }

            store.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestoreStoreAsync(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null || !store.IsDeleted) throw new Exception("Store not found or not deleted.");

            // Restore related branches
            var branches = await _context.Branches
                .Where(b => b.StoreId == id && b.IsDeleted)
                .ToListAsync();
            foreach (var branch in branches)
            {
                await RestoreBranchAsync(branch.Id);
            }

            // Restore related user stores
            var userStores = await _context.UserStores
                .Where(us => us.StoreId == id && us.IsDeleted)
                .ToListAsync();
            foreach (var userStore in userStores)
            {
                userStore.IsDeleted = false;
            }

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

        public async Task<List<Branch>> GetDeletedBranchesAsync(int storeId)
        {
            return await _context.Branches
                .Where(b => b.StoreId == storeId && b.IsDeleted)
                .ToListAsync();
        }

        public async Task<Branch> CreateBranchAsync(int storeId, string branchName)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null || store.IsDeleted) throw new Exception("Store not found.");

            var existingBranch = await _context.Branches
                .FirstOrDefaultAsync(b => b.BranchName == branchName && b.StoreId == storeId && !b.IsDeleted);
            if (existingBranch != null) throw new Exception("Branch already exists in this store.");

            var branch = new Branch
            {
                BranchName = branchName,
                StoreId = storeId
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
            if (branch == null || branch.IsDeleted) throw new Exception("Branch not found.");

            // Delete related rooms
            var rooms = await _context.Rooms
                .Where(r => r.BranchId == id)
                .ToListAsync();
            foreach (var room in rooms)
            {
                await DeleteRoomAsync(room.Id);
            }

            branch.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestoreBranchAsync(int id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null || !branch.IsDeleted) throw new Exception("Branch not found or not deleted.");

            // Restore related rooms
            var rooms = await _context.Rooms
                .Where(r => r.BranchId == id && r.IsDeleted)
                .ToListAsync();
            foreach (var room in rooms)
            {
                await RestoreRoomAsync(room.Id);
            }

            branch.IsDeleted = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Room>> GetRoomsByBranchIdAsync(int branchId)
        {
            return await _context.Rooms
                .Include(r => r.Branch)
                .Where(r => r.BranchId == branchId && !r.IsDeleted)
                .Select(r => new Room
                {
                    Id = r.Id,
                    RoomName = r.RoomName,
                    BranchId = r.BranchId,
                    IsDeleted = r.IsDeleted,
                    Branch = new Branch
                    {
                        Id = r.Branch.Id,
                        BranchName = r.Branch.BranchName,
                        StoreId = r.Branch.StoreId
                    }
                })
                .ToListAsync();
        }

        public async Task<List<Room>> GetDeletedRoomsAsync(int branchId)
        {
            return await _context.Rooms
                .Where(r => r.BranchId == branchId && r.IsDeleted)
                .ToListAsync();
        }

        public async Task<Room> CreateRoomAsync(int branchId, string roomName)
        {
            var branch = await _context.Branches.FindAsync(branchId);
            if (branch == null || branch.IsDeleted) throw new Exception("Branch not found.");

            var existingRoom = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomName == roomName && r.BranchId == branchId && !r.IsDeleted);
            if (existingRoom != null) throw new Exception("Room already exists in this branch.");

            var room = new Room
            {
                RoomName = roomName,
                BranchId = branchId
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
            if (room == null || room.IsDeleted) throw new Exception("Room not found.");

            // Delete related guests
            var guests = await _context.Guests
                .Where(g => g.RoomId == id)
                .ToListAsync();
            foreach (var guest in guests)
            {
                await DeleteGuestAsync(guest.Id);
            }

            room.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestoreRoomAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null || !room.IsDeleted) throw new Exception("Room not found or not deleted.");

            // Restore related guests
            var guests = await _context.Guests
                .Where(g => g.RoomId == id && g.IsDeleted)
                .ToListAsync();
            foreach (var guest in guests)
            {
                await RestoreGuestAsync(guest.Id);
            }

            room.IsDeleted = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasGuestAsync(int roomId)
        {
            return await _context.Guests
                .AnyAsync(g => g.RoomId == roomId && !g.IsDeleted);
        }

        public async Task<List<Guest>> GetGuestsByRoomIdAsync(int roomId)
        {
            return await _context.Guests
                .Include(g => g.Room)
                .Where(g => g.RoomId == roomId && !g.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Guest>> GetDeletedGuestsAsync(int branchId)
        {
            return await _context.Guests
                .Include(g => g.Room)
                .Where(g => g.Room.BranchId == branchId && g.IsDeleted)
                .ToListAsync();
        }

        public async Task<Guest> CreateGuestAsync(int roomId, int storeId, string username, string password)
        {
            var room = await _context.Rooms
                .Include(r => r.Branch)
                .FirstOrDefaultAsync(r => r.Id == roomId && !r.IsDeleted);
            if (room == null) throw new Exception("Room not found or has been deleted.");

            var store = await _context.Stores
                .FirstOrDefaultAsync(s => s.Id == storeId && !s.IsDeleted);
            if (store == null) throw new Exception("Store not found or has been deleted.");

            if (room.Branch.StoreId != storeId)
                throw new Exception("The Room's Branch does not belong to the specified Store.");

            var existingGuest = await _context.Guests
                .FirstOrDefaultAsync(g => g.Username == username && g.StoreId == storeId && !g.IsDeleted);
            if (existingGuest != null) throw new Exception("Guest with this username already exists in this store.");

            var hasGuest = await _context.Guests
                .AnyAsync(g => g.RoomId == roomId && !g.IsDeleted);
            if (hasGuest) throw new Exception("This room already has a guest.");

            var hasher = new PasswordHasher<Guest>();
            var guest = new Guest
            {
                Id = Guid.NewGuid().ToString(),
                RoomId = roomId,
                StoreId = storeId,
                Username = username,
                Password = hasher.HashPassword(null, password)
            };
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
            return guest;
        }

        public async Task<bool> DeleteGuestAsync(string id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null || guest.IsDeleted) throw new Exception("Guest not found.");
            guest.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestoreGuestAsync(string id)
        {
            var guest = await _context.Guests
                .Include(g => g.Room)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (guest == null || !guest.IsDeleted) throw new Exception("Guest not found or not deleted.");

            if (guest.Room.IsDeleted) throw new Exception("Cannot restore guest because the associated room is deleted.");

            var hasGuest = await _context.Guests
                .AnyAsync(g => g.RoomId == guest.RoomId && !g.IsDeleted);
            if (hasGuest) throw new Exception("This room already has a guest.");

            guest.IsDeleted = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> GuestLoginAsync(string username, string password, string storeName)
        {
            var store = await _context.Stores
                .FirstOrDefaultAsync(s => s.Name == storeName && !s.IsDeleted);
            if (store == null) throw new Exception("Store not found.");

            var guest = await _context.Guests
                .Include(g => g.Room)
                .FirstOrDefaultAsync(g => g.Username == username && g.StoreId == store.Id && !g.IsDeleted);
            if (guest == null) throw new Exception("Invalid username.");

            var hasher = new PasswordHasher<Guest>();
            var result = hasher.VerifyHashedPassword(null, guest.Password, password);
            if (result != PasswordVerificationResult.Success) throw new Exception("Invalid password.");

            if (!string.IsNullOrEmpty(guest.CurrentSessionId))
            {
                await _hubContext.Clients.Client(guest.CurrentSessionId)
                    .SendAsync("ForceLogout", "This device has been logged out due to a new login.");
            }

            var sessionId = Guid.NewGuid().ToString();
            guest.CurrentSessionId = sessionId;
            await _context.SaveChangesAsync();

            return sessionId;
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
            var userDtos = new List<UserDto>();
            foreach (var owner in owners)
            {
                var userStore = await _context.UserStores.FirstOrDefaultAsync(us => us.UserId == owner.Id);
                if (userStore != null && !userStore.IsDeleted)
                {
                    userDtos.Add(new UserDto
                    {
                        Id = owner.Id,
                        Email = owner.Email
                    });
                }
            }
            return userDtos;
        }

        public async Task<List<UserDto>> GetDeletedOwnersAsync()
        {
            var owners = await _userManager.GetUsersInRoleAsync("Owner");
            var userDtos = new List<UserDto>();
            foreach (var owner in owners)
            {
                var userStore = await _context.UserStores.FirstOrDefaultAsync(us => us.UserId == owner.Id);
                if (userStore != null && userStore.IsDeleted)
                {
                    userDtos.Add(new UserDto
                    {
                        Id = owner.Id,
                        Email = owner.Email
                    });
                }
            }
            return userDtos;
        }

        public async Task<UserDto> CreateMainOwnerAsync(string email, string password, int storeId)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null || store.IsDeleted) throw new Exception("Store not found.");

            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null) throw new Exception("User with this email already exists.");

            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) throw new Exception("Failed to create owner: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, "Owner");

            var userStore = new UserStore
            {
                UserId = user.Id,
                StoreId = storeId,
                IsDeleted = false
            };
            _context.UserStores.Add(userStore);
            await _context.SaveChangesAsync();

            return new UserDto { Id = user.Id, Email = user.Email };
        }

        public async Task<UserDto> UpdateMainOwnerAsync(string id, string email)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new Exception("Owner not found.");

            var userStore = await _context.UserStores.FirstOrDefaultAsync(us => us.UserId == id);
            if (userStore == null || userStore.IsDeleted) throw new Exception("Owner not found or has been deleted.");

            user.Email = email;
            user.UserName = email;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) throw new Exception("Failed to update owner: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            return new UserDto { Id = user.Id, Email = user.Email };
        }

        public async Task<bool> DeleteMainOwnerAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new Exception("Owner not found.");

            var userStore = await _context.UserStores.FirstOrDefaultAsync(us => us.UserId == id);
            if (userStore == null || userStore.IsDeleted) throw new Exception("Owner not found or already deleted.");

            userStore.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RestoreOwnerAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new Exception("Owner not found.");

            var userStore = await _context.UserStores.FirstOrDefaultAsync(us => us.UserId == id);
            if (userStore == null || !userStore.IsDeleted) throw new Exception("Owner not found or not deleted.");

            var store = await _context.Stores.FindAsync(userStore.StoreId);
            if (store == null || store.IsDeleted) throw new Exception("Cannot restore owner because the associated store is deleted.");

            userStore.IsDeleted = false;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<(bool isSuccess, string redirectUrl)> UserLoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return (false, null);

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Admin"))
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (result.Succeeded) return (true, "/Admin/Index");
            }
            else if (roles.Contains("Owner"))
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (result.Succeeded) return (true, "/Owner/Index");
            }
            else if (roles.Contains("Staff"))
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (result.Succeeded) return (true, "/Staff/Index");
            }

            return (false, null);
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

        public async Task LinkOwnerToStoreAsync(string email, int storeId)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new Exception("Owner not found.");

            var isOwner = await _userManager.IsInRoleAsync(user, "Owner");
            if (!isOwner) throw new Exception("User is not an Owner.");

            var store = await _context.Stores.FindAsync(storeId);
            if (store == null || store.IsDeleted) throw new Exception("Store not found.");

            var existingUserStore = await _context.UserStores
                .FirstOrDefaultAsync(us => us.UserId == user.Id);
            if (existingUserStore != null)
            {
                existingUserStore.StoreId = storeId;
            }
            else
            {
                var userStore = new UserStore
                {
                    UserId = user.Id,
                    StoreId = storeId
                };
                _context.UserStores.Add(userStore);
            }

            await _context.SaveChangesAsync();
        }
    }
}