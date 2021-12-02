using SharedBookingLibrary.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedBookingLibrary.Validation
{
    class BookingStartDateLessEndDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                ErrorMessage = "Invalid bookind data. Check your data and try again!";
                return false;
            }

            BookingInputDTO bookingInputDTO = value as BookingInputDTO;

            if (bookingInputDTO == null)
                throw new ValidationException("Incorrect using of attribute!");

            if (bookingInputDTO.Start >= bookingInputDTO.End)
                return false;

            return true;
        }
    }
}
