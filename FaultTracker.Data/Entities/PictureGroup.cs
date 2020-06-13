using System.Collections.Generic;

namespace FaultTracker.Data.Entities
{
    public class PictureGroup : BaseEntityModel
    {
        public string PictureImage { get; set; }

        //Virtual Objects
        public virtual ICollection<Maintenance> Maintenances{ get; set; }
    }
}
