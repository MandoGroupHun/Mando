using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MandoWebApp.Models
{
    public class Building
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Zip { get; set; }
        public string Address1 { get; set; }
        public string HU_Description { get; set; }
        public string EN_Description { get; set; }
        
        public ICollection<Storage> Storages { get; set; }

    }
}
