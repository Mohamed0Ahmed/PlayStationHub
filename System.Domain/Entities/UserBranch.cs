using Microsoft.AspNetCore.Identity;

namespace System.Domain.Entities
{
    public class UserBranch
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}