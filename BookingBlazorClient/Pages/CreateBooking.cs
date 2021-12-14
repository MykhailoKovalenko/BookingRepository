using Microsoft.AspNetCore.Components;
using SharedBookingLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BookingBlazorClient.Pages
{
    public partial class CreateBooking
    {
        [Inject]
        private HttpClient Http { get; set; }
        private BookingInputDTO _booking;
        private string _errorMessage;

        private async Task OnCreateBookingClose(bool accepted)
        {
            if (accepted)
            {
                HttpResponseMessage response = await Http.PostAsJsonAsync($"/Booking", _booking);

                if (response.IsSuccessStatusCode)
                {
                    _errorMessage = "";
                    StateHasChanged();
                }
                else
                {
                    var errorString = response.Content.ReadAsStringAsync();
                    _errorMessage = errorString.Result;
                    StateHasChanged();
                    return;
                }
            }

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
