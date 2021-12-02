using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Places { get; set; }
        public bool IsProjector { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
