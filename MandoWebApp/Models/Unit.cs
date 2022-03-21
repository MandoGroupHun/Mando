//using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("Unit")]
    public class Unit
    {
        public int ID { get; set; }

        [Column(name: "HU_Name", TypeName = "varchar(20)")]
        public string HUName { get; set; }

        [Column(name: "EN_Name", TypeName = "varchar(20)")]
        public string? ENName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
