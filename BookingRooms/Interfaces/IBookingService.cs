using BookingRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<Booking> GetAllForPeriod(DateTime startDate, DateTime endDate);
        Booking Add(Booking booking);
    }
}
