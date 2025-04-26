using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Store : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<Branch> Branches { get; set; } = [];
        public List<PointsSetting> PointsSettings { get; set; } = [];
        public List<Reward> Rewards { get; set; } = [];
        public List<UserStore> UserStores { get; set; } = [];
        public List<Guest> Guests { get; set; } = [];

    }
}