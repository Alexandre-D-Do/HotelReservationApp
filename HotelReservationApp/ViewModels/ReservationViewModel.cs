using CommunityToolkit.Mvvm.ComponentModel;
using HotelReservationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.ViewModels
{
    public class ReservationViewModel : ObservableObject
    {
        private readonly Reservation _reservation;
        public string RoomID => _reservation.RoomID?.ToString();
        public string Username => _reservation.Username;
        public string StartDate => _reservation.StartDate.ToString("d");
        public string EndDate => _reservation.EndDate.ToString("d");
        public int? FloorNumber => _reservation.RoomID.FloorNumber;
        public int? RoomNumber => _reservation.RoomID.RoomNumber;
        public DateTime StartDateData => _reservation.StartDate;
        public DateTime EndDateData => _reservation.EndDate;

        public ReservationViewModel(Reservation reservation)
        {
            _reservation = reservation;

        }
    }
}
