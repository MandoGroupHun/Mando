using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("Invite")]
    public class Invite
    {
        public Guid InviteId { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [Required]
        [MaxLength(200)]
        [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$", ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public InviteStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public enum InviteStatus
    {
        New,
        Sent,
        Claimed
    }
}
