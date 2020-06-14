namespace FaultTracker.Business.DataTransfer.Request
{
    public class VehicleRequestDto
    {
        public int ID { get; set; }
        public string PlateNo{ get; set; }
        public int VehicleTypeID { get; set; }
        public int UserID { get; set; }
        public int ModifiedBy { get; set; }
        public int CreatedBy { get; set; }
    }
}
