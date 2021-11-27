using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedBookingLibrary.DTO
{
    public class RoomOutputDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Places { get; set; }
        public bool IsProjector { get; set; }
    }
}
