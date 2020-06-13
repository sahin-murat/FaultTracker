using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FaultTracker.Data.Entities
{
    public class ActionType : BaseEntityModel
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        //Virtual Objects
        public virtual ICollection<MaintenanceHistory> MaintenanceHistories { get; set; }
    }
}
