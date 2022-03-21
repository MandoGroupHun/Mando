using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("Building")]
    public class Building
    {
        public int ID { get; set; }

        [Column(name: "HU_Name", TypeName = "varchar(100)")]
        public string HUName { get; set; }

        [Column(name: "EN_Name", TypeName = "varchar(100)")]
        public string? ENName { get; set; }

        public int Zip { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        public string Address1 { get; set; }

        [Column(name: "HU_Description", TypeName = "varchar(500)")]
        public string? HUDescription { get; set; }
        
        [Column(name: "En_Description", TypeName = "varchar(500)")]
        public string? ENDescription { get; set; }
        public ICollection<BuildingProduct> BuildingProducts { get; set; }
        
    }
}
