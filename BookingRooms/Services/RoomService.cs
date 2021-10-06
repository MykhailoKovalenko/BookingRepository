using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;
using BookingRooms.Interfaces;

namespace BookingRooms.Services
{
    public class RoomService : IRoomService
    {
        static List<Room> Rooms { get; }
        static RoomService()
        {
            Rooms = new List<Room>
            {
                new Room { Id = 1, Name = "Colorado", Places = 10 },
                new Room { Id = 2, Name = "Minesota", Places = 5 },
                new Room { Id = 3, Name = "New York", Places = 15}
            };
        }

        public List<Room> GetAll() => Rooms;
        public Room Get(int id) => Rooms.FirstOrDefault(x => x.Id == id);
    }
}
