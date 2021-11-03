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
        private IRoomService _roomService;
        public BookingController(IBookingService bookingService, IRoomService roomService)
        {
            _bookingService = bookingService;
            _roomService = roomService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Booking))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Booking>> Get(int id)
        {
            var booking = await _bookingService.GetAsync(id);

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Booking))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public IActionResult Create([FromBody] Booking booking)
        {
            if (booking == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var freeRoomIds = _roomService.GetFree(booking.Start, booking.End).Select(i => i.Id);
            //if (!freeRoomIds.Contains(booking.RoomId))
            //    return UnprocessableEntity(new { error = "Sorry! The room is occupied at this time.", roomId = booking.RoomId });

            _bookingService.Add(booking);
            return CreatedAtAction(nameof(Create), new { id = booking.Id }, booking);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(int id, [FromBody] Booking booking)
        {
            if (booking == null || id != booking.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBooking = _bookingService.GetAsync(id);
            if (existingBooking == null)
                return NotFound();

            _bookingService.Update(booking);

            return NoContent();
        }

    }
}
