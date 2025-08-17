using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task_Manager.ApiService;
using Task_Manager.ApiService.Data;
using Task_Manager.Web.Data;

namespace LearningASP_NET_Core_API.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController(JwtOptions jwtOptions, AppDbContext dbContext) : ControllerBase
{
    [HttpPut]
    [Route("auth")]
    [AllowAnonymous] // ← Critical for auth endpoints
    // usually return object contains the token and some other details // return string for simplicity
    public async Task<ActionResult<string>> AuthenticateUser(AuthenticationRequest request)
    {
        var user = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Name == request.UserName);

        if (user == null) 
            return Unauthorized("Invalid credentials.");

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
        if (!isPasswordValid)
            return Unauthorized("Invalid credentials.");

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email),
            new Claim("jti", Guid.NewGuid().ToString())
        };

        if (user.IsAdmin)
        {
            claims.Add(new Claim(ClaimTypes.Role, "AppAdmin"));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = jwtOptions.Issuer,  
            Audience = jwtOptions.Audience, 

            Expires = DateTime.UtcNow.AddMinutes(jwtOptions.Lifetime),

            SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
            SecurityAlgorithms.HmacSha256), 

            Subject = new ClaimsIdentity(claims) // Use the dynamically built claims list  
        };

        var tokenHandler = new JwtSecurityTokenHandler();  

        var securityToken = tokenHandler.CreateToken(tokenDescriptor); // object
         
        var accessToken = tokenHandler.WriteToken(securityToken); // string

        return Ok(accessToken);
    }
}

