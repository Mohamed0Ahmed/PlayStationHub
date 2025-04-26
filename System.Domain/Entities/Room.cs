using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Room : BaseEntity<int>
    {
        public string RoomName { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public List<Guest> Guests { get; set; } = [];
    }
}