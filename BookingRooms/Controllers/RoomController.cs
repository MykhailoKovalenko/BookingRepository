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

        [HttpGet]
        public ActionResult<List<Room>> GetAll() => _roomService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Room> Get(int id)
        {
            var room = _roomService.Get(id);

            if (room == null)
                return NotFound(); 
            return room;
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            _roomService.Add(room);
            return CreatedAtAction(nameof(Create), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Room room)
        {
            if (id != room.Id)
                return BadRequest();

            var existingRoom = _roomService.Get(id);
            if (existingRoom == null)
                return NotFound();

            _roomService.Update(room);

            return NoContent();
        }

        [HttpDelete("{id}")]
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
