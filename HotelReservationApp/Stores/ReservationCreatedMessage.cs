using CommunityToolkit.Mvvm.Messaging.Messages;
using HotelReservationApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationApp.Stores
{
    public class ReservationCreatedMessage : ValueChangedMessage<Reservation>
    {
        public ReservationCreatedMessage(Reservation value) : base(value)
        {

        }
    }
}
