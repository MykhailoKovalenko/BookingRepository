using SharedBookingLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingBlazorClient.Pages
{
    public partial class CreateBooking
    {
        private BookingInputDTO _booking;
        private async Task Create()
        {
            //await service.CreateCustomerAsync(customer);
            navigation.NavigateTo("bookings");
        }

        protected override async Task OnInitializedAsync()
        {
            _booking = new BookingInputDTO();
            _booking.Start = DateTime.Today.AddHours(DateTime.Now.Hour + 1);
            _booking.End = _booking.Start.AddHours(1);
        }

    }
}
