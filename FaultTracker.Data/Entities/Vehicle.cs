using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FaultTracker.Data.Entities
{
    public class Vehicle : BaseEntityModel
    {
        [Required, StringLength(60)]
        public string PlateNo { get; set; }
        public int VehicleTypeID { get; set; }
        public int UserID { get; set; }

        //Virtual Objects
        public virtual VehicleType VehicleType { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Maintenance> Maintenances { get; set; }
    }
}
