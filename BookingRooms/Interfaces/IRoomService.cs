﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;

namespace BookingRooms.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<Room> GetAll();
        Room Get(int id);
        Room Add(Room room);
        Room Update(Room room);
        Room Delete(int id);
        IEnumerable<Room> GetFree(DateTime startDate, DateTime endDate);

    }
}
