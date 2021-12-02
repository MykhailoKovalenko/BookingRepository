using BookingRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(int id);
        Task<User> GetByNameAsync(string name);
        Task<User> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
    }
}
