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

        public List<Room> GetAll() => _roomRepository.GetAll();
        public Room Get(int id) => _roomRepository.Get(id);
        public Room Add(Room room) => _roomRepository.Add(room);
        public Room Update(Room room) => _roomRepository.Update(room);
        public Room Delete(int id) => _roomRepository.Delete(id);

    }
}
