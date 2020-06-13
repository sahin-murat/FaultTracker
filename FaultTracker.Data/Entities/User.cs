using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FaultTracker.Data.Entities
{
    public class User : BaseEntityModel
    {
        [Required, StringLength(255)]
        public string FirstName { get; set; }
        [Required, StringLength(255)]
        public string LastName { get; set; }
        [Required, StringLength(50)]
        public string PhoneNumber { get; set; }
        [Required, StringLength(255)]
        public string Address { get; set; }
        public string profilepicture { get; set; }

        //Virtual Objects
        public virtual ICollection<Maintenance> Maintenances{ get; set; }
    }
}
