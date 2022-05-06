using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("Map_Building_Product_History")]
    public class BuildingProductHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int BuildingID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string Size { get; set; }

        public DateTime RecordedAt { get; set; }

        public string UserId { get; set; }
    }
}
