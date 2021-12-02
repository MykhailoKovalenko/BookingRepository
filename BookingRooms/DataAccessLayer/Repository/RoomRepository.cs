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

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms
                        .AsQueryable()
                        .ToListAsync();
        }

        public async Task<Room> GetAsync(int id)
        {
            return await _context.Rooms
                    .Include(i => i.Bookings)
                    .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Room> GetByNameAsync(string name)
        {
            return await _context.Rooms
                    .Include(i => i.Bookings)
                    .FirstOrDefaultAsync(i => i.Name == name);
        }

        public async Task<Room> AddAsync(Room room)
        {
            _context.Rooms.Add(room);

            await SaveChangesAsync();

            return room;
        }

        public async Task<bool> UpdateAsync(Room room)
        {
            Room existingRoom = await GetAsync(room.Id);

            existingRoom.Name = room.Name;
            existingRoom.Places = room.Places;
            existingRoom.IsProjector = room.IsProjector;

            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Room room = await GetAsync(id);

            _context.Rooms.Remove(room);

            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
