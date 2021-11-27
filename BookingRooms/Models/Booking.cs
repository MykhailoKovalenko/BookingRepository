using BookingRooms.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Models
{
    [MaxBookingHours(maxBookingHours: 8,  ErrorMessage = "Booking time should not exceed {0} hours!")]
    public class Booking
    {
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
