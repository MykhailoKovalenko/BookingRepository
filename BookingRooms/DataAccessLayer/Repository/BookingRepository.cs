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

        public IEnumerable<Booking> GetAllForPeriod(DateTime startDate, DateTime endDate)
        {
            var booking = _context.Bookings.Where(i => i.Start < endDate && startDate < i.End);

            //i => (startDate < i.Start || startDate >= i.End) && (endDate <= i.Start || endDate > i.End));

            // i => i.Start >= startDate && i.End <= startDate 

            return booking;
        }

        public Booking Add(Booking booking) //(int roomId, int userId, DateTime startDate, DateTime endDate)
        {
            //Booking newBooking = new Booking() { RoomId = roomId, UserId = userId, Start = startDate, End = endDate };
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return booking;
        }
    }
}
