using BookingRooms.DBContext;
using BookingRooms.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.DataAccessLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BRoomsContext _context;
        public UserRepository(BRoomsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                        .AsQueryable()
                        .ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users
                    .Include(i => i.Bookings)
                    .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);

            await SaveChangesAsync();

            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            User existingUser = await GetAsync(user.Id);

            existingUser.Name = user.Name;

            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            User user = await GetAsync(id);

            _context.Users.Remove(user);

            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }       
    }
}
