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
        public string Name { get; set; }
    }
}
