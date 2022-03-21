using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("Product")]
    public class Product
    {
        public int ID { get; set; }

        [Column(name: "HU_Name", TypeName = "varchar(150)")]
        public string HUName { get; set; }

        [Column(name: "EN_Name", TypeName = "varchar(150)")]
        public string? ENName { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        public string Category { get; set; }
        
        public int UnitID { get; set; }
        public SizeType? SizeType { get; set; }
        public ICollection<BuildingProduct> BuildingProducts { get; set; }
    }

    public enum SizeType
    {
        Numbered, // 32, 36, 44
        TShirt, // S, M, L, XL
        Child // 126, 134
    }
}
