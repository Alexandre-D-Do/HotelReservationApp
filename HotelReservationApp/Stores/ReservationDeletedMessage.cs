using CommunityToolkit.Mvvm.Messaging.Messages;
using HotelReservationApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationApp.Stores
{
    public class ReservationDeletedMessage : ValueChangedMessage<Reservation>
    {
        public ReservationDeletedMessage(Reservation value) : base(value)
        {
        }
    }
}
