using Microsoft.AspNetCore.Components;
using SharedBookingLibrary.DTO;
using SharedBookingLibrary.RequestClasses;
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

        [Parameter]
        public RoomParameters RoomParameters { get; set; }

        private RoomOutputDTO[] rooms;
        private RoomOutputDTO _roomToDelete;
        private RoomOutputDTO _roomToEdit;
        private RoomOutputDTO _roomToCreate;
        private string _errorMessage;
        public bool DeleteDialogOpen { get; set; }
        public bool EditRoomDialogOpen { get; set; }
        public bool CreateRoomDialogOpen { get; set; }

        #region DeleteRegion
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
                await LoadData();
                _roomToDelete = null;
            }

            DeleteDialogOpen = false;
            StateHasChanged();
        }

        #endregion

        private void OpenEditRoomDialog(RoomOutputDTO room)
        {
            EditRoomDialogOpen = true;
            _roomToEdit = (RoomOutputDTO) room.Clone();
            _errorMessage = "";
            StateHasChanged();
        }

        private async Task OnEditRoomDialogClose(bool accepted)
        {
            if (!accepted)
            {
                EditRoomDialogOpen = false;
                await LoadData();
                StateHasChanged();
                return;
            }

            HttpResponseMessage response = await Http.PutAsJsonAsync($"/Room/{_roomToEdit.Id}", _roomToEdit);

            if (response.IsSuccessStatusCode)
            {
                EditRoomDialogOpen = false;
                _errorMessage = "";
                _roomToEdit = null;
            }
            else
            {
                var errorString = response.Content.ReadAsStringAsync();
                _errorMessage = errorString.Result;
            }

            await LoadData();
            StateHasChanged();
        }

        private void OpenCreateRoomDialog()
        {
            CreateRoomDialogOpen = true;
            _roomToCreate = new RoomOutputDTO();
            _errorMessage = "";
            StateHasChanged();
        }

        private async Task OnCreateRoomDialogClose(bool accepted)
        {
            if (!accepted)
            {
                CreateRoomDialogOpen = false;
                await LoadData();
                StateHasChanged();
                return;
            }

            HttpResponseMessage response = await Http.PostAsJsonAsync($"/Room", _roomToCreate);

            if (response.IsSuccessStatusCode)
            {
                CreateRoomDialogOpen = false;
                _errorMessage = "";
                _roomToCreate = null;
            }
            else
            {
                var errorString = response.Content.ReadAsStringAsync();
                _errorMessage = errorString.Result;
            }

            await LoadData();
            StateHasChanged();
        }

        private async Task LoadData()
        {
            if(RoomParameters == null)
            {
                RoomParameters = new RoomParameters();
            }

            // filter?StartBookingDate=2000-12-13T19:00:00&EndBookingDate=2000-12-13T20:30:00&MinPlaces=0&ProjectorAvailable=false&UseProjectorParameter=false
            string queryString = $"?StartBookingDate={RoomParameters.StartBookingDate.ToString("yyyy-MM-ddTHH:mm")}&EndBookingDate={RoomParameters.EndBookingDate.ToString("yyyy-MM-ddTHH:mm")}"
                                         + $"&MinPlaces={RoomParameters.MinPlaces}&ProjectorAvailable={RoomParameters.ProjectorAvailable}"
                                        + $"&UseProjectorParameter={RoomParameters.UseProjectorParameter}";

            rooms = await Http.GetFromJsonAsync<RoomOutputDTO[]>("/Room/filter" + queryString);
        }

        protected override async Task OnInitializedAsync()
        { 
            await LoadData(); 
        }

        //protected override async Task OnParametersSetAsync()
        //{
        //    await LoadData();
        //    //return base.OnParametersSetAsync();
        //}
    }
}
