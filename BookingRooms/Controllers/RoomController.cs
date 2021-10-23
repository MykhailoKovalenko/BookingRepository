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
        private IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var controller = RouteData.Values["controller"].ToString();
            var action = RouteData.Values["action"].ToString();
            return Content($"controller: {controller} | action: {action}");
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Room>))]
        public ActionResult<IEnumerable<Room>> GetAll() => _roomService.GetAll().ToList();

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Room))]
        [ProducesResponseType(404)]
        public ActionResult<Room> Get(int id)
        {
            var room = _roomService.Get(id);

            if (room == null)
                return NotFound(); 
            return Ok(room);
        }


        [HttpGet("GetFree")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Room>))]
        public ActionResult<IEnumerable<Room>> GetFree([FromQuery] DateTime start, [FromQuery] DateTime end) => _roomService.GetFree(start, end).ToList();

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Room))]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] Room room)
        {
            if (room == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _roomService.Add(room);
            return CreatedAtAction(nameof(Create), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(int id, [FromBody] Room room)
        {
            if (room == null || id != room.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingRoom = _roomService.Get(id);
            if (existingRoom == null)
                return NotFound();

            _roomService.Update(room);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            var room = _roomService.Get(id);

            if (room == null)
                return NotFound();

            _roomService.Delete(id);

            return NoContent();
        }
    }
}
