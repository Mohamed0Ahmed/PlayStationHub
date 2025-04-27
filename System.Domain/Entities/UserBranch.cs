using Microsoft.AspNetCore.Identity;
using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class UserBranch : BaseEntity<int>
    {
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}