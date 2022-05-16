using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("SizeTypes")]
    public class SizeType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(name: "HU_Name", TypeName = "varchar(50)")]
        public string HUName { get; set; }

        [Column(name: "EN_Name", TypeName = "varchar(50)")]
        public string ENName { get; set; }

        public string Examples { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<PendingBuildingProduct> PendingBuildingProducts { get; set; }

        public string Name(string lang) => lang switch
        {
            "hu" => HUName,
            "en" => ENName,
            _ => HUName
        } ?? HUName!;
    }
}
