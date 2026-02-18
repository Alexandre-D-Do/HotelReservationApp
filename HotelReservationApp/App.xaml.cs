using HotelReservationApp.DbContexts;
using HotelReservationApp.Exceptions;
using HotelReservationApp.HostBuilders;
using HotelReservationApp.Models;
using HotelReservationApp.Services;
using HotelReservationApp.Stores;
using HotelReservationApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        


        public App()
        {
            /// Look into logging

            //Create builder
            _host = Host.CreateDefaultBuilder().AddViewModels().AddServices()
                .ConfigureServices((hostContext, services) =>
            {
                string connectionString = hostContext.Configuration.GetConnectionString("ReservationsDatabase");

                // Add Db Context using App.config for connection string
                services.AddDbContext<HotelReservationAppDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
                

                /// Register Reservation Book and Hotel through factory function
                services.AddTransient<ReservationBook>();
                services.AddSingleton((s) => new Hotel("Houston Inn", s.GetRequiredService<ReservationBook>()));

                

                services.AddSingleton<HotelStore>();
                services.AddSingleton<NavigationStore>();
                
                services.AddSingleton(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainWindowViewModel>()
                });
            }).Build();


        }

        

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            using (HotelReservationAppDbContext hotelReservationAppDbContext = _host.Services.GetRequiredService<HotelReservationAppDbContext>())
            {
                hotelReservationAppDbContext.Database.Migrate();
            }

            /// Initial navigation
            NavigationService<ReservationListingViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<ReservationListingViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();

            MainWindow.Show();

            base.OnStartup(e);
        }




        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }
    }
}

