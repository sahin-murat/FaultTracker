using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FaultTracker.Data.Entities
{
    public class Status : BaseEntityModel
    {
        [Required, StringLength(255)]
        public string Name { get; set; }

        //Virtual Objects
        public virtual ICollection<Maintenance> Maintenances{ get; set; }
    }
}
