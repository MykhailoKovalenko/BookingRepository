using Microsoft.AspNetCore.Components;
using SharedBookingLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingBlazorClient.Pages
{
    public partial class ModalDialogCreateRoom
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string ErrorMessage { get; set; }

        [Parameter]
        public RoomOutputDTO Room { get; set; }

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
