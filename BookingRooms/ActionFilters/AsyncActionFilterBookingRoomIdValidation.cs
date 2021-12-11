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
    public class AsyncActionFilterBookingRoomIdValidation : IAsyncActionFilter
    {
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        public AsyncActionFilterBookingRoomIdValidation(IRoomService roomService, IBookingService bookingService)
        {
            _roomService = roomService;
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
            if (!context.ActionArguments.ContainsKey("roomId"))
            {
                context.Result = new BadRequestObjectResult("Parameter \"roomId\" is missing");
                return false;
            }

            int roomId = (int)context.ActionArguments["roomId"];

            var existingRoom = await _roomService.GetAsync(roomId);
            if (existingRoom == null)
            {
                context.Result = new NotFoundObjectResult($"Room with id = {roomId} not found");
                return false;
            }

            int bookingId = (int)context.ActionArguments["id"];

            var existingBooking = await _bookingService.GetAsync(bookingId);
            if (existingBooking == null)
            {
                context.Result = new NotFoundObjectResult($"Booking with id = {bookingId} not found");
                return false;
            }

            var freeRoomIds = (await _roomService.GetFreeAsync(existingBooking.Start, existingBooking.End)).Select(i => i.Id);
            if (!freeRoomIds.Contains(roomId))
            {
                context.Result = new UnprocessableEntityObjectResult(new
                {
                    errors = new Dictionary<string, string>()
                                                                                                { { "error", "Sorry! The room is occupied at this time." },
                                                                                                  { "RoomId", roomId.ToString() } }
                });

                return false;
            }

            return true;
        }
    }
}
