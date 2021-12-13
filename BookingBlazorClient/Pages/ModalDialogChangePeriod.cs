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
    public partial class ModalDialogChangePeriod
    {
        [Inject]
        private HttpClient Http { get; set; }
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string ErrorMessage { get; set; }

        [Parameter]
        public BookingOutputDTO Booking { get; set; }

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

    }

}
