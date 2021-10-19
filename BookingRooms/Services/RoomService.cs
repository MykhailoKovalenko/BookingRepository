using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;
using BookingRooms.Interfaces;
using BookingRooms.Repository;
using BookingRooms.DBContext;

namespace BookingRooms.Services
{
    public class RoomService : IRoomService
    {

        private IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        //static List<Room> Rooms { get; }
        //static RoomService()
        //{
        //    Rooms = new List<Room>
        //    {
        //        new Room { Id = 1, Name = "Colorado", Places = 10 },
        //        new Room { Id = 2, Name = "Minesota", Places = 5 },
        //        new Room { Id = 3, Name = "New York", Places = 15}
        //    };
        //}

        public List<Room> GetAll() => _roomRepository.GetAll();
        public Room Get(int id) => _roomRepository.Get(id);  //Rooms.FirstOrDefault(x => x.Id == id);
        public Room Add(Room room) => _roomRepository.Add(room);
        
    }
}
