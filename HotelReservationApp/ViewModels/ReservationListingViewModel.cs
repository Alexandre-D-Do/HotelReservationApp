using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelReservationApp.Models;
using HotelReservationApp.Services;
using HotelReservationApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelReservationApp.ViewModels
{
    public partial class ReservationListingViewModel : ObservableRecipient, IRecipient<ReservationCreatedMessage>, IRecipient<ReservationDeletedMessage>, IPageViewModel
    {

        private readonly HotelStore _hotelStore;
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        private readonly NavigationService<MakeReservationViewModel> _makeReservationNavigationService;

        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        public bool DisplayHasNoReservationsMsg => CheckDisplayHasReservationMessage();
        public bool DisplayDeleteButton => CheckDisplayDeleteButton();

        [RelayCommand]
        private async Task LoadReservations()
        {
            ErrorMessage = string.Empty;
            IsLoading = true;
            try
            {
                await _hotelStore.Initialize();
                UpdateReservations(_hotelStore.Reservations);
            }
            catch (Exception)
            {
                ErrorMessage = "Failed to load reservations.";
            }
            IsLoading = false;
        }

        [RelayCommand]
        private void MakeReservation()
        {
            _makeReservationNavigationService.Navigate();
            Application.Current.MainWindow.Width = 500;
            Application.Current.MainWindow.Height = 375;

        }

        [RelayCommand]
        private async Task DeleteReservation()
        {
            try
            {
                ReservationViewModel reservationViewModel = SelectedReservation;
                Reservation reservation = new Reservation(
                    new RoomID(reservationViewModel.FloorNumber, reservationViewModel.RoomNumber),
                    reservationViewModel.Username, reservationViewModel.StartDateData, reservationViewModel.EndDateData);
                await _hotelStore.DeleteReservation(reservation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error deleting reservation.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [ObservableProperty]
        private ReservationViewModel _selectedReservation;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasErrorMessage))]
        private string _errorMessage;

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ReservationListingViewModel(HotelStore hotelStore, NavigationService<MakeReservationViewModel> makeReservationNavigationService)
        {
            _hotelStore = hotelStore;

            _reservations = new ObservableCollection<ReservationViewModel>();

            _makeReservationNavigationService = makeReservationNavigationService;

            _reservations.CollectionChanged += OnReservationsChanged;

        }

        protected override void OnActivated()
        {
            StrongReferenceMessenger.Default.RegisterAll(this);
            base.OnActivated();
        }

        protected override void OnDeactivated()
        {
            StrongReferenceMessenger.Default.UnregisterAll(this);
            base.OnDeactivated();
        }

        public void Receive(ReservationCreatedMessage message)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(message.Value);
            _reservations.Add(reservationViewModel);
        }

        public void Receive(ReservationDeletedMessage message)
        {
            _reservations.Remove(SelectedReservation);
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(DisplayHasNoReservationsMsg));
            OnPropertyChanged(nameof(DisplayDeleteButton));
        }

        public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, NavigationService<MakeReservationViewModel> makeReservationNavigationService)
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

        private bool CheckDisplayHasReservationMessage()
        {
            if (HasErrorMessage == false && _reservations.Any() == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckDisplayDeleteButton()
        {
            if (_reservations.Any() == true && HasErrorMessage == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}