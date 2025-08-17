using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using Task_Manager.ApiService;
using Task_Manager.ApiService.Services;
using Task_Manager.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITodoTaskService, TodoTaskService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext> // Scoped
(cfg => cfg.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]));
builder.Services.AddEndpointsApiExplorer();


var redisConnection = builder.Configuration.GetValue<string>("Redis:Connection");
//builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));
//builder.Services.AddScoped(s => s.GetRequiredService<IConnectionMultiplexer>().GetDatabase()); // For IDatabase
//builder.Services.AddScoped<ITokenBlacklistService, TokenBlacklistService>();

// JWT Bearer Token Authentication 
var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
builder.Services.AddSingleton(jwtOptions);
builder.Services.AddAuthentication().
    AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.SaveToken = true; // to keep the token string after getting the info so it can be accessed using HttpContext

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,

            ValidateIssuerSigningKey = true, // the most important validation parameter
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
        };

        /*options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var tokenBlacklistService = context.HttpContext?.RequestServices?.GetRequiredService<ITokenBlacklistService>();
                if (tokenBlacklistService == null)
                {
                    context.Fail("Token Blacklist Service not available."); // Should not happen in configured app
                    return;
                }

                var tokenString = context.SecurityToken as JwtSecurityToken;

                var jti = tokenString?.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;

                if (await tokenBlacklistService.IsTokenBlacklistedAsync(jti))
                {
                    context.Fail("This token (JTI) has been blacklisted.");
                    return;
                }
            },
        }; */
    });

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.EnableAnnotations();
});


var app = builder.Build();

app.UseExceptionHandler();
  
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();