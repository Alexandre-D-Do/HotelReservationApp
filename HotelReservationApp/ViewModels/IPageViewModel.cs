using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HotelReservationApp.ViewModels
{
    public interface IPageViewModel : INotifyPropertyChanged
    {
        bool IsActive { get; set; }
    }
}
