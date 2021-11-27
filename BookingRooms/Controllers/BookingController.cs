using AutoMapper;
using BookingRooms.Interfaces;
using BookingRooms.Models;
using Microsoft.AspNetCore.Mvc;
using SharedBookingLibrary.DTO;
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
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        public BookingController(IBookingService bookingService, IRoomService roomService, IMapper mapper)
        {
            _bookingService = bookingService;
            _roomService = roomService;
            _mapper = mapper;
        }

    #region getForPeriod

        [HttpGet("forPeriod")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookingOutputDTO>))]
        public async Task<ActionResult<IEnumerable<BookingOutputDTO>>> GetAllForPeriod([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            return Ok(_mapper.Map<IEnumerable<BookingOutputDTO>>(await _bookingService.GetAllForPeriodAsync(start, end)));
        }
        
    #endregion

    #region get

        [HttpGet("{id}", Name = nameof(GetBooking))]
        [ProducesResponseType(200, Type = typeof(BookingOutputDTO))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BookingOutputDTO>> GetBooking(int id)
        {
            var booking = await _bookingService.GetAsync(id);

            if (booking == null)
                return NotFound();

            return Ok(_mapper.Map<BookingOutputDTO>(booking));
        }

        #endregion

    #region create

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BookingOutputDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<ActionResult<BookingOutputDTO>> Create([FromBody] BookingInputDTO bookingInputDTO)
        {
            Booking booking = _mapper.Map<Booking>(bookingInputDTO);

            if (booking == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //if(!_bookingService.Validate(out string message))

            //var freeRoomIds = _roomService.GetFree(booking.Start, booking.End).Select(i => i.Id);
            //if (!freeRoomIds.Contains(booking.RoomId))
            //    return UnprocessableEntity(new { error = "Sorry! The room is occupied at this time.", roomId = booking.RoomId });

            await _bookingService.AddAsync(booking);

            BookingOutputDTO bookingOutputDTO = _mapper.Map<BookingOutputDTO>(booking);

            return CreatedAtAction(nameof(Create), new { id = bookingOutputDTO.Id }, bookingOutputDTO);
        }

    #endregion

    #region update

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(int id, [FromBody] BookingInputDTO bookingInputDTO)
        {
            Booking booking = _mapper.Map<Booking>(bookingInputDTO);

            if (booking == null || id != booking.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBooking = await _bookingService.GetAsync(id);
            if (existingBooking == null)
                return NotFound();

            var result = await _bookingService.UpdateAsync(booking);

            //if(!result)
            //    return 

            return NoContent();
        }

        #endregion

    #region delete

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var booking = await _bookingService.GetAsync(id);

            if (booking == null)
                return NotFound();

            var result = await _bookingService.DeleteAsync(id);

            return NoContent();
        }

    #endregion
    }
}
