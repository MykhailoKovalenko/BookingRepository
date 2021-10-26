using BookingRooms.Interfaces;
using BookingRooms.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Booking))]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] Booking booking)
        {
            if (booking == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _bookingService.Add(booking);
            return CreatedAtAction(nameof(Create), new { id = booking.Id }, booking);
        }

    }
}
