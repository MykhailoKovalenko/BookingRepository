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
    }
}
