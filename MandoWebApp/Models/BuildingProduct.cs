using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("Map_Building_Product")]
    public class BuildingProduct
    {
        public int BuildingID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string? Size { get; set; }
    }
}
