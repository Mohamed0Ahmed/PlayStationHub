﻿using System.Shared.BaseModel;

namespace System.Domain.Entities
{
    public class Guest : BaseEntity<string>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int StoreId { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public Store Store { get; set; }
        public string? CurrentSessionId { get; set; }
    }
}