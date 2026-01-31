using HotelReservationApp.Exceptions;
using HotelReservationApp.Models;
using HotelReservationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservationApp.Commands
{
    internal class MakeReservationCommand : CommandBase
    {
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly Hotel _hotel;

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, Hotel hotel)
        {
            _makeReservationViewModel = makeReservationViewModel;
            _hotel = hotel;

            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        

        public override bool CanExecute(object? parameter)
        {
            // Check if floor number is parsable
            if (!int.TryParse(_makeReservationViewModel.FloorNumber, out int floorNumberValue))
            {
                return false;
            }

            // Check if room number is parsable
            if (!int.TryParse(_makeReservationViewModel.RoomNumber, out int roomNumberValue))
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(_makeReservationViewModel.Username) &&
                floorNumberValue > 0 &&
                roomNumberValue > 0 &&
                DateTime.Compare(_makeReservationViewModel.StartDate, _makeReservationViewModel.EndDate) < 0 &&
                base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_makeReservationViewModel.Username) || 
                e.PropertyName == nameof(_makeReservationViewModel.FloorNumber) || 
                e.PropertyName == nameof(_makeReservationViewModel.RoomNumber) ||
                e.PropertyName == nameof(_makeReservationViewModel.StartDate) ||
                e.PropertyName == nameof(_makeReservationViewModel.EndDate))
            {
                OnExecuteChanged();
            }
        }

        public override void Execute(object? parameter)
        {
            Reservation reservation = new Reservation(
                new RoomID(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
                _makeReservationViewModel.Username, 
                _makeReservationViewModel.StartDate, 
                _makeReservationViewModel.EndDate
                );

            try
            {
                _hotel.MakeReservation(reservation);
                MessageBox.Show("Successfully reserved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(ReservationConflictException)
            {
                MessageBox.Show("This room is already taken within the selected timeframe.", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
