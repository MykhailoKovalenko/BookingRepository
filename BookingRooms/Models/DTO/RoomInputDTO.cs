using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Models.DTO
{
    public class RoomInputDTO
    {
        [Required(ErrorMessage = "Name is not specified")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Places are not specified")]
        [Range(1, 500, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Places { get; set; }

        [Required(ErrorMessage = "Projector is not specified")]
        public bool IsProjector { get; set; }
    }
}
