using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;

namespace BookingRooms.Interfaces
{
    public interface IRoomService
    {
        public List<Room> GetAll();
        public Room Get(int id);
        public Room Add(Room room);
    }
}
