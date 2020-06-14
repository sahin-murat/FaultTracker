using FaultTracker.Business.DataTransfer.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace FaultTracker.Business.DataTransfer.Request
{
    public class VehicleTypeRequestDto : VehicleTypeSharedDto
    {
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
