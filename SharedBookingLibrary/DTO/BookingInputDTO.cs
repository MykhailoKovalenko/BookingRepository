using BookingRooms.Models.Validation;
using SharedBookingLibrary.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedBookingLibrary.DTO
{
    [MaxBookingHours(maxBookingHours: 8, ErrorMessage = "Booking time should not exceed {0} hours!")]
    [MinBookingMinutes(minBookingMinutes: 30, ErrorMessage = "Booking time should not be less then {0} minutes!")]
    public class BookingInputDTO
    {
        [Required(ErrorMessage = "Start date is not specified")]
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = "End date is not specified")]
        [DataType(DataType.DateTime)]
        public DateTime End { get; set; }

        [Required(ErrorMessage = "RoomId is not specified")]
        [Range(1, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "UserId is not specified")]
        [Range(1, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int UserId { get; set; }

    }
}
