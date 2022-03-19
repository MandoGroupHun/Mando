using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("Invite")]
    public class Invite
    {
        public Guid InviteId { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Email { get; set; }
    }
}
