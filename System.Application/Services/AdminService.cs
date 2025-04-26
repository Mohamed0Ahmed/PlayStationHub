using Microsoft.AspNetCore.Identity;
using System.Application.Interfaces;
using System.Domain.Entities;
using System.Infrastructure.Abstraction;
using System.Shared.BaseModel;

namespace System.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(
            IUnitOfWork unitOfWork,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<Store>> GetAllStoresAsync()
        {
            return await _unitOfWork.Repository<Store, int>().FindAsync(s => !s.IsDeleted);
        }

        public async Task<IEnumerable<Store>> GetDeletedStoresAsync()
        {
            return await _unitOfWork.Repository<Store, int>().FindAsync(s => s.IsDeleted);
        }

        public async Task<Store> CreateStoreAsync(string storeName, string address)
        {
            var store = new Store
            {
                Name = storeName,
                Address = address
            };
            await _unitOfWork.Repository<Store, int>().AddAsync(store);
            await _unitOfWork.SaveChangesAsync();
            return store;
        }

        public async Task<Store> UpdateStoreAsync(int id, string storeName, string address)
        {
            var store = await _unitOfWork.Repository<Store, int>().GetByIdAsync(id);
            if (store == null || store.IsDeleted)
                throw new Exception("Store not found.");

            store.Name = storeName;
            store.Address = address;
            _unitOfWork.Repository<Store, int>().Update(store);
            await _unitOfWork.SaveChangesAsync();
            return store;
        }

        public async Task<bool> DeleteStoreAsync(int id)
        {
            await _unitOfWork.Repository<Store, int>().SoftDeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestoreStoreAsync(int id)
        {
            var store = await _unitOfWork.Repository<Store, int>().GetByIdAsync(id);
            if (store == null || !store.IsDeleted)
                return false;

            store.IsDeleted = false;
            store.DeletedOn = null;
            _unitOfWork.Repository<Store, int>().Update(store);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Store> GetStoreByIdAsync(int storeId)
        {
            return await _unitOfWork.Repository<Store, int>().GetByIdAsync(storeId);
        }

        public async Task<IEnumerable<Branch>> GetAllBranchesAsync()
        {
            return await _unitOfWork.Repository<Branch, int>().FindAsync(b => !b.IsDeleted);
        }

        public async Task<IEnumerable<Branch>> GetBranchesByStoreIdAsync(int storeId)
        {
            return await _unitOfWork.Repository<Branch, int>().FindAsync(b => b.StoreId == storeId && !b.IsDeleted);
        }

        public async Task<Branch> CreateBranchAsync(int storeId, string branchName)
        {
            var store = await _unitOfWork.Repository<Store, int>().GetByIdAsync(storeId);
            if (store == null || store.IsDeleted)
                throw new Exception("Store not found.");

            var branch = new Branch
            {
                StoreId = storeId,
                BranchName = branchName
            };
            await _unitOfWork.Repository<Branch, int>().AddAsync(branch);
            await _unitOfWork.SaveChangesAsync();
            return branch;
        }

        public async Task<Branch> UpdateBranchAsync(int id, string branchName)
        {
            var branch = await _unitOfWork.Repository<Branch, int>().GetByIdAsync(id);
            if (branch == null || branch.IsDeleted)
                throw new Exception("Branch not found.");

            branch.BranchName = branchName;
            _unitOfWork.Repository<Branch, int>().Update(branch);
            await _unitOfWork.SaveChangesAsync();
            return branch;
        }

        public async Task<bool> DeleteBranchAsync(int id)
        {
            await _unitOfWork.Repository<Branch, int>().SoftDeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Branch> GetBranchByIdAsync(int branchId)
        {
            return await _unitOfWork.Repository<Branch, int>().GetByIdAsync(branchId);
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _unitOfWork.Repository<Room, int>().FindAsync(r => !r.IsDeleted);
        }

        public async Task<IEnumerable<Room>> GetRoomsByBranchIdAsync(int branchId)
        {
            return await _unitOfWork.Repository<Room, int>().FindAsync(r => r.BranchId == branchId && !r.IsDeleted);
        }

        public async Task<Room> CreateRoomAsync(int branchId, string roomName)
        {
            var branch = await _unitOfWork.Repository<Branch, int>().GetByIdAsync(branchId);
            if (branch == null || branch.IsDeleted)
                throw new Exception("Branch not found.");

            var room = new Room
            {
                BranchId = branchId,
                RoomName = roomName
            };
            await _unitOfWork.Repository<Room, int>().AddAsync(room);
            await _unitOfWork.SaveChangesAsync();
            return room;
        }

        public async Task<Room> UpdateRoomAsync(int id, string roomName)
        {
            var room = await _unitOfWork.Repository<Room, int>().GetByIdAsync(id);
            if (room == null || room.IsDeleted)
                throw new Exception("Room not found.");

            room.RoomName = roomName;
            _unitOfWork.Repository<Room, int>().Update(room);
            await _unitOfWork.SaveChangesAsync();
            return room;
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            await _unitOfWork.Repository<Room, int>().SoftDeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Room> GetRoomByIdAsync(int roomId)
        {
            return await _unitOfWork.Repository<Room, int>().GetByIdAsync(roomId);
        }

        public async Task<IEnumerable<Guest>> GetGuestsByRoomIdAsync(int roomId)
        {
            return await _unitOfWork.Repository<Guest, string>().FindAsync(g => g.RoomId == roomId && !g.IsDeleted);
        }

        public async Task<Guest> CreateGuestAsync(int roomId)
        {
            var room = await _unitOfWork.Repository<Room, int>().GetByIdAsync(roomId);
            if (room == null || room.IsDeleted)
                throw new Exception("Room not found.");

            var guest = new Guest
            {
                Id = Guid.NewGuid().ToString(),
                RoomId = roomId,
                SessionToken = Guid.NewGuid().ToString()
            };
            await _unitOfWork.Repository<Guest, string>().AddAsync(guest);
            await _unitOfWork.SaveChangesAsync();
            return guest;
        }

        public async Task<bool> DeleteGuestAsync(string id)
        {
            await _unitOfWork.Repository<Guest, string>().SoftDeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UserDto>> GetAllMainOwnersAsync()
        {
            var owners = await _userManager.GetUsersInRoleAsync("Owner");
            return owners.Select(o => new UserDto
            {
                Id = o.Id,
                Email = o.Email,
                IsLocked = o.LockoutEnd != null && o.LockoutEnd > DateTimeOffset.UtcNow
            }).ToList();
        }

        public async Task<UserDto> CreateMainOwnerAsync(string email, string password, int storeId)
        {
            var store = await _unitOfWork.Repository<Store, int>().GetByIdAsync(storeId);
            if (store == null || store.IsDeleted)
                throw new Exception("Store not found.");

            var user = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new Exception("Failed to create owner: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, "Owner");

            var userStore = new UserStore
            {
                UserId = user.Id,
                StoreId = storeId
            };
            await _unitOfWork.Repository<UserStore, int>().AddAsync(userStore);
            await _unitOfWork.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                IsLocked = false
            };
        }

        public async Task<UserDto> UpdateMainOwnerAsync(string id, string email)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new Exception("Owner not found.");

            user.Email = email;
            user.UserName = email;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception("Failed to update owner: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                IsLocked = user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.UtcNow
            };
        }

        public async Task<bool> DeleteMainOwnerAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<int?> GetMainOwnerStoreIdAsync(string ownerId)
        {
            var userStore = await _unitOfWork.Repository<UserStore, int>()
                .GetAsync(us => us.UserId == ownerId);
            return userStore?.StoreId;
        }

        public async Task<bool> AdminLoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin)
                return false;

            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}