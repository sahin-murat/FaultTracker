using System;
using System.Collections.Generic;
using System.Text;

namespace FaultTracker.Business.DataTransfer.Request
{
    public class MaintenanceHistoryRequestDto
    {
        public int ID { get; set; }
        public int ActionTypeID { get; set; }
        public int MaintenanceID { get; set; }
        public string text { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
