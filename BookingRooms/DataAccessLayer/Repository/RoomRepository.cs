using BookingRooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;



namespace BookingRooms.Repository
{
    public class RoomRepository : IRoomRepository
    {
        public List<Room> GetAll()
        {
            using (DBContext.BRoomsContext context = new DBContext.BRoomsContext())
            {
                List<Room> rooms = context.Rooms.ToList();          
               
                return rooms;
            }
        }
        public Room Get(int id)
        {
            using (DBContext.BRoomsContext context = new DBContext.BRoomsContext())
            {
                Room room = context.Rooms.Find(id);

                return room;
            }
        }

        public Room Add(Room room)
        {
            using (DBContext.BRoomsContext context = new DBContext.BRoomsContext())
            {
                context.Rooms.Add(room);
                context.SaveChanges();

                return room;       
            }
        }

        public Room Update(Room room)
        {
            using (DBContext.BRoomsContext context = new DBContext.BRoomsContext())
            {
                Room existingRoom = context.Rooms.Find(room.Id);

                existingRoom.Name = room.Name;
                existingRoom.Places = room.Places;
 
                context.SaveChanges();

                return existingRoom;
            }
        }

        public Room Delete(int id)
        {
            using (DBContext.BRoomsContext context = new DBContext.BRoomsContext())
            {
                Room existingRoom = context.Rooms.Find(id);

                context.Rooms.Remove(existingRoom);
                context.SaveChanges();

                return existingRoom;
            }
        }
    }
}
