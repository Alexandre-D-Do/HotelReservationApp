using HotelReservationApp.DbContexts;
using HotelReservationApp.Exceptions;
using HotelReservationApp.Models;
using HotelReservationApp.Services;
using HotelReservationApp.Stores;
using HotelReservationApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        private readonly IHost _host;
        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;

        public App()
        {
            /// Look into logging

            //Create builder
            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                // Add Db Context using App.config for connection string
                services.AddDbContext<HotelReservationAppDbContext>(options =>
                {
                    options.UseSqlServer(ConfigurationManager.ConnectionStrings["ReservationsDatabase"].ConnectionString);
                });
            }).Build();

            

            _hotel = new Hotel("Houston Inn");
            _navigationStore = new NavigationStore();
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            HotelReservationAppDbContext hotelReservationAppDbContext = _host.Services.GetRequiredService<HotelReservationAppDbContext>();
            using (hotelReservationAppDbContext)
            {
                hotelReservationAppDbContext.Database.Migrate();
            }
            
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
