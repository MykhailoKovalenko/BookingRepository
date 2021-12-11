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
    public partial class ModalDialogChangeRoom
    {
        [Inject]
        private HttpClient Http { get; set; }
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string ErrorMessage { get; set; }
        //[Parameter]
        //public bool SelectIsValid { get; set; }
       
        //[Parameter]
        public string ValidString { get; set; }
        private string _erm;

        [Parameter]
        public BookingOutputDTO Booking { get; set; }

        private RoomOutputDTO[] rooms;
        private int selectedRoomId;
        private CultureInfo CultureInfo_EN = CultureInfo.CreateSpecificCulture("en-US");

        [Parameter]
        public EventCallback<(bool, int)> OnClose { get; set; }

        private Task ModalCancel()
        {
            return OnClose.InvokeAsync((false, 0));
        }

        private Task ModalOk()
        {
            return OnClose.InvokeAsync((true, selectedRoomId));
        }

        private void OnSelectRoom()
        {
            // SelectIsValid = !(selectedRoomId == 0);
            //!(selectedRoomId == 0 || !String.IsNullOrEmpty(ErrorMessage));

            //ValidString = "is-valid";

            //if (selectedRoomId == 0 || !String.IsNullOrEmpty(ErrorMessage))
            //{
            //    ValidString = "is-invalid";
            //}

            _erm = "12w3e" + selectedRoomId;

            ValidString = (selectedRoomId > 0) ? "is-valid" : "is-invalid";
        }

        private async Task<RoomOutputDTO[]> getAvailableRoomsOnThePeriod(DateTime start, DateTime end)
        {
            string startDate = start.ToString("yyyy.MM.ddTHH:mm:ss");
            string endDate = end.ToString("yyyy.MM.ddTHH:mm:ss");

            return await Http.GetFromJsonAsync<RoomOutputDTO[]>($"/Room/free?Start={startDate}&End={endDate}");
        }

        protected override async Task OnInitializedAsync()
        {
            rooms = (await getAvailableRoomsOnThePeriod(Booking.Start, Booking.End))
                           .OrderBy(i => i.Places)
                           .ThenByDescending(i => i.IsProjector)
                           .ToArray(); 
        }
    }
}
