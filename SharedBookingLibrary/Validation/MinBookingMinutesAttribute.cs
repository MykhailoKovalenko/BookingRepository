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
    public class MinBookingMinutesAttribute : ValidationAttribute
    {
        int _minBookingMinutes;
        public MinBookingMinutesAttribute(int minBookingMinutes)
        {
            _minBookingMinutes = minBookingMinutes;
        }
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                ErrorMessage = "Invalid bookind data. Check your data and try again!";
                return false;
            }

            BookingInputDTO bookingInputDTO = value as BookingInputDTO;

            if (bookingInputDTO == null)
            {
                throw new ValidationException("Incorrect using of attribute!");
            }

            TimeSpan bookingTime = bookingInputDTO.End.Subtract(bookingInputDTO.Start);
            TimeSpan minutes = TimeSpan.FromMinutes(_minBookingMinutes);

            if (bookingTime.CompareTo(minutes) == -1)
            {
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, _minBookingMinutes);
        }
    }
}
