using BookingRooms.DBContext;
using BookingRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.DataAccessLayer.Repository
{
    public class BookingRepository : IBookingRepository
    {

        private readonly BRoomsContext _context;
        public BookingRepository(BRoomsContext context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetBooking(DateTime startDate, DateTime endDate)
        {
            var booking = _context.Bookings.Where(i => i.Start < endDate && startDate < i.End);

            //i => (startDate < i.Start || startDate >= i.End) && (endDate <= i.Start || endDate > i.End));

            // i => i.Start >= startDate && i.End <= startDate 

            return booking;
        }
    }
}
