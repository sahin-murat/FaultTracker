using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FaultTracker.Data.Entities
{
    public class Maintenance : BaseEntityModel
    {
        public int VehicleID { get; set; }
        public int UserID { get; set; }
        [Required, StringLength(255)]
        public string Description { get; set; }
        public int PictureGroupID { get; set; }
        public DateTime ExpectedTimeToFix { get; set; }
        public int ResponsibleUserID { get; set; }
        [Required, StringLength(20)]
        public string LocationLongitude { get; set; }
        [Required, StringLength(20)]
        public string LocationLatitude { get; set; }
        public int StatusID { get; set; }

        //Virtual Objects
        public virtual Vehicle Vehicle { get; set; } 
        public virtual User User { get; set; } 
        public virtual PictureGroup PictureGroup { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<MaintenanceHistory> MaintenanceHistories { get; set; }

    }
}
