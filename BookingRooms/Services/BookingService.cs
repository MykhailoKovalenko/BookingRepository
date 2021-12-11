using BookingRooms.DataAccessLayer.Repository;
using BookingRooms.Interfaces;
using BookingRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<Booking>> GetAllForPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _bookingRepository.GetAllForPeriodAsync(startDate, endDate);
        }

        public async Task<Booking> GetAsync(int id) => await _bookingRepository.GetAsync(id);

        public async Task<Booking> AddAsync(Booking booking) => await _bookingRepository.AddAsync(booking);

        public async Task<bool> ChangeRoomAsync(int id, int roomId) => await _bookingRepository.ChangeRoomAsync(id, roomId);

        public async Task<bool> UpdateAsync(Booking booking) => await _bookingRepository.UpdateAsync(booking);

        public async Task<bool> DeleteAsync(int id) => await _bookingRepository.DeleteAsync(id);
    }
}
