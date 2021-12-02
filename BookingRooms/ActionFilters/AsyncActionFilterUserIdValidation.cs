using BookingRooms.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.ActionFilters
{
    public class AsyncActionFilterUserIdValidation : IAsyncActionFilter
    {
        private readonly IUserService _userService;
        public AsyncActionFilterUserIdValidation(IUserService userService)
        {
            _userService = userService;
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

            var existingUser = await _userService.GetAsync(id);
            if (existingUser == null)
            {
                context.Result = new NotFoundObjectResult($"User with id = {id} not found");
                return false;
            }

            return true;
        }
    }
}
