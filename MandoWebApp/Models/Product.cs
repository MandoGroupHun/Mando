using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("Product")]
    public class Product
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string HU_Name { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string? EN_Name { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        public string Category { get; set; }
        
        public int UnitID { get; set; }
        public SizeType? SizeType { get; set; }
        public ICollection<Building_Product> Building_Products { get; set; }
    }

    public enum SizeType
    {
        Numbered, //32, 36, 44
        TShirt, //S, M, L, XL
        Child // 126, 134
    }
}
