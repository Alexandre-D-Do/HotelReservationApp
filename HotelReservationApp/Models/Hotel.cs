using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationApp.Models
{
    public class Hotel
    {
        private readonly ReservationBook reservationBook;

        public string Name { get; }

        public Hotel(string name)
        {
            Name = name;
            reservationBook = new ReservationBook();
        }

        /// <summary>
        /// Get the reservations for user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The reservations for the user.</returns>
        public IEnumerable<Reservation> GetReservations(string username)
        {
            return reservationBook.GetReservations(username);
        }

        /// <summary>
        /// Make a reservation.
        /// </summary>
        /// <param name="reservation">The reservation to be made.</param>
        /// <exception cref="ReservationConflictException">
        /// Exception thrown when new reservation conflicts with existing reservation.
        /// </exception>
        public void MakeReservation(Reservation reservation) 
        {
            reservationBook.AddReservation(reservation);
        }

    }
}
