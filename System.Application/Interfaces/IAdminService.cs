using System.Domain.Entities;
using System.Shared.BaseModel;

namespace System.Application.Interfaces
{
    public interface IAdminService
    {
        // Store Operations
        Task<List<Store>> GetAllStoresAsync();
        Task<List<Store>> GetDeletedStoresAsync();
        Task<Store> CreateStoreAsync(string storeName, string address);
        Task<Store> UpdateStoreAsync(int id, string storeName, string address);
        Task<bool> DeleteStoreAsync(int id);
        Task<bool> RestoreStoreAsync(int id);
        Task<Store> GetStoreByIdAsync(int id);

        // Branch Operations
        Task<List<Branch>> GetBranchesByStoreIdAsync(int storeId);
        Task<List<Branch>> GetDeletedBranchesAsync(int storeId);
        Task<Branch> CreateBranchAsync(int storeId, string branchName);
        Task<Branch> UpdateBranchAsync(int id, string branchName);
        Task<bool> DeleteBranchAsync(int id);
        Task<bool> RestoreBranchAsync(int id);

        // Room Operations
        Task<List<Room>> GetRoomsByBranchIdAsync(int branchId);
        Task<List<Room>> GetDeletedRoomsAsync(int branchId);
        Task<Room> CreateRoomAsync(int branchId, string roomName);
        Task<Room> UpdateRoomAsync(int id, string roomName);
        Task<bool> DeleteRoomAsync(int id);
        Task<bool> RestoreRoomAsync(int id);
        Task<bool> HasGuestAsync(int roomId);

        // Guest Operations
        Task<List<Guest>> GetGuestsByRoomIdAsync(int roomId);
        Task<List<Guest>> GetDeletedGuestsAsync(int branchId);
        Task<Guest> CreateGuestAsync(int roomId, int storeId, string username, string password);
        Task<bool> DeleteGuestAsync(string id);
        Task<bool> RestoreGuestAsync(string id);
        Task<string> GuestLoginAsync(string username, string password, string storeName);

        // Customer Operations
        Task<Customer> CreateCustomerAsync(string phoneNumber, int branchId);

        // Owner Operations
        Task<List<UserDto>> GetAllMainOwnersAsync();
        Task<List<UserDto>> GetDeletedOwnersAsync();
        Task<UserDto> CreateMainOwnerAsync(string email, string password, int storeId);
        Task<UserDto> UpdateMainOwnerAsync(string id, string email);
        Task<bool> DeleteMainOwnerAsync(string id);
        Task<bool> RestoreOwnerAsync(string id);

        // Authentication and Authorization
        Task<(bool isSuccess, string redirectUrl)> UserLoginAsync(string email, string password);
        Task<bool> AdminLoginAsync(string email, string password);
        Task<bool> OwnerLoginAsync(string email, string password);
        Task LogoutAsync();

        // Linking Operations
        Task LinkOwnerToStoreAsync(string email, int storeId);
    }
}