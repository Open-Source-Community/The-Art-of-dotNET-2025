using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Task_Manager.ApiService.DTOs;
using Task_Manager.Web.Data;

namespace Task_Manager.ApiService.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public UserService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        async Task<int> IUserService.CreateUser(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();

            return user.Id;
        }
         
        async Task<bool> IUserService.UpdateUser(int userId, UpdateUserDto userDto)
        {
            var destination = await _appDbContext.Users.FindAsync(userId);

            if (destination == null)
                return false;

            if (destination.Email != userDto.Email)
            {
                var collision = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);
                if (collision != null)
                    return false;
            }

            _mapper.Map(userDto, destination);

            _appDbContext.Users.Update(destination);

            await _appDbContext.SaveChangesAsync();

            return true;
        }

        async Task<bool> IUserService.DeleteUser(int userId)
        {
            var destination = await _appDbContext.Users.FindAsync(userId);

            if (destination == null)
                return false;

            _appDbContext.Users.Remove(destination);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        async Task<IEnumerable<User>> IUserService.GetAllUsers()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        async Task<User?> IUserService.GetById(int userId)
        {
            var record = await _appDbContext.Users.FindAsync(userId);

            return record;
        }
    }
}
