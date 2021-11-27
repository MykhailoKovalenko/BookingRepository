using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRooms.Models.Validation
{
    public class MaxBookingHoursAttribute : ValidationAttribute
    {
        int _maxBookingHours;
        public MaxBookingHoursAttribute(int maxBookingHours)
        {
            _maxBookingHours = maxBookingHours;
        }
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                ErrorMessage = "Invalid bookind data. Check your data and try again!";
                return false;
            }
            
            Booking booking = value as Booking;

            if(booking == null)
            {
                throw new ValidationException("Incorrect using of attribute!");
            }

            TimeSpan bookingTime = booking.End.Subtract(booking.Start);
            TimeSpan hours = TimeSpan.FromHours(_maxBookingHours);

            if (bookingTime.CompareTo(hours) == 1)
            {     
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, _maxBookingHours);
        }

    }
}
