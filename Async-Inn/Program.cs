using Async_Inn.Data;
using Async_Inn.Model;
using Async_Inn.Model.Interface;
using Async_Inn.Model.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddNewtonsoftJson(
                option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );




            string connString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<HotelDbContext>(options => options.UseSqlServer(connString));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<HotelDbContext>();


            builder.Services.AddTransient<IHotel, HotelServices>();
            builder.Services.AddTransient<IRoom, RoomServices>();
            builder.Services.AddTransient<IAmenities, AminitiesServices>();
            builder.Services.AddTransient<IHotelRoom, HotelRoomServices>();
            builder.Services.AddTransient<IUser,IdentityUserServices>();
            builder.Services.AddScoped<JWTTokenServices>();

            builder.Services.AddAuthentication
                (options => { options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = JWTTokenServices.GetValidationParameters(builder.Configuration);
                });
            builder.Services.AddAuthorization(option =>
            {
                option.AddPolicy("Create", policy => policy.RequireClaim("Permisstion", "Create"));
                option.AddPolicy("Update", policy => policy.RequireClaim("Permisstion", "Update"));
            }

            );

            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Hotel APi",
                    Version="v1"

                });
            });
           



            var app = builder.Build();
            app.UseSwagger(option =>
            {
                option.RouteTemplate = "/api/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(option => { 
                option.SwaggerEndpoint("/api/v1/swagger.json", "School API");
                option.RoutePrefix = "docs";
            });

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}