using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("Building")]
    public class Building
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string HU_Name { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? EN_Name { get; set; }

        public int Zip { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        public string Address1 { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? HU_Description { get; set; }
        
        [Column(TypeName = "varchar(500)")]
        public string? EN_Description { get; set; }
        public ICollection<Building_Product> Building_Products { get; set; }
        
    }
}
