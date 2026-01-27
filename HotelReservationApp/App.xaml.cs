using HotelReservationApp.Exceptions;
using HotelReservationApp.Models;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel()

            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
