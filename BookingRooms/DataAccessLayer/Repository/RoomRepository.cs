using BookingRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingRooms.DBContext;

namespace BookingRooms.DataAccessLayer.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly BRoomsContext _context;
        public RoomRepository(BRoomsContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetAll()
        {
            IQueryable<Room> query = _context.Rooms; // Set<Room>();

            return query.AsEnumerable();
            
            // alternative variant, check!
            //IEnumerable<Room> rooms = _context.Rooms.ToList().AsEnumerable();
            //return rooms;
        }

        public Room Get(int id)
        {
            Room room = _context.Rooms.Find(id);

            return room;
        }

        public Room Add(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();

            return room;
        }

        public Room Update(Room room)
        {
            Room existingRoom = _context.Rooms.Find(room.Id);

            existingRoom.Name = room.Name;
            existingRoom.Places = room.Places;

            //context.Update(room);

            _context.SaveChanges();

            return existingRoom;
        }

        public Room Delete(int id)
        {
            Room existingRoom = _context.Rooms.Find(id);

            _context.Rooms.Remove(existingRoom);
            _context.SaveChanges();

            return existingRoom;
        }
    }
}
