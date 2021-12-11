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

        public async Task<IEnumerable<Booking>> GetAllForPeriodAsync(DateTime startDate, DateTime endDate)
        {
            var bookings = await _context.Bookings
                                .Include(i => i.User)
                                .Include(i => i.Room)
                                .AsQueryable()
                                .Where(i => i.Start < endDate && startDate < i.End)
                                .ToListAsync();

            return bookings;
        }

        public async Task<Booking> GetAsync(int id)
        {
            return await _context.Bookings
                .Include(i => i.User)
                .Include(i => i.Room)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);

            await SaveChangesAsync();

            return booking;
        }

        public async Task<bool> ChangeRoomAsync(int id, int roomId)
        {
            Booking existingbooking = await GetAsync(id);

            existingbooking.RoomId = roomId;

            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Booking booking)
        {
            Booking existingbooking = await GetAsync(booking.Id);
            
            existingbooking.Start = booking.Start;
            existingbooking.End = booking.End;
            existingbooking.RoomId = booking.RoomId;
            existingbooking.UserId = booking.UserId;

            return await SaveChangesAsync();     
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Booking booking = await GetAsync(id);

            _context.Bookings.Remove(booking);

            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
