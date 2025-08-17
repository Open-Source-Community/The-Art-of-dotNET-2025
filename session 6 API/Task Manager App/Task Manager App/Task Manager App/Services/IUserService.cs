using Microsoft.AspNetCore.Mvc;
using Task_Manager.ApiService.DTOs;
using Task_Manager.Web.Data;

namespace Task_Manager.ApiService.Services
{
    public interface IUserService
    {
        public Task<int> CreateUser(CreateUserDto user);
        public Task<bool> UpdateUser(int userId, UpdateUserDto user);
        public Task<bool> DeleteUser(int userId);
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User?> GetById(int userId);
    }
}
