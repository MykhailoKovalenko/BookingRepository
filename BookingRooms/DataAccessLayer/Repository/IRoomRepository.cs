using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;

namespace BookingRooms.DataAccessLayer.Repository
{
    public interface IRoomRepository
    {
        IAsyncEnumerable<Room> GetAllAsync();
        Task<Room> GetAsync(int id);
        Room Add(Room room);
        Room Update(Room room);
        Room Delete(int id);
    }
}
