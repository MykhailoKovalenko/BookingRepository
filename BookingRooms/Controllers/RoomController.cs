using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using BookingRooms.Models;
using BookingRooms.Services;
using BookingRooms.Interfaces;

namespace BookingRooms.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

    #region index

        [HttpGet("index")]
        public IActionResult Index()
        {
            var controller = RouteData.Values["controller"].ToString();
            var action = RouteData.Values["action"].ToString();
            return Content($"controller: {controller} | action: {action}");
        }

    #endregion

    #region getAll

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Room>))]
        public async Task<ActionResult<IEnumerable<Room>>> GetAll() => Ok(await _roomService.GetAllAsync());

    #endregion

    #region get

        [HttpGet("{id}", Name = nameof(GetRoom))]
        [ProducesResponseType(200, Type = typeof(Room))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Room>> GetRoom(int id) 
        {
            var room = await _roomService.GetAsync(id);

            if (room == null)
                return NotFound(); 

            return Ok(room);
        }

    #endregion

    #region getFree

        [HttpGet("free")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Room>))]
        public async Task<ActionResult<IEnumerable<Room>>> GetFree([FromQuery] DateTime start, [FromQuery] DateTime end) => Ok(await _roomService.GetFreeAsync(start, end));

    #endregion

    #region create
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Room))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Room>> Create([FromBody] Room room)
        {
            if (room == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _roomService.AddAsync(room);

            return CreatedAtAction(nameof(Create), new { id = room.Id }, room);
        }

    #endregion

    #region update

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(int id, [FromBody] Room room)
        {
            if (room == null || id != room.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingRoom = await _roomService.GetAsync(id);
            if (existingRoom == null)
                return NotFound();

            var result = await _roomService.UpdateAsync(room);

            return NoContent();
        }

    #endregion

    #region delete

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var room = await _roomService.GetAsync(id);

            if (room == null)
                return NotFound();

            var result = await _roomService.DeleteAsync(id);

            return NoContent();
        }

    #endregion

    }
}
