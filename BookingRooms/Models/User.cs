using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
