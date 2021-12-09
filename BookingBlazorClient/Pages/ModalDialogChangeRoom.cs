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
        public BookingOutputDTO Booking { get; set; }
        private RoomOutputDTO[] rooms;
        private CultureInfo CultureInfo_EN = CultureInfo.CreateSpecificCulture("en-US");

        [Parameter]
        public EventCallback<bool> OnClose { get; set; }

        private Task ModalCancel()
        {
            return OnClose.InvokeAsync(false);
        }

        private Task ModalOk()
        {
            return OnClose.InvokeAsync(true);
        }

        private async Task<RoomOutputDTO[]> getAvailableRoomsOnThePeriod(DateTime start, DateTime end)
        {
            string startDate = start.ToString("yyyy.MM.ddTHH:mm:ss");
            string endDate = end.ToString("yyyy.MM.ddTHH:mm:ss");

            return await Http.GetFromJsonAsync<RoomOutputDTO[]>($"/Room/free?Start={startDate}&End={endDate}");
        }

        protected override async Task OnInitializedAsync()
        {
            rooms = await getAvailableRoomsOnThePeriod(Booking.Start, Booking.End);

            rooms = rooms.OrderBy(i => i.Places)
                .ThenByDescending(i => i.IsProjector)
                .ToArray();
        }
    }
}
