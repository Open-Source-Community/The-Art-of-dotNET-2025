using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Task_Manager.ApiService.DTOs;
using Task_Manager.ApiService.Services;
using Task_Manager.Web.Data;

namespace Task_Manager.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private IUserService _userService; 
        //private readonly ITokenBlacklistService _tokenBlacklistService;
        private readonly IMapper _mapper;
        private static readonly HashSet<string> _tokenBlacklist = new();
        public AppUsersController(IUserService userService, /*ITokenBlacklistService tokenBlacklistService,*/IMapper mapper)
        {
            _userService = userService;
            //_tokenBlacklistService = tokenBlacklistService;
            _mapper = mapper;
        }

        [HttpPost] // C
        [Route("")]
        public async Task<ActionResult<int>> CreateUser(CreateUserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = await _userService.CreateUser(userDto);

            return Ok(userId);
        }

        [HttpPut] // U
        [Route("")]
        [Authorize]
        public async Task<ActionResult<int>> UpdateUser(UpdateUserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
             
            var result = await _userService.UpdateUser(userId, userDto);

            if (!result)
                return NotFound();

            return Ok();
        }

        /*
        [HttpDelete] // D
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<int>> DeleteUser()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
             
            var tokenString = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(tokenString))
            {
                return Unauthorized("Bearer token not found."); // This shouldn't happen as [Authorize] is used, but good to check.
            }

            // Extract JTI and expiration from the token
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokenString);

            var jti = jwtToken.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;
            var expiration = jwtToken.ValidTo; // This gets the actual expiration DateTime

            await _tokenBlacklistService.BlacklistTokenAsync(jti, expiration);
            
            var deleted = await _userService.DeleteUser(userId);

            if (!deleted)
                return NotFound();

            return Ok();
        }//*/

        [HttpPost]
        [Route("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            var tokenString = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(tokenString))
                return Unauthorized();

            // Simplified: Just store the raw token (not production-safe!)
            _tokenBlacklist.Add(tokenString);

            return Ok("Logged out (demo mode)");
        }

        [HttpGet]
        [Route("")]
        [Authorize(Roles = "AppAdmin")] // / GET
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet] // R
        [Route("{id}")] // /78 GET
        [Authorize(Roles = "AppAdmin")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userService.GetById(id);
            
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }



}
