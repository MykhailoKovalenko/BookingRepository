using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedBookingLibrary.DTO
{
    public class UserOutputDTO : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
