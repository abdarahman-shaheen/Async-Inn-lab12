﻿using Async_Inn.Data;
using Async_Inn.Model.Interface;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Model.Services
{
    public class HotelRoomServices : IHotelRoom
    {
        private readonly HotelDbContext _context;

        public HotelRoomServices(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<HotelRoom> Create( HotelRoom HotelRoom)
        {

            
            HotelRoom HotelRooms = new HotelRoom()
            {
                HotelId = HotelRoom.HotelId,
                Rate = HotelRoom.Rate,
                RoomNumber = HotelRoom.RoomNumber,
                Room = await _context.Rooms.Where(x=>x.Id==HotelRoom.RoomId).FirstOrDefaultAsync(),
                Hotel = await _context.Hotels.Where(x => x.Id == HotelRoom.RoomId).FirstOrDefaultAsync()
        };
         // HotelRoom.Hotel = await _context.Hotels.Where(x => x.Id == HotelRoom.HotelId).FirstOrDefaultAsync();
          //HotelRoom.Room = await _context.Rooms.Where(x => x.Id == HotelRoom.RoomId).FirstOrDefaultAsync();
            //  _context.Entry(HotelRooms).State= EntityState.Added;
             _context.HotelRooms.Add(HotelRoom);
            await _context.SaveChangesAsync();
            return HotelRooms;
        }

        public async Task<HotelRoom> Delete(int idHotel, int idRoom)
        {
            var HotelRoom = await GetHotelRoomId(idHotel , idRoom);
            _context.HotelRooms.Remove(HotelRoom);
            await _context.SaveChangesAsync();
            return HotelRoom;
        }
        public async Task<HotelRoom> GetHotelRoomId(int idHotel,int idRoom)
        {
            var HotelRoom =await _context.HotelRooms.Where(x => x.HotelId == idHotel && x.RoomNumber == idRoom).FirstOrDefaultAsync();
            return HotelRoom;
        }

        public async Task<List<HotelRoom>> GetHotelRooms()
        {
            return await _context.HotelRooms.Include(x => x.Room).ThenInclude(D=>D.RoomAmeneties).Include(r=>r.Hotel).ToListAsync();

        }

        public async Task<HotelRoom> Update(int idHotel, int idRoom, HotelRoom HotelRoom)
        {
            var Temproom = await GetHotelRoomId(idHotel,idRoom);
            Temproom.RoomNumber=HotelRoom.RoomNumber;
            Temproom.Rate = HotelRoom.Rate;
            Temproom.PetFriendly = HotelRoom.PetFriendly;
            _context.Entry(Temproom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Temproom;

        }
    }
}
