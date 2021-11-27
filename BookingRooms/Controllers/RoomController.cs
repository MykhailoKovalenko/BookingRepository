using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using BookingRooms.Models;
using BookingRooms.Services;
using BookingRooms.Interfaces;
using AutoMapper;
using SharedBookingLibrary.DTO;

namespace BookingRooms.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        public RoomController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<RoomOutputDTO>))]
        public async Task<ActionResult<IEnumerable<RoomOutputDTO>>> GetAll() 
                            => Ok(_mapper.Map<IEnumerable<RoomOutputDTO>>(await _roomService.GetAllAsync()));

    #endregion

    #region get

        [HttpGet("{id}", Name = nameof(GetRoom))]
        [ProducesResponseType(200, Type = typeof(RoomOutputDTO))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RoomOutputDTO>> GetRoom(int id) 
        {
            var room = await _roomService.GetAsync(id);

            if (room == null)
                return NotFound(); 

            return Ok(_mapper.Map<RoomOutputDTO>(room));
        }

        #endregion

    #region getFree

        [HttpGet("free")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RoomOutputDTO>))]
        public async Task<ActionResult<IEnumerable<RoomOutputDTO>>> GetFree([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            return Ok(_mapper.Map<RoomOutputDTO>(await _roomService.GetFreeAsync(start, end)));
        }

    #endregion

    #region create
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(RoomOutputDTO))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RoomOutputDTO>> Create([FromBody] RoomInputDTO roomInputDTO)
        {
            Room room = _mapper.Map<Room>(roomInputDTO);

            if (room == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _roomService.AddAsync(room);

            RoomOutputDTO roomOutputDTO = _mapper.Map<RoomOutputDTO>(room);

            return CreatedAtAction(nameof(Create), new { id = roomOutputDTO.Id }, roomOutputDTO);
        }

    #endregion

    #region update

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(int id, [FromBody] RoomInputDTO roomInputDTO)
        {
            Room room = _mapper.Map<Room>(roomInputDTO);

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
