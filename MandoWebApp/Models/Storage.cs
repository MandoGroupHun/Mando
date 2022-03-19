using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MandoWebApp.Models
{
    public class Storage
    {
        public int ID { get; set; }
        public int BuildingID { get; set; }
        public string HU_Name { get; set; }
        public string EN_Name { get; set; }
        public string HU_Description { get; set; }
        public string EN_Description { get; set; }
    }
}
