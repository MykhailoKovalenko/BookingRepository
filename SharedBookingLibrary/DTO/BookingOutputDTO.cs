using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedBookingLibrary.DTO
{
    public class BookingOutputDTO : ICloneable
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
