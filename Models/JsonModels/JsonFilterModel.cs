using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.JsonModels
{
    public class JsonFilterModel
    {
        public string KeyWord { get; set; }
        public string PriceRange { get; set; }
        public string BathRoomsRange { get; set; }
        public string BedRoomsRange { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public string PropertyGeo { get; set; }
        public string FirePlace { get; set; }
        public string DualSinks { get; set; }
        public string SwimmingPool { get; set; }
        public string Stories { get; set; }
        public string LaundryRoom { get; set; }
        public string EmergencyExit { get; set; }
        public string JogPath { get; set; }
        public string Ceilings { get; set; }
        public string HurricaneShutters { get; set; }
        public string PropertyDate { get; set; }
        public string PropertyPrice { get; set; }
        public string Offset { get; set; }
    }
}