using BookingRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingRooms.DBContext;

namespace BookingRooms.DataAccessLayer.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly BRoomsContext _context;
        public RoomRepository(BRoomsContext context)
        {
            _context = context;
        }

        public async IAsyncEnumerable<Room> GetAllAsync()
        {
            var enumerator = _context.Rooms.AsAsyncEnumerable().GetAsyncEnumerator();

            while (await enumerator.MoveNextAsync())
            {
                yield return enumerator.Current;
            }
        }

        public Task<Room> GetAsync(int id)
        {
            return _context.Rooms
                    .Include(i => i.Bookings)
                    .FirstOrDefaultAsync(i => i.Id == id);
        }

        public Room Add(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();

            return room;
        }

        public Room Update(Room room)
        {
            Room existingRoom = _context.Rooms.Find(room.Id);

            existingRoom.Name = room.Name;
            existingRoom.Places = room.Places;
            existingRoom.IsProjector = room.IsProjector;

            //context.Update(room);

            _context.SaveChanges();

            return existingRoom;
        }

        public Room Delete(int id)
        {
            Room existingRoom = _context.Rooms.Find(id);

            _context.Rooms.Remove(existingRoom);
            _context.SaveChanges();

            return existingRoom;
        }
    }
}
