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
    public class AsyncActionFilterUserValidation : IAsyncActionFilter
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AsyncActionFilterUserValidation(IUserService userService, IMapper mapper)
        {
            _userService = userService;
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

            if (context.ActionArguments.ContainsKey("userInputDTO"))
            {
                UserInputDTO userInputDTO = (UserInputDTO)context.ActionArguments["userInputDTO"];

                User user = _mapper.Map<User>(userInputDTO);

                if (user == null)
                {
                    context.Result = new BadRequestObjectResult("Input data is null");
                    return false;
                }

                if (!context.ModelState.IsValid)
                {
                    context.Result = new BadRequestObjectResult(context.ModelState);
                    return false;
                }

                var existingUser = await _userService.GetByNameAsync(user.Name);
                if (existingUser != null)
                {
                    context.Result = new UnprocessableEntityObjectResult(new { errors = new Dictionary<string, string>()
                                                                                        { { "Name", $"Name {user.Name} is not unique. Choose another name" } }
                    });
                    return false;
                }

                context.HttpContext.Items.Add("user", user);
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
