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
        public event Action ReservationDeleted;

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
        
        public async Task Initialize()
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

        public async Task DeleteReservation(Reservation reservation)
        {
            await _hotel.DeleteReservation(reservation);
            _reservations.Remove(reservation);
            OnReservationDeleted();
        }

        public void OnReservationCreated(Reservation reservation)
        {
            ReservationCreated?.Invoke(reservation);
        }

        public void OnReservationDeleted()
        {
            ReservationDeleted?.Invoke();
        }
    }


}
