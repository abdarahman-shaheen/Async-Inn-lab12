using Async_Inn.Data;
using Async_Inn.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsTest
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;

        protected readonly HotelDbContext _db;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new HotelDbContext(

                new DbContextOptionsBuilder<HotelDbContext>()
                .UseSqlite(_connection).Options);

            _db.Database.EnsureCreated();
        }
        protected async Task<Room> CreateAndSaveRoom()
        {

            var room = new Room()
            {
                Name = "TestRooms",
                Layout=88
            };

            _db.Rooms.Add(room);

            await _db.SaveChangesAsync();

            return room;
            // Assert.NotEqual(0,room.Id);


        }

        protected async Task<Hotel> CreateAndHotel()
        {

            var hotel = new Hotel()
            {

                Name = "TestHotel",
                City = "Zarqa",
                StreetAddres = "Vs",
                Phone = "0791855139",
                Country = "Jordan",
                State = "avalablie"
            };
            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();

            // Assert.NotEqual(0,hotel.Id);


            return hotel;


        }
        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
    }
