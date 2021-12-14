using Microsoft.AspNetCore.Components;
using SharedBookingLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookingBlazorClient.Pages
{
    public partial class RoomDetails
    {
        [Inject]
        private HttpClient Http { get; set; }
        [Parameter]
        public RoomOutputDTO Room { get; set; }
        
        [Parameter]
        public string ErrorMessage { get; set; }

    }
}
