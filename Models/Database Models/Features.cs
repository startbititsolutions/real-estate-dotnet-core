using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Database_Models
{
    public class Features
    {
        [Key]
        public int FeaturesId { get; set; }
        public bool SwimmingPool { get; set; }
        public bool Stories {get; set; }
        public bool EmergencyExit { get; set; }
        public bool FirePlace { get; set; }
        public bool LaundryRoom { get; set; }
        public bool JogPath { get; set; }
        public bool Ceilings { get; set; }
        public bool DoubleSinks { get; set; }
        public bool HurricaneShutters { get; set; }

        public int PropertyId { get; set; }

        public virtual Property Property
        {
            get; set;
        }


    }
}
