using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedBookingLibrary.RequestClasses
{
    public class RoomParameters
    {
        public DateTime StartBookingDate { get; set; } = DateTime.MinValue;
        public DateTime EndBookingDate { get; set; } = DateTime.MinValue;
        public uint MinPlaces { get; set; } = 0;
        public bool ProjectorAvailable { get; set; } = false;
        public bool UseProjectorParameter { get; set; } = false;
    }
}
