using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;
using BookingRooms.Interfaces;
using BookingRooms.DataAccessLayer.Repository;
using BookingRooms.DBContext;
using SharedBookingLibrary.RequestClasses;

namespace BookingRooms.Services
{
    public class RoomService : IRoomService
    {

        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        public RoomService(IRoomRepository roomRepository, IBookingRepository bookingRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<Room>> GetAllAsync() => await _roomRepository.GetAllAsync(); 
        public async Task<Room> GetAsync(int id) => await _roomRepository.GetAsync(id);
        public async Task<Room> GetByNameAsync(string name) => await _roomRepository.GetByNameAsync(name);
        public async Task<IEnumerable<Room>> GetFreeAsync(DateTime startDate, DateTime endDate)
        { 
            var bookedRoomIds = (await _bookingRepository.GetAllForPeriodAsync(startDate, endDate))
                                    .Select(i => i.RoomId);

            var freeRooms = (await _roomRepository.GetAllAsync())
                                    .Where(i => !bookedRoomIds.Contains(i.Id));

            return freeRooms;
        }

        public async Task<IEnumerable<Room>> GetFreeForBookingAsync(DateTime startDate, DateTime endDate, Booking booking)
        {
            var bookedRoomIds = (await _bookingRepository.GetAllForPeriodAsync(startDate, endDate))
                                    .Where(i => i.Id != booking.Id)
                                    .Select(i => i.RoomId);

            var freeRooms = (await _roomRepository.GetAllAsync())
                                    .Where(i => !bookedRoomIds.Contains(i.Id));

            return freeRooms;
        }

        public async Task<IEnumerable<Room>> GetByConditionAsync(RoomParameters roomParameters)
        {
            var availableRooms = await GetFreeAsync(roomParameters.StartBookingDate, roomParameters.EndBookingDate);

            return availableRooms.Where(i => i.Places >= roomParameters.MinPlaces && 
                                            (i.IsProjector == roomParameters.ProjectorAvailable || !roomParameters.UseProjectorParameter));
        }

        public async Task<Room> AddAsync(Room room) => await _roomRepository.AddAsync(room);
        public async Task<bool> UpdateAsync(Room room) => await _roomRepository.UpdateAsync(room);
        public async Task<bool> DeleteAsync(int id) => await _roomRepository.DeleteAsync(id);
        
    }
}
