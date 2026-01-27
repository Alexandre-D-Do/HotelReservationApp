using HotelReservationApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelReservationApp.ViewModels
{
    internal class ReservationListingViewModel :ViewModelBase
    {
        private readonly ObservableCollection<Reservation> _reservations;
        public ICommand MakeReservationCommand { get; }
        public ReservationListingViewModel()
        {
            
        }
    }
}
