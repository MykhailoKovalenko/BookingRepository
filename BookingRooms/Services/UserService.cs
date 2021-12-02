using BookingRooms.DataAccessLayer.Repository;
using BookingRooms.Interfaces;
using BookingRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
      
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _userRepository.GetAllAsync();
        public async Task<User> GetAsync(int id) => await _userRepository.GetAsync(id);
        public async Task<User> GetByNameAsync(string name) => await _userRepository.GetByNameAsync(name);
        public async Task<User> AddAsync(User user) => await _userRepository.AddAsync(user);
        public async Task<bool> UpdateAsync(User user) => await _userRepository.UpdateAsync(user);
        public async Task<bool> DeleteAsync(int id) => await _userRepository.DeleteAsync(id);
    }
}
