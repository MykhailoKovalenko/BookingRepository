using BookingRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Interfaces
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllForPeriod(DateTime startDate, DateTime endDate);
        Task<Booking> GetAsync(int id);
        Booking Add(Booking booking);
        Booking Update(Booking booking);
    }
}
