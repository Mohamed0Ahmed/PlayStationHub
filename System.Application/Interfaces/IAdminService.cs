using System.Domain.Entities;
using System.Shared.BaseModel;

namespace System.Application.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<Store>> GetAllStoresAsync();
        Task<IEnumerable<Store>> GetDeletedStoresAsync();
        Task<Store> CreateStoreAsync(string storeName, string address);
        Task<Store> UpdateStoreAsync(int id, string storeName, string address);
        Task<bool> DeleteStoreAsync(int id);
        Task<bool> RestoreStoreAsync(int id);
        Task<Store> GetStoreByIdAsync(int storeId);

        Task<IEnumerable<Branch>> GetAllBranchesAsync();
        Task<IEnumerable<Branch>> GetBranchesByStoreIdAsync(int storeId);
        Task<Branch> CreateBranchAsync(int storeId, string branchName);
        Task<Branch> UpdateBranchAsync(int id, string branchName);
        Task<bool> DeleteBranchAsync(int id);
        Task<Branch> GetBranchByIdAsync(int branchId);

        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<IEnumerable<Room>> GetRoomsByBranchIdAsync(int branchId);
        Task<Room> CreateRoomAsync(int branchId, string roomName);
        Task<Room> UpdateRoomAsync(int id, string roomName);
        Task<bool> DeleteRoomAsync(int id);
        Task<Room> GetRoomByIdAsync(int roomId);

        Task<IEnumerable<Guest>> GetGuestsByRoomIdAsync(int roomId);
        Task<Guest> CreateGuestAsync(int roomId);
        Task<bool> DeleteGuestAsync(string id);

        Task<IEnumerable<UserDto>> GetAllMainOwnersAsync();
        Task<UserDto> CreateMainOwnerAsync(string email, string password, int storeId);
        Task<UserDto> UpdateMainOwnerAsync(string id, string email);
        Task<bool> DeleteMainOwnerAsync(string id);
        Task<int?> GetMainOwnerStoreIdAsync(string ownerId);

        Task<bool> AdminLoginAsync(string email, string password);
        Task LogoutAsync();
    }
}