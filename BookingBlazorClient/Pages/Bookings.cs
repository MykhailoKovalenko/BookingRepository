using Microsoft.AspNetCore.Components;
using SharedBookingLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BookingBlazorClient.Pages
{
    public partial class Bookings
    {
        [Inject]
        private HttpClient Http { get; set; }
        private BookingOutputDTO[] bookings;
        private BookingOutputDTO _bookingToDelete;
        private BookingOutputDTO _bookingToChangeRoom;
        //private RoomOutputDTO[] rooms;
        public bool DeleteDialogOpen { get; set; }
        public bool ChangeRoomDialogOpen { get; set; }
        private CultureInfo CultureInfo_EN = CultureInfo.CreateSpecificCulture("en-US");

        private async Task OnDeleteDialogClose(bool accepted)
        {
            if(accepted)
            {
                await Http.DeleteAsync($"/Booking/{_bookingToDelete.Id}");
                await LoadData();
                _bookingToDelete = null;
            }
            
            DeleteDialogOpen = false;
            StateHasChanged();
        }

        private void OpenDeleteDialog(BookingOutputDTO booking)
        {
            DeleteDialogOpen = true;
            _bookingToDelete = booking;
            StateHasChanged();
        }
        
        protected override async Task OnInitializedAsync()
        {
            await LoadData();    
        }

        private async Task LoadData()
        {
            string start = DateTime.Today.ToString("yyyy.MM.ddTHH:mm:ss");
            string end = DateTime.Now.AddMonths(1).ToString("yyyy.MM.ddTHH:mm:ss");

            bookings = await Http.GetFromJsonAsync<BookingOutputDTO[]>($"/Booking/forPeriod?Start={start}&End={end}");
        }

        private async Task OnChangeRoomDialogClose(bool accepted)
        {
            if (accepted)
            {
                //await Http.DeleteAsync($"/Booking/{_bookingToDelete.Id}");
                await LoadData();
                _bookingToChangeRoom = null;
            }

            ChangeRoomDialogOpen = false;
            StateHasChanged();
        }

        private void OpenChangeRoomDialog(BookingOutputDTO booking)
        {
            ChangeRoomDialogOpen = true;
            _bookingToChangeRoom = booking;
            StateHasChanged();
        }
        
    }
}
