using AutoMapper;
using BookingRooms.Models;
using SharedBookingLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomOutputDTO>();
            CreateMap<RoomInputDTO, Room>();
        }
    }
}
