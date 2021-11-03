using BookingRooms.DBContext;
using BookingRooms.Models;
using Microsoft.EntityFrameworkCore;
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
        public async IAsyncEnumerable<Booking> GetAllForPeriodAsync(DateTime startDate, DateTime endDate)
        {
            var booking = _context.Bookings.Where(i => i.Start < endDate && startDate < i.End);

            var enumerator = booking.AsAsyncEnumerable().GetAsyncEnumerator();

            while(await enumerator.MoveNextAsync())
            {
                yield return enumerator.Current;
            }
        }

        public Task<Booking> GetAsync(int id)
        {
            return _context.Bookings
                .Include(i => i.User)
                .Include(i => i.Room)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        public Booking Add(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return booking;
        }

        public Booking Update(Booking booking)
        {
            Booking existingbooking = _context.Bookings.Find(booking.Id);

            existingbooking.Start = booking.Start;
            existingbooking.End = booking.End;
            existingbooking.RoomId = booking.RoomId;
            existingbooking.UserId = booking.UserId; 

            _context.SaveChanges();

            return existingbooking;
        }
    }
}
