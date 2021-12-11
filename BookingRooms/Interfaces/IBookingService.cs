using BookingRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllForPeriodAsync(DateTime startDate, DateTime endDate);
        Task<Booking> GetAsync(int id);
        Task<Booking> AddAsync(Booking booking);
        Task<bool> ChangeRoomAsync(int id, int roomId);
        Task<bool> UpdateAsync(Booking booking);
        Task<bool> DeleteAsync(int id);
    }
}
