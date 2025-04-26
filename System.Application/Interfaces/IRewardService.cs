using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IRewardService
    {
        Task<IEnumerable<Reward>> GetRewardsByBranchAsync(int branchId);
        Task<Reward> CreateRewardAsync(int branchId, string name, int requiredPoints);
        Task<Reward> UpdateRewardAsync(int rewardId, string name, int requiredPoints);
        Task<bool> DeleteRewardAsync(int rewardId);
    }
}