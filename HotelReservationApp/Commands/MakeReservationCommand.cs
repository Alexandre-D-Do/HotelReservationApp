using HotelReservationApp.Exceptions;
using HotelReservationApp.Models;
using HotelReservationApp.Services;
using HotelReservationApp.Stores;
using HotelReservationApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservationApp.Commands
{
    internal class MakeReservationCommand : AsyncCommandBase
    {
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly HotelStore _hotelStore;
        private readonly NavigationService<ReservationListingViewModel> _reservationListingNavigationService;

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, HotelStore hotelStore, 
            NavigationService<ReservationListingViewModel> reservationListingNavigationService)
        {
            _makeReservationViewModel = makeReservationViewModel;
            _hotelStore = hotelStore;
            _reservationListingNavigationService = reservationListingNavigationService;

            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        

        public override bool CanExecute(object parameter)
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

            return _makeReservationViewModel.CanCreateReservation && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_makeReservationViewModel.CanCreateReservation))
            {
                OnExecuteChanged();
            }
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _makeReservationViewModel.SubmitErrorMessage = string.Empty;
            _makeReservationViewModel.IsSubmitting = true;
            Reservation reservation = new Reservation(
                new RoomID(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
                _makeReservationViewModel.Username, 
                _makeReservationViewModel.StartDate, 
                _makeReservationViewModel.EndDate
                );

            try
            {
                await Task.Delay(2000);
                await _hotelStore.MakeReservation(reservation);
                MessageBox.Show("Successfully reserved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _reservationListingNavigationService.Navigate();

            }
            catch(ReservationConflictException)
            {
                _makeReservationViewModel.SubmitErrorMessage ="This room is already taken for the selected dates.";
            }
            catch (InvalidReservationTimeRangeException)
            {
                _makeReservationViewModel.SubmitErrorMessage = "Start date must be before end date";
            }
            catch (Exception)
            {
                _makeReservationViewModel.SubmitErrorMessage = "Failed to make reservation";
            }
            _makeReservationViewModel.IsSubmitting = false;
        }
    }
}
