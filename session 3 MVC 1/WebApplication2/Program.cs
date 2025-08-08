using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            #region

            builder.Services.AddSession(conf =>
              conf.IdleTimeout = TimeSpan.FromHours(1));

            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error"); //use //map//run
            }

            app.UseStaticFiles();

            app.UseRouting(); 

            #region
            app.UseSession();
            #endregion
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{action=Index}/{controller=Home}/{id?}");

            app.Run();




        }
    }
}
