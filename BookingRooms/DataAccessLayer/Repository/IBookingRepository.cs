using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;

namespace BookingRooms.DataAccessLayer.Repository
{
    public interface IBookingRepository
    {
        IAsyncEnumerable<Booking> GetAllForPeriodAsync(DateTime startDate, DateTime endDate);
        Task<Booking> GetAsync(int id);
        Booking Add(Booking booking);
        Booking Update(Booking booking);
    }
}
