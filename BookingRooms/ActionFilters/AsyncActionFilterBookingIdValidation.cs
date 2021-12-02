using BookingRooms.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.ActionFilters
{
    public class AsyncActionFilterBookingIdValidation : IAsyncActionFilter
    {
        private readonly IBookingService _bookingService;
        public AsyncActionFilterBookingIdValidation(IBookingService bookingService)
        {
            _bookingService = bookingService;
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

            var existingBooking = await _bookingService.GetAsync(id);
            if (existingBooking == null)
            {
                context.Result = new NotFoundObjectResult($"Booking with id = {id} not found");
                return false;
            }

            return true;
        }
    }
}
