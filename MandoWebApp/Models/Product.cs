using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(name: "HU_Name", TypeName = "varchar(150)")]
        public string HUName { get; set; }

        [Column(name: "EN_Name", TypeName = "varchar(150)")]
        public string? ENName { get; set; }

        public int CategoryID { get; set; }
        public int UnitID { get; set; }
        public SizeType? SizeType { get; set; }
        public ICollection<BuildingProduct> BuildingProducts { get; set; }
        public ICollection<BuildingProductHistory> BuildingProductHistories { get; set; }

        public string Name(string lang) => lang switch
        {
            "hu" => HUName,
            "en" => ENName,
            _ => HUName
        } ?? HUName!;
    }

    public enum SizeType
    {
        Numbered, // 32, 36, 44
        TShirt, // S, M, L, XL
        Child // 126, 134
    }
}
