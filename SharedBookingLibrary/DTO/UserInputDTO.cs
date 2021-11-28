using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharedBookingLibrary.DTO
{
    public class UserInputDTO
    {
        [Required(ErrorMessage = "Name is not specified")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "The {0} value must be between {2} and {1} characters")]
        public string Name { get; set; }
    }
}
