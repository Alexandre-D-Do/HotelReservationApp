using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.Models
{
    public class Hotel
    {
        private readonly ReservationBook _reservationBook;

        public string Name { get; }

        public Hotel(string name, ReservationBook reservationBook)
        {
            Name = name;
            _reservationBook = reservationBook;
        }

        /// <summary>
        /// Get all reservations.
        /// </summary>
        /// <returns>All current reservations.</returns>
        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationBook.GetAllReservations();
        }

        /// <summary>
        /// Make a reservation.
        /// </summary>
        /// <param name="reservation">The reservation to be made.</param>
        /// /// <exception cref="InvalidReservationTimeRangeException">
        /// Exception thrown if reservation start time is after end time.
        /// </exception>
        /// <exception cref="ReservationConflictException">
        /// Exception thrown when new reservation conflicts with existing reservation.
        /// </exception>
        public async Task MakeReservation(Reservation reservation) 
        {
            await _reservationBook.AddReservation(reservation);
        }

        public async Task DeleteReservation(Reservation reservation)
        {
            await _reservationBook.DeleteReservation(reservation);
        }

    }
}
