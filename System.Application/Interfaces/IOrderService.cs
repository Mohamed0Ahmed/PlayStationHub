using System.Domain.Entities;
using System.Shared.BaseModel;

namespace System.Application.Interfaces
{
    public interface IOwnerService
    {
        Task<bool> OwnerLoginAsync(string email, string password);
        Task LogoutAsync();

        Task<IEnumerable<UserDto>> GetAllBranchManagersAsync();
        Task<UserDto> CreateBranchManagerAsync(string email, string password, int branchId);
        Task<UserDto> UpdateBranchManagerAsync(string id, string email);
        Task<bool> DeleteBranchManagerAsync(string id);
        Task<bool> LockBranchManagerAsync(string id);
        Task<bool> UnlockBranchManagerAsync(string id);

        Task<IEnumerable<Branch>> GetBranchesAsync(string ownerId);
        Task<string> GenerateQrCodeAsync(int roomId);

        Task<IEnumerable<Product>> GetProductsByBranchAsync(int branchId);
        Task<Product> CreateProductAsync(int branchId, string name, decimal price);
        Task<Product> UpdateProductAsync(int productId, string name, decimal price);
        Task<bool> DeleteProductAsync(int productId);

        Task<IEnumerable<Reward>> GetRewardsByBranchAsync(int branchId);
        Task<Reward> CreateRewardAsync(int branchId, string name, int requiredPoints);
        Task<Reward> UpdateRewardAsync(int rewardId, string name, int requiredPoints);
        Task<bool> DeleteRewardAsync(int rewardId);

        Task<IEnumerable<Order>> GetPendingOrdersAsync(int? branchId = null);
        Task ConfirmOrderAsync(int orderId);
        Task CancelOrderAsync(int orderId, string reason);

        Task<IEnumerable<HelpRequest>> GetPendingHelpRequestsAsync(int? branchId = null);
        Task ConfirmHelpRequestAsync(int helpRequestId);
        Task CancelHelpRequestAsync(int helpRequestId, string reason);

        Task<IEnumerable<Room>> GetRoomsByBranchAsync(int branchId);
    }
}