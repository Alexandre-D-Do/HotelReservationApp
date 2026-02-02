using HotelReservationApp.Exceptions;
using HotelReservationApp.Models;
using HotelReservationApp.Services;
using HotelReservationApp.Stores;
using HotelReservationApp.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace HotelReservationApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _hotel = new Hotel("Houston Inn");
            _navigationStore = new NavigationStore();
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateReservationListingViewModel();
            
            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(_navigationStore)

            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationListingViewModel));
        }

        private ReservationListingViewModel CreateReservationListingViewModel()
        {
            return new ReservationListingViewModel(_hotel, new NavigationService(_navigationStore, CreateMakeReservationViewModel));
        }
    }

}
