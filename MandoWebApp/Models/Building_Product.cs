using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MandoWebApp.Models
{
    [Table("Map_Building_Product")]
    public class Building_Product
    {
        public int BuildingID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string? Size { get; set; }

    }

}
