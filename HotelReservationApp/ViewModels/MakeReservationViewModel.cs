using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelReservationApp.Exceptions;
using HotelReservationApp.Models;
using HotelReservationApp.Services;
using HotelReservationApp.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelReservationApp.ViewModels
{
    [ObservableRecipient]
    public partial class MakeReservationViewModel : ObservableValidator, IPageViewModel
    {
        private readonly HotelStore _hotelStore;
        private readonly NavigationService<ReservationListingViewModel> _reservationListingNavigationService;

        [ObservableProperty]
        private bool _isLoading;

		[ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Username is required.")]
        [NotifyPropertyChangedFor(nameof(CanCreateReservation))]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string _username;


		[ObservableProperty]
        [NotifyDataErrorInfo]
        [Range(1, int.MaxValue, ErrorMessage = "Floor number is less than 0.")]
		[NotifyPropertyChangedFor(nameof(CanCreateReservation))]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private int? _floorNumber = 1;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Range(1, int.MaxValue, ErrorMessage = "Room number is less than 0.")]
        [NotifyPropertyChangedFor(nameof(CanCreateReservation))]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private int? _roomNumber = 1;

		[ObservableProperty]
        [NotifyDataErrorInfo]
        [CustomValidation(typeof(MakeReservationViewModel), nameof(ValidateDates), ErrorMessage = "Start date is after end date.")]
        [NotifyPropertyChangedFor(nameof(CanCreateReservation))]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private DateTime _startDate = new DateTime(2026, 1, 1);

        //This revalidates the start date when the end date changes to ensure that the validation error message is updated in real time as the user changes the dates.
        partial void OnStartDateChanged(DateTime value)
        {
            ValidateProperty(EndDate, nameof(EndDate));
        }

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [CustomValidation(typeof(MakeReservationViewModel), nameof(ValidateDates), ErrorMessage = "End date is before start date.")]
        [NotifyPropertyChangedFor(nameof(CanCreateReservation))]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private DateTime _endDate = new DateTime(2026, 1, 8);

        //This revalidates the end date when the start date changes to ensure that the validation error message is updated in real time as the user changes the dates.
        partial void OnEndDateChanged(DateTime value)
        {
            ValidateProperty(StartDate, nameof(StartDate));
        }


        public static ValidationResult? ValidateDates(DateTime date, ValidationContext context)
        {
            MakeReservationViewModel viewModel = (MakeReservationViewModel)context.ObjectInstance;
            
            if(viewModel.StartDate < viewModel.EndDate)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Start date is after end date.");
        }

        private bool HasUsername => !string.IsNullOrEmpty(Username);
		private bool IsValidFloorNumber => FloorNumber > 0;
        private bool IsValidRoomNumber => RoomNumber > 0;
		private bool HasStartDateBeforeEndDate => StartDate < EndDate;
		public bool HasSubmitErrorMessage => !string.IsNullOrEmpty(SubmitErrorMessage);
        
        
        public bool CanCreateReservation => HasUsername && IsValidFloorNumber && IsValidRoomNumber && HasStartDateBeforeEndDate
			&& !HasErrors;


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasSubmitErrorMessage))]
        private string _submitErrorMessage;


        [ObservableProperty]
		private bool _isSubmitting;

        [RelayCommand(CanExecute = nameof(CanCreateReservation))]
        private async Task Submit()
        {
            SubmitErrorMessage = string.Empty;
            IsSubmitting = true;
            Reservation reservation = new Reservation(new RoomID(FloorNumber, RoomNumber), Username, StartDate, EndDate);

            try
            {
                await _hotelStore.MakeReservation(reservation);
                MessageBox.Show("Successfully reserved.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _reservationListingNavigationService.Navigate();
                Application.Current.MainWindow.Width = 500;
                Application.Current.MainWindow.Height = 275;
            }
            catch (ReservationConflictException)
            {
                SubmitErrorMessage = "This room is already taken for the selected dates.";
            }
            catch (InvalidReservationTimeRangeException)
            {
                SubmitErrorMessage = "Start date must be before end date";
            }
            catch (Exception)
            {
                SubmitErrorMessage = "Failed to make reservation";
            }   
            IsSubmitting = false;
        }

        [RelayCommand]
        private void Cancel()
        {
            _reservationListingNavigationService.Navigate();
            Application.Current.MainWindow.Width = 500;
            Application.Current.MainWindow.Height = 275;

        }
   
        public MakeReservationViewModel(HotelStore hotelStore, NavigationService<ReservationListingViewModel> reservationListingNavigationService)
        {
			_hotelStore = hotelStore;
            _reservationListingNavigationService = reservationListingNavigationService;
            Messenger = StrongReferenceMessenger.Default;
        }


        
    }
}
