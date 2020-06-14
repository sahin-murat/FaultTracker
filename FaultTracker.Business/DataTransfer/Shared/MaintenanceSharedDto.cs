using AutoMapper.Mappers;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace FaultTracker.Business.DataTransfer.Shared
{
    public class MaintenanceSharedDto
    {
        public int ID { get; set; }
        public string VehiclePlateNo { get; set; }
        public string VehicleTypeName { get; set; }
        public string Description { get; set; }
        public string DriverFullName { get; set; }
        public string DriverPhoneNumber { get; set; }
        public DateTime ExpectedTimeToFix { get; set; }
        public string PictureImage { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationLatitude { get; set; }
        public List<MaintenanceHistorySharedDto> MaintenanceHistories { get; set; }
        public string StatusName { get; set; }
        public int StatusID { get; set; }
    }
}
