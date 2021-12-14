using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;
using SharedBookingLibrary.RequestClasses;

namespace BookingRooms.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetAsync(int id);
        Task<Room> GetByNameAsync(string name);
        Task<Room> GetByNameExceptAsync(string name, int exceptRoomId);
        Task<IEnumerable<Room>> GetFreeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Room>> GetFreeForBookingAsync(DateTime startDate, DateTime endDate, Booking booking);
        Task<IEnumerable<Room>> GetByConditionAsync(RoomParameters roomParameters);
        Task<Room> AddAsync(Room room);
        Task<bool> UpdateAsync(Room room);
        Task<bool> DeleteAsync(int id);
    }
}
