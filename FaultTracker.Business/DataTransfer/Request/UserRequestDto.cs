using FaultTracker.Business.DataTransfer.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace FaultTracker.Business.DataTransfer.Request
{
    public class UserRequestDto : UserSharedDto
    {
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
