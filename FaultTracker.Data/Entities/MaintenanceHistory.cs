using System.ComponentModel.DataAnnotations;

namespace FaultTracker.Data.Entities
{
    public class MaintenanceHistory : BaseEntityModel
    {
        public int MaintenanceID { get; set; }
        public int ActionTypeID { get; set; }
        [Required, StringLength(200)]
        public string text { get; set; }

        //Virtual Objects
        public virtual Maintenance Maintenance { get; set; }
        public virtual ActionType ActionType { get; set; }
    }
}
