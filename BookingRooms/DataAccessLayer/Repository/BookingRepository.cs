using BookingRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.DataAccessLayer.Repository
{
    public class BookingRepository : IBookingRepository
    {
        
        public IEnumerable<Booking> GetBooking(DateTime startDate, DateTime endDate)
        {
            using (DBContext.BRoomsContext context = new DBContext.BRoomsContext())
            {
                var booking = context.Bookings.Where(i => i.Start < endDate && startDate < i.End);
                
                
                //i => (startDate < i.Start || startDate >= i.End) && (endDate <= i.Start || endDate > i.End));

                    // i => i.Start >= startDate && i.End <= startDate 

                return booking;
            }
        }
    }
}
