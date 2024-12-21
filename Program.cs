using Microsoft.EntityFrameworkCore;
using TrainTicketReservationSystem.Models;

namespace TrainTicketReservationSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Ensure you have the correct namespace for your DbContext and correct connection string name
            builder.Services.AddDbContext<MyNewContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("constr")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Development environment setup
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();  // Enable HTTP Strict Transport Security for production
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=GetAllUser}/{id?}");

            app.Run();
        }
    }
}
