using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Models;

namespace BookingRooms.DBContext
{
    public class BRoomsContext : DbContext
    {
        
        public BRoomsContext(DbContextOptions<BRoomsContext> options)
             : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();    
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            

            //modelBuilder.Entity<Booking>()
            //.HasOne(p => p.Room)
            //.WithMany(b => b.Bookings)
            //.HasForeignKey(p => p.RoomId);

            

            //modelBuilder.Entity<Booking>()
            //.HasOne(p => p.User)
            //.WithMany(b => b.Bookings)
            //.HasForeignKey(p => p.UserId);


            //modelBuilder.Entity<Room>()
            //    .Property(room => room.Name)
            //    .IsRequired();
            //.HasAnnotation("ErrorMessage", "Name is is not specified");

            // [Required(ErrorMessage = "Name is not specified")]

            //modelBuilder.Entity<Room>().HasData(
            //    new Room[] {
            //        new Room { Id = 1, Name = "Ohio", Places = 12 },
            //        new Room { Id = 2, Name = "Nebraska", Places = 10 },
            //        new Room { Id = 3, Name = "Colorado", Places = 15 },
            //        new Room { Id = 4, Name = "New York", Places = 20 }
            //    });

            /*modelBuilder.Entity<Booking>().HasData(
                new Booking[] {
                    new Booking { Id = 1, Room = new Room { Id = 1, Name = "Ohio", Places = 12 }, Start = DateTime.UtcNow,   },
                    new Room { Id = 2, Name = "Nebraska", Places = 10 },
                    new Room { Id = 3, Name = "Colorado", Places = 15 },
                    new Room { Id = 4, Name = "New York", Places = 20 }
                });*/

            base.OnModelCreating(modelBuilder);
        }

    }

  
}
