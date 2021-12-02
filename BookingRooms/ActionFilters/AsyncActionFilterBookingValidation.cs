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
    public class AsyncActionFilterBookingValidation : IAsyncActionFilter
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        public AsyncActionFilterBookingValidation(IBookingService bookingService, IRoomService roomService, IMapper mapper)
        {
            _bookingService = bookingService;
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

            if (context.ActionArguments.ContainsKey("bookingInputDTO"))
            {
                BookingInputDTO bookingInputDTO = (BookingInputDTO) context.ActionArguments["bookingInputDTO"];

                Booking booking = _mapper.Map<Booking>(bookingInputDTO);

                if (booking == null)
                {
                    context.Result = new BadRequestObjectResult("Input data is null");
                    return false;
                }

                if (!context.ModelState.IsValid)
                {
                    context.Result = new BadRequestObjectResult(context.ModelState);
                    return false;
                }

                var freeRoomIds = (await _roomService.GetFreeAsync(booking.Start, booking.End)).Select(i => i.Id);
                if (!freeRoomIds.Contains(booking.RoomId))
                {
                    context.Result = new UnprocessableEntityObjectResult(new { errors = new Dictionary<string, string>()
                                                                                                { { "error", "Sorry! The room is occupied at this time." },
                                                                                                  { "RoomId", booking.RoomId.ToString() } }
                            });

                        
                    return false;
                }

                context.HttpContext.Items.Add("booking", booking);
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
