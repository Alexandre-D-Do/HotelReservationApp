using HotelReservationApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationApp.Stores
{
    public class HotelStore
    {
        private readonly Hotel _hotel;
        private readonly List<Reservation> _reservations;
        private Lazy<Task> _initializeLazy;
        public IEnumerable<Reservation> Reservations => _reservations;
        public event Action<Reservation> ReservationCreated;

        public HotelStore(Hotel hotel)
        {
            _hotel = hotel;
            _initializeLazy = new Lazy<Task>(Initialize);
            _reservations = new List<Reservation>();
        }

        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception)
            {
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }  
        
        private async Task Initialize()
        {
            IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();
            _reservations.Clear();
            _reservations.AddRange(reservations);
        }

        public async Task MakeReservation(Reservation reservation)
        {
            await _hotel.MakeReservation(reservation);

            _reservations.Add(reservation);

            OnReservationCreated(reservation);
        }

        public void OnReservationCreated(Reservation reservation)
        {
            ReservationCreated?.Invoke(reservation);
        }
    }


}
