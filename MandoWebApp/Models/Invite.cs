﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("Invite")]
    public class Invite
    {
        public Guid InviteId { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public InviteStatus Status { get; set; }
    }

    public enum InviteStatus
    {
        New,
        Sent,
        Claimed
    }
}
