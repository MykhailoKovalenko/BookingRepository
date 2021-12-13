using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;

namespace BookingRooms.DataAccessLayer.Repository
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllForPeriodAsync(DateTime startDate, DateTime endDate);
        Task<Booking> GetAsync(int id);
        Task<Booking> AddAsync(Booking booking);
        Task<bool> UpdateAsync(Booking booking);
        Task<bool> DeleteAsync(int id);
    }
}
