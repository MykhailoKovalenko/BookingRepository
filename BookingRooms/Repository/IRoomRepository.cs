using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;

namespace BookingRooms.Repository
{
    interface IRoomRepository
    {
        List<Room> GetAll();
        Room Get(int id);
    }
}
