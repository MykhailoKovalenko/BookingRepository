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
        private string _errorMessage;
        private bool _selectIsValid;
        public bool DeleteDialogOpen { get; set; }
        public bool ChangeRoomDialogOpen { get; set; }
        private CultureInfo CultureInfo_EN = CultureInfo.CreateSpecificCulture("en-US");

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

        #region ModalDialogDelete

        private void OpenDeleteDialog(BookingOutputDTO booking)
        {
            DeleteDialogOpen = true;
            _bookingToDelete = booking;
            StateHasChanged();
        }

        private async Task OnDeleteDialogClose(bool accepted)
        {
            if (accepted)
            {
                await Http.DeleteAsync($"/Booking/{_bookingToDelete.Id}");
                await LoadData();
                _bookingToDelete = null;
            }

            DeleteDialogOpen = false;
            StateHasChanged();
        }

        #endregion

        #region ModalDialogChangeRoom

        private void OpenChangeRoomDialog(BookingOutputDTO booking)
        {
            ChangeRoomDialogOpen = true;
            _bookingToChangeRoom = booking;
            _errorMessage = "Please, select room!";
            _selectIsValid = false;
            StateHasChanged();
        }

        private async Task OnChangeRoomDialogClose((bool accepted, int selectedRoomId) selectionResult)
        {
            if(!selectionResult.accepted)
            {
                ChangeRoomDialogOpen = false;
                StateHasChanged();
                return;
            }

            if(selectionResult.selectedRoomId == 0)
            {
                _errorMessage = "Please, select room!";
                _selectIsValid = false;
                StateHasChanged();
                return;
            }

            _bookingToChangeRoom.RoomId = selectionResult.selectedRoomId;

            HttpResponseMessage response = await Http.PutAsJsonAsync($"/Booking/{_bookingToChangeRoom.Id}", _bookingToChangeRoom);

            if(response.IsSuccessStatusCode)
            {
                ChangeRoomDialogOpen = false;
                _errorMessage = "";
                _selectIsValid = true;

                await LoadData();
                _bookingToChangeRoom = null;

                StateHasChanged();
                return;
            }
            else
            {
                var errorString = response.Content.ReadAsStringAsync();
                _errorMessage = errorString.Result;
                _selectIsValid = false;
                StateHasChanged();
            }
        }

        #endregion
        //private async Task UpdateBookingRoomAsync(BookingOutputDTO Booking, int selectedRoomId)
        //{

        //    Booking.RoomId = selectedRoomId;

        //    try
        //    {


        //        HttpResponseMessage response = await Http.PutAsJsonAsync($"/Booking/{Booking.Id}", Booking);

        //        var dfkhknh = response.IsSuccessStatusCode;

        //        Task<string> err = response.Content.ReadAsStringAsync();

        //        var readAsStringAsync = response.Content.ReadAsStringAsync();
        //        string dfjg = readAsStringAsync.Result;

        //        var scode = response.StatusCode;
        //        string sscode = scode.ToString();

        //        //string json = JsonSerializer.Serialize<Person>(tom);

        //        //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(employeesFromDb.MetaData));

        //        //SystemTextJsonOutputFormatter



        //        //Stream receiveStream = response.GetResponseStream();
        //        //StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
        //        //txtBlock.Text = readStream.ReadToEnd();

        //        // string err = hc.ToString();

        //        string ss = await response.Content.ReadFromJsonAsync<string>();
        //    }
        //    catch (Exception ex)
        //    {
        //        string mes = ex.Message;
        //        int fg = 5;
        //    }


        //    //return Task;
        //}



    }
}
