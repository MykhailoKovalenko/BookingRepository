using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;

namespace BookingRooms.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetAsync(int id);
        Task<IEnumerable<Room>> GetFreeAsync(DateTime startDate, DateTime endDate);
        Task<Room> AddAsync(Room room);
        Task<bool> UpdateAsync(Room room);
        Task<bool> DeleteAsync(int id);
    }
}
