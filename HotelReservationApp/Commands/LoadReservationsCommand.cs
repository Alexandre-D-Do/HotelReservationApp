using HotelReservationApp.Models;
using HotelReservationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationApp.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly Hotel _hotel;
        private readonly ReservationListingViewModel _viewModel;

        public LoadReservationsCommand(ReservationListingViewModel viewModel, Hotel hotel)
        {
            _hotel = hotel;
            _viewModel = viewModel;
        }

        public override async Tast ExecuteAsync(object parameter)
        {
            ;
        }
    }
}
