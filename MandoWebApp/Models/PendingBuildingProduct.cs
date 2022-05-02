using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("PendingBuildingProducts")]
    public class PendingBuildingProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int BuildingID { get; set; }

        public int CategoryID { get; set; }

        [Column(name: "HU_ProductName", TypeName = "varchar(150)")]
        public string? HuProductName { get; set; }

        [Column(name: "EN_ProductName", TypeName = "varchar(150)")]
        public string? EnProductName { get; set; }

        public int Quantity { get; set; }

        public SizeType? SizeType { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string Size { get; set; }

        public int UnitID { get; set; }

        public DateTime RecordedAt { get; set; }

        public string UserId { get; set; }

        public bool IsProcessed { get; set; }

        public bool IsAccepted { get; set; }

        public string? ProcessedByUserId { get; set; }
    }
}
