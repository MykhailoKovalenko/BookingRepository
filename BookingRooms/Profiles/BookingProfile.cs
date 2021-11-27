using AutoMapper;
using BookingRooms.Models;
using SharedBookingLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingOutputDTO>();
            CreateMap<BookingInputDTO, Booking>();
        }
    }
}
