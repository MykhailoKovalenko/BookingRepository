using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharedBookingLibrary.DTO
{
    public class RoomInputDTO
    {
        [Required(ErrorMessage = "Name is not specified")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "The {0} value must be between {2} and {1} characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Places are not specified")]
        [Range(1, 500, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int Places { get; set; }

        [Required(ErrorMessage = "Projector is not specified")]
        public bool IsProjector { get; set; }
    }
}
