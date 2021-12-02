using AutoMapper;
using BookingRooms.Interfaces;
using BookingRooms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedBookingLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.ActionFilters
{
    public class AsyncActionFilterRoomValidation : IAsyncActionFilter
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        public AsyncActionFilterRoomValidation(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // execute any code before the action executes
            bool executionResult = await OnActionExecutionAsyncBefore(context);

            if (executionResult)
            {
                var result = await next();
            }

            // execute any code after the action executes
        }

        private async Task<bool> OnActionExecutionAsyncBefore(ActionExecutingContext context)
        {

            if (context.ActionArguments.ContainsKey("roomInputDTO"))
            {
                RoomInputDTO roomInputDTO = (RoomInputDTO)context.ActionArguments["roomInputDTO"];

                Room room = _mapper.Map<Room>(roomInputDTO);

                if (room == null)
                {
                    context.Result = new BadRequestObjectResult("Input data is null");
                    return false;
                }
                
                if (!context.ModelState.IsValid)
                {
                    context.Result = new BadRequestObjectResult(context.ModelState);
                    return false;
                }

                var existingRoom = await _roomService.GetByNameAsync(room.Name);
                if (existingRoom != null)
                {
                    context.Result = new UnprocessableEntityObjectResult(new { errors = new Dictionary<string, string>()
                                                                                        { { "Name", $"Name {room.Name} is not unique. Choose another name" } }
                    });
                    return false;
                }

                context.HttpContext.Items.Add("room", room);
            }
            else
            {
                context.Result = new BadRequestObjectResult("Invalid input data");
                return false;
            }

            return true;
        }
    }   
}
