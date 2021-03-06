using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;
using SharedBookingLibrary.RequestClasses;

namespace BookingRooms.DataAccessLayer.Repository
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetAsync(int id);
        Task<Room> GetByNameAsync(string name);
        Task<Room> GetByNameExceptAsync(string name, int exceptRoomId);
        Task<Room> AddAsync(Room room);
        Task<bool> UpdateAsync(Room room);
        Task<bool> DeleteAsync(int id);
    }
}
