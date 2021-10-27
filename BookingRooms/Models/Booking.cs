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
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public User User {get; set; }
        public Room Room { get; set; }     
    }
}
