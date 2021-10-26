using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;
using BookingRooms.Interfaces;
using BookingRooms.DataAccessLayer.Repository;
using BookingRooms.DBContext;

namespace BookingRooms.Services
{
    public class RoomService : IRoomService
    {

        private IRoomRepository _roomRepository;
        private IBookingRepository _bookingRepository;
        public RoomService(IRoomRepository roomRepository, IBookingRepository bookingRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }

        public IEnumerable<Room> GetAll() => _roomRepository.GetAll();
        public Room Get(int id) => _roomRepository.Get(id);
        public Room Add(Room room) => _roomRepository.Add(room);
        public Room Update(Room room) => _roomRepository.Update(room);
        public Room Delete(int id) => _roomRepository.Delete(id);
        public IEnumerable<Room> GetFree(DateTime startDate, DateTime endDate)
        {
            var bookedRoomIds = _bookingRepository.GetAllForPeriod(startDate, endDate).Select(i => i.RoomId);

            IEnumerable<Room> freeRooms = _roomRepository.GetAll().Where(i => !bookedRoomIds.Contains(i.Id));

            return freeRooms;
        }
    }
}
