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
    public partial class Rooms

    {
        [Inject]
        private HttpClient Http { get; set; }

        private RoomOutputDTO[] rooms;
        protected override async Task OnInitializedAsync() =>
            rooms = await Http.GetFromJsonAsync<RoomOutputDTO[]>("https://localhost:44391/Room");

    }
}
