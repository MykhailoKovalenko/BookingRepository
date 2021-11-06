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

        public async IAsyncEnumerable<Room> GetAllAsync()
        {
            await foreach (var room in _roomRepository.GetAllAsync())
            {
                yield return room;
            }
        } 

        public Task<Room> GetAsync(int id) => _roomRepository.GetAsync(id);
        public Room Add(Room room) => _roomRepository.Add(room);
        public Room Update(Room room) => _roomRepository.Update(room);
        public Room Delete(int id) => _roomRepository.Delete(id);
        public async IAsyncEnumerable<Room> GetFreeAsync(DateTime startDate, DateTime endDate)
        {
            //List<int> bookedRoomIds = new List<int>();

             var bookedRoomIds = (await _bookingRepository.GetAllForPeriod(startDate, endDate)).Select(i=> i.Id);

            //await foreach (var booking in _bookingRepository.GetAllForPeriod(startDate, endDate))
            //{
            //    bookedRoomIds.Add(booking.RoomId);
            //}

            await foreach (var room in _roomRepository.GetAllAsync())
            {
                if (!bookedRoomIds.Contains(room.Id))
                    yield return room;    
            }
        }
    }
}
