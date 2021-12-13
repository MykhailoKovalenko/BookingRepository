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
    public partial class BookingDetails
    {
        [Inject]
        private HttpClient Http { get; set; }
        [Parameter]
        public BookingInputDTO Booking { get; set; }
        [Parameter]
        public string ButtonText { get; set; }
        [Parameter]
        public EventCallback OnValidSubmit { get; set; }

        private RoomOutputDTO[] rooms;
        private UserOutputDTO[] users;

        protected override async Task OnInitializedAsync()
        {
            rooms = (await Http.GetFromJsonAsync<RoomOutputDTO[]>($"/Room"))
                            .OrderBy(i => i.Places)
                            .ThenByDescending(i => i.IsProjector)
                            .ToArray();

            users = (await Http.GetFromJsonAsync<UserOutputDTO[]>($"/User"))
                            .OrderBy(i => i.Name)
                            .ToArray();
        }
    }

}

