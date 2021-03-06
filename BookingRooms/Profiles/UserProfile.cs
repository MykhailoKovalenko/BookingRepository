using AutoMapper;
using BookingRooms.Models;
using SharedBookingLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserOutputDTO>();
            CreateMap<UserInputDTO, User>();
        }
    }
}
