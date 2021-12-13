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
        private RoomOutputDTO _roomToDelete;
        private RoomOutputDTO _roomToEdit;   
        private string _errorMessage;
        public bool DeleteDialogOpen { get; set; }
        public bool EditRoomDialogOpen { get; set; }

        #region ModalDialogDelete
        private void OpenDeleteDialog(RoomOutputDTO room)
        {
            DeleteDialogOpen = true;
            _roomToDelete = room;
            StateHasChanged();
        }

        private async Task OnDeleteDialogClose(bool accepted)
        {
            if (accepted)
            {
                await Http.DeleteAsync($"/Room/{_roomToDelete.Id}");
                //await LoadData();
                _roomToDelete = null;
            }

            DeleteDialogOpen = false;
            StateHasChanged();
        }

        #endregion

        #region ModalDialogEditRoom

        private void OpenEditRoomDialog(RoomOutputDTO room)
        {
            EditRoomDialogOpen = true;
            _roomToEdit = room;
            //_errorMessage = "Please, select room!";
            StateHasChanged();
        }

        private async Task OnEditRoomDialogClose(bool accepted)
        {
            if (!accepted)
            {
                EditRoomDialogOpen = false;
                StateHasChanged();
                return;
            }

            

            //_bookingToChangeRoom.RoomId = selectionResult.selectedRoomId;

            HttpResponseMessage response = await Http.PutAsJsonAsync($"/Booking/{_roomToEdit.Id}", _roomToEdit);

            if (response.IsSuccessStatusCode)
            {
                EditRoomDialogOpen = false;
                _errorMessage = "";

                //await LoadData();
                _roomToEdit = null;

                StateHasChanged();
                return;
            }
            else
            {
                var errorString = response.Content.ReadAsStringAsync();
                _errorMessage = errorString.Result;
                StateHasChanged();
            }
        }

        #endregion

        protected override async Task OnInitializedAsync() =>
            rooms = await Http.GetFromJsonAsync<RoomOutputDTO[]>("/Room");
    }
}
