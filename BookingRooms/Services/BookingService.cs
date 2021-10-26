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
        private IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IEnumerable<Booking> GetAllForPeriod(DateTime startDate, DateTime endDate) => _bookingRepository.GetAllForPeriod(startDate, endDate);
        public Booking Get(int id) => _bookingRepository.Get(id);
        public Booking Add(Booking booking) => _bookingRepository.Add(booking);
        public Booking Update(Booking booking) => _bookingRepository.Update(booking);
    }
}
