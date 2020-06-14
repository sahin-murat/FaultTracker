using System.ComponentModel.DataAnnotations;

namespace FaultTracker.Business.DataTransfer.Shared
{
    public class StatusSharedDto
    {
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
