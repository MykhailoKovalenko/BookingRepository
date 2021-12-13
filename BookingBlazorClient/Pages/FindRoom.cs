using SharedBookingLibrary.RequestClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingBlazorClient.Pages
{
    public partial class FindRoom
    {
        private RoomParameters _roomParameters;

        protected override void OnInitialized()
        {
            _roomParameters =  new RoomParameters();
            _roomParameters.StartBookingDate = DateTime.Today.AddHours(DateTime.Now.Hour + 1);
            _roomParameters.EndBookingDate = _roomParameters.StartBookingDate.AddHours(1);
        }
    }
}
