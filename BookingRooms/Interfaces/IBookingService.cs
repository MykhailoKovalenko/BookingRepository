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
        Booking Get(int id);
        Task<Booking> GetAsync(int id);
        Booking Add(Booking booking);
        Booking Update(Booking booking);
    }
}
