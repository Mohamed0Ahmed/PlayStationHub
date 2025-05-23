﻿using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Customer : BaseEntity<int>
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public List<Order> Orders { get; set; } = [];
        public List<CustomerPoints> CustomerPoints { get; set; } = [];
    }
}