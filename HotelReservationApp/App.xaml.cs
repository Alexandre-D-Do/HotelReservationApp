using HotelReservationApp.Exceptions;
using HotelReservationApp.Models;
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
            Hotel hotel = new Hotel("Houston Inn");
            try
            {
                hotel.MakeReservation(new Reservation(
                    new RoomID(1, 3),
                    "Alex Do",
                    new DateTime(2026, 1, 1),
                    new DateTime(2026, 1, 1)
                    ));

                hotel.MakeReservation(new Reservation(
                    new RoomID(1, 3),
                    "Alex Do",
                    new DateTime(2026, 1, 1),
                    new DateTime(2026, 1, 4)
                    ));
            }

            catch (ReservationConflictException ex) 
            {

            }
            IEnumerable<Reservation> reservations = hotel.GetReservations("Alex Do");


            base.OnStartup(e);
        }
    }

}
