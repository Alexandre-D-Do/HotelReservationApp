using HotelReservationApp.Commands;
using HotelReservationApp.Models;
using HotelReservationApp.Services;
using HotelReservationApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservationApp.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {
        
        private readonly HotelStore _hotelStore;
        private readonly ObservableCollection<ReservationViewModel> _reservations;

        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ICommand LoadReservationsCommand { get; }
        public ICommand MakeReservationCommand { get; }

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


        public ReservationListingViewModel(HotelStore hotelStore, NavigationService makeReservationNavigationService) 
        {
            _hotelStore = hotelStore;

            _reservations = new ObservableCollection<ReservationViewModel>();

            LoadReservationsCommand = new LoadReservationsCommand(this, hotelStore);

            MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);

            _hotelStore.ReservationCreated += OnReservationCreated;
        }

        public override void Dispose()
        {
            _hotelStore.ReservationCreated -= OnReservationCreated;
            base.Dispose();
        }

        private void OnReservationCreated(Reservation reservation)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModel);
        }

        public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, NavigationService makeReservationNavigationService)
        {
            ReservationListingViewModel viewModel = new ReservationListingViewModel(hotelStore, makeReservationNavigationService);
            viewModel.LoadReservationsCommand.Execute(null);
            return viewModel;
        }

        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();

            foreach (Reservation reservation in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }
        }
    }
}
