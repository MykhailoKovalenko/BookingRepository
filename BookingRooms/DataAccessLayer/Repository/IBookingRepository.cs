using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;

namespace BookingRooms.DataAccessLayer.Repository
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetBooking(DateTime startDate, DateTime endDate);
    }
}
