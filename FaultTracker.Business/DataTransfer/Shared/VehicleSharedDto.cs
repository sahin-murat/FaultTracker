using System;
using System.Collections.Generic;
using System.Text;

namespace FaultTracker.Business.DataTransfer.Shared
{
    public class VehicleSharedDto
    {
        public int ID { get; set; }
        public string PlateNo { get; set; }
        public int VehicleTypeID { get; set; }
        public string VehicleTypeName { get; set; }
        public int UserID { get; set; }
        public string UserFullName { get; set; }
    }
}
