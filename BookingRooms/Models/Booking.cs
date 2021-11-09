using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Models
{
    public class Booking
    {
        //[Required(ErrorMessage = "Booking Id is not specified")]
        //[Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Id { get; set; }
       
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime End { get; set; }
        
        [Required]
        public int RoomId { get; set; }
        
        [Required]
        public int UserId { get; set; }
        public virtual User User {get; set; }
        public virtual Room Room { get; set; }     
    }
}
