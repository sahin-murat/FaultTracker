using System;
using System.Collections.Generic;
using System.Text;

namespace FaultTracker.Business.DataTransfer.Shared
{
    public class MaintenanceHistorySharedDto
    {
        public int ID { get; set; }
        public int MaintenanceID { get; set; }
        public string Text { get; set; }
        public int ActionTypeID { get; set; }        
        public string ActionTypeName { get; set; }

    }
}
