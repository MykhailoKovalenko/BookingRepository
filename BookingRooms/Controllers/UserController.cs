using AutoMapper;
using BookingRooms.Interfaces;
using BookingRooms.Models;
using BookingRooms.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        #region getAll

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserOutputDTO>))]
        public async Task<ActionResult<IEnumerable<UserOutputDTO>>> GetAll()
                            => Ok(_mapper.Map<IEnumerable<UserOutputDTO>>(await _userService.GetAllAsync()));

        #endregion

        #region get

        [HttpGet("{id}", Name = nameof(GetUser))]
        [ProducesResponseType(200, Type = typeof(UserOutputDTO))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserOutputDTO>> GetUser(int id)
        {
            var user = await _userService.GetAsync(id);

            if (user == null)
                return NotFound();

            return Ok(_mapper.Map<UserOutputDTO>(user));
        }

        #endregion

        #region create
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserOutputDTO))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserOutputDTO>> Create([FromBody] UserInputDTO userInputDTO)
        {
            User user = _mapper.Map<User>(userInputDTO);

            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userService.AddAsync(user);

            UserOutputDTO userOutputDTO = _mapper.Map<UserOutputDTO>(user);

            return CreatedAtAction(nameof(Create), new { id = userOutputDTO.Id }, userOutputDTO);
        }

        #endregion

        #region update

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(int id, [FromBody] UserInputDTO userInputDTO)
        {
            User user = _mapper.Map<User>(userInputDTO);

            if (user == null || id != user.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _userService.GetAsync(id);
            if (existingUser == null)
                return NotFound();

            var result = await _userService.UpdateAsync(user);

            return NoContent();
        }

        #endregion

        #region delete

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userService.GetAsync(id);

            if (user == null)
                return NotFound();

            var result = await _userService.DeleteAsync(id);

            return NoContent();
        }

        #endregion
         
    }
}
