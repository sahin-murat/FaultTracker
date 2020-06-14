using System;
using System.Collections.Generic;
using System.Text;

namespace FaultTracker.Business.DataTransfer.Shared
{
    public class MaintenanceHistorySharedDto
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public string ActionTypeName { get; set; }

    }
}
