//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("Unit")]
    public class Unit
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string HU_Name { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? EN_Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
