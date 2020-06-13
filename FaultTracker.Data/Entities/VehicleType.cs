using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FaultTracker.Data.Entities
{
    public class VehicleType : BaseEntityModel
    {
        [Required, StringLength(255)]
        public string Name { get; set; }

        //Virtual Objects
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
