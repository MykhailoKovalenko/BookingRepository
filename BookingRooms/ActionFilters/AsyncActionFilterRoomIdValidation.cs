using BookingRooms.Interfaces;
using BookingRooms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.ActionFilters
{
    public class AsyncActionFilterRoomIdValidation : ActionFilterAttribute, IAsyncActionFilter
    {
        private readonly IRoomService _roomService;
        public AsyncActionFilterRoomIdValidation(IRoomService roomService)
        {
            _roomService = roomService;
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
            if (!context.ActionArguments.ContainsKey("id"))
            {
                context.Result = new BadRequestObjectResult("Parameter \"id\" is missing");
                return false;
            }

            int id = (int)context.ActionArguments["id"];

            var existingRoom = await _roomService.GetAsync(id);
            if (existingRoom == null)
            {
                context.Result = new NotFoundObjectResult($"Room with id = {id} not found");
                return false;
            }

            return true;
        }
    }
}
