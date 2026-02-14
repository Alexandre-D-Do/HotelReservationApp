using HotelReservationApp.Commands;
using HotelReservationApp.Models;
using HotelReservationApp.Services;
using HotelReservationApp.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservationApp.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
    {

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private string _username;
		public string Username
		{
			get
			{
				return _username;
			}
			set
			{
				_username = value;
				OnPropertyChanged(nameof(Username));

				ClearErrors(nameof(Username));
				if (!HasUsername)
				{
					AddError("Username cannot be empty.", nameof(Username));
				}
                OnPropertyChanged(nameof(CanCreateReservation));
            }
		}

		private string _floorNumber;
		public string FloorNumber
		{
			get
			{
				return _floorNumber;
			}
			set
			{
				_floorNumber = value;
				OnPropertyChanged(nameof(FloorNumber));

                ClearErrors(nameof(FloorNumber));
                if (!IsValidFloorNumber)
                {
                    AddError("Floor number must be greater than 0.", nameof(FloorNumber));
                }
                OnPropertyChanged(nameof(CanCreateReservation));
            }
		}

		private string _roomNumber;
		public string RoomNumber
		{
			get
			{
				return _roomNumber;
			}
			set
			{
				_roomNumber = value;
				OnPropertyChanged(nameof(RoomNumber));

                ClearErrors(nameof(RoomNumber));
                if (!IsValidRoomNumber)
                {
                    AddError("Room number must be greater than 0.", nameof(RoomNumber));
                }
                OnPropertyChanged(nameof(CanCreateReservation));
            }
		}

		private DateTime _startDate = new DateTime(2026, 1, 1);
		public DateTime StartDate
		{
			get
			{
				return _startDate;
			}
			set
			{
				_startDate = value;
				OnPropertyChanged(nameof(StartDate));

				ClearErrors(nameof(StartDate));
				ClearErrors(nameof(EndDate));

				if(!HasStartDateBeforeEndDate)
				{
					AddError("The start date cannot be after the end date.", nameof(StartDate));
				}
				OnPropertyChanged(nameof(CanCreateReservation));

			}
		}

		private DateTime _endDate = new DateTime(2026, 1, 8);

        public DateTime EndDate
		{
			get
			{
				return _endDate;
			}
			set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));

				ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (!HasStartDateBeforeEndDate)
                {
                    AddError("The end date cannot be before the start date.", nameof(EndDate));
                }

				OnPropertyChanged(nameof(CanCreateReservation));

            }
        }

		private bool HasUsername => !string.IsNullOrEmpty(Username);
		private bool IsValidFloorNumber => int.Parse(FloorNumber) > 0;
        private bool IsValidRoomNumber => int.Parse(RoomNumber) > 0;
		private bool HasStartDateBeforeEndDate => StartDate < EndDate;
		public bool HasSubmitErrorMessage => !string.IsNullOrEmpty(SubmitErrorMessage);
		public bool CanCreateReservation => HasUsername && IsValidFloorNumber && IsValidRoomNumber && HasStartDateBeforeEndDate
			&& !HasErrors;

		private string _submitErrorMessage;

		public string SubmitErrorMessage
		{
			get 
			{ 
				return _submitErrorMessage; 
			}
			set 
			{ 
				_submitErrorMessage = value;
                OnPropertyChanged(nameof(SubmitErrorMessage));
                OnPropertyChanged(nameof(HasSubmitErrorMessage));
            }
		}

		private bool _isSubmitting;

		public bool IsSubmitting
		{
			get 
			{ 
				return _isSubmitting; 
			}
			set 
			{ 
				_isSubmitting = value; 
				OnPropertyChanged(nameof(IsSubmitting));
			}
		}


		public ICommand SubmitCommand { get;}
        public ICommand CancelCommand { get; }

        public MakeReservationViewModel(HotelStore hotelStore, NavigationService<ReservationListingViewModel> reservationListingNavigationService)
        {
			SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationListingNavigationService);
			CancelCommand = new NavigateCommand<ReservationListingViewModel>(reservationListingNavigationService);
			_propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        }

        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
		public bool HasErrors => _propertyNameToErrorsDictionary.Any();


        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        

        public IEnumerable GetErrors(string? propertyName)
        {
			return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
