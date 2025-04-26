using System.Domain.Entities;
using System.Shared.BaseModel;

namespace System.Application.Interfaces
{
    public interface IAdminService
    {
        Task<List<Store>> GetAllStoresAsync();
        Task<List<Store>> GetDeletedStoresAsync();
        Task<Store> CreateStoreAsync(string storeName, string address);
        Task<Store> UpdateStoreAsync(int id, string storeName, string address);
        Task<bool> DeleteStoreAsync(int id);
        Task<bool> RestoreStoreAsync(int id);
        Task<Store> GetStoreByIdAsync(int id);
        Task<List<Branch>> GetBranchesByStoreIdAsync(int storeId);
        Task<Branch> CreateBranchAsync(int storeId, string branchName);
        Task<Branch> UpdateBranchAsync(int id, string branchName);
        Task<bool> DeleteBranchAsync(int id);
        Task<List<Room>> GetRoomsByBranchIdAsync(int branchId);
        Task<Room> CreateRoomAsync(int branchId, string roomName);
        Task<Room> UpdateRoomAsync(int id, string roomName);
        Task<bool> DeleteRoomAsync(int id);
        Task<List<Guest>> GetGuestsByRoomIdAsync(int roomId);
        Task<Guest> CreateGuestAsync(int roomId, int storeId, string username, string password);
        Task<bool> DeleteGuestAsync(string id);
        Task<bool> GuestLoginAsync(string username, string password, string storeName);
        Task<Customer> CreateCustomerAsync(string phoneNumber, int branchId);
        Task<List<UserDto>> GetAllMainOwnersAsync();
        Task<UserDto> CreateMainOwnerAsync(string email, string password, int storeId);
        Task<UserDto> UpdateMainOwnerAsync(string id, string email);
        Task<bool> DeleteMainOwnerAsync(string id);
        Task<bool> AdminLoginAsync(string email, string password);
        Task<bool> OwnerLoginAsync(string email, string password);
        Task LogoutAsync();
    }
}