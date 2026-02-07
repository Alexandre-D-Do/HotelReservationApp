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

                ClearErrors(nameof(EndDate);

                if (EndDate < StartDate)
                {
                    List<string> endDateErrors = new List<string>()
                    {
                        "The end date cannot be before the start date."
                    };
                    AddError(endDateErrors);
                    OnErrorsChanged(nameof(EndDate);
                }


            }
        }

        private void AddError(List<string> endDateErrors)
        {
            _propertyNameToErrorsDictionary.Add(nameof(EndDate), endDateErrors);
        }

        public ICommand SubmitCommand { get;}
        public ICommand CancelCommand { get; }

		

        public MakeReservationViewModel(HotelStore hotelStore, NavigationService reservationListingNavigationService)
        {
			SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationListingNavigationService);
			CancelCommand = new NavigateCommand(reservationListingNavigationService);
			_propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        }

        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
		public bool HasErrors => _propertyNameToErrorsDictionary.Any();


        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        

        public IEnumerable GetErrors(string? propertyName)
        {
			return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
