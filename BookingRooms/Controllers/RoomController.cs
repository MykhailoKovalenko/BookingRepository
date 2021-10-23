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
        public ActionResult<IEnumerable<Room>> GetAll() => _roomService.GetAll().ToList();

        [HttpGet("{id}")]
        public ActionResult<Room> Get(int id)
        {
            var room = _roomService.Get(id);

            if (room == null)
                return NotFound(); 
            return Ok(room);
        }

      
        [HttpGet("GetFree")]
        public ActionResult<IEnumerable<Room>> GetFree(DateTime start, DateTime end) => _roomService.GetFree(start, end).ToList();

        [HttpPost]
        public IActionResult Create(Room room)
        {
            if (room == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
