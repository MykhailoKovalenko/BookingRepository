using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;

namespace BookingRooms.Repository
{
    public interface IRoomRepository
    {
        List<Room> GetAll();
        Room Get(int id);
        Room Add(Room room);
    }
}
