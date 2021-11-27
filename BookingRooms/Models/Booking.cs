using BookingRooms.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Models
{
    public class Booking
    {
        public int Id { get; set; }  
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public virtual User User {get; set; }
        public virtual Room Room { get; set; }     
    }
}
