using Async_Inn.Data;
using Async_Inn.Model;
using Async_Inn.Model.Interface;
using Async_Inn.Model.Services;

using Microsoft.EntityFrameworkCore;

namespace Async_Inn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          
            string connString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<HotelDbContext>(options => options.UseSqlServer(connString));
            builder.Services.AddControllers();
            builder.Services.AddTransient<IHotel, HotelServices>();
            builder.Services.AddTransient<IRoom, RoomServices>();
            builder.Services.AddTransient<IAmenities, AminitiesServices>();


            var app = builder.Build();
          
            app.MapControllers();
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}