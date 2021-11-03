using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;

namespace BookingRooms.Interfaces
{
    public interface IRoomService
    {
        IAsyncEnumerable<Room> GetAllAsync();
        Task<Room> GetAsync(int id);
        Room Add(Room room);
        Room Update(Room room);
        Room Delete(int id);
        IAsyncEnumerable<Room> GetFreeAsync(DateTime startDate, DateTime endDate);

    }
}
