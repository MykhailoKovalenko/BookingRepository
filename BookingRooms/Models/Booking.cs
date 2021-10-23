using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public Room Room { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public User Author {get; set; }
        public int RoomId { get; set; }
        public int AuthorId { get; set; }
    }
}
