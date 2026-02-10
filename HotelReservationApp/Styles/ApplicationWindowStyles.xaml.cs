using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HotelReservationApp.Styles
{
    public partial class ApplicationWindowStyles : ResourceDictionary
    {
        Window parentWindow;



        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            parentWindow = sender as Window;
            if (parentWindow.WindowState != WindowState.Maximized)
            {
                parentWindow?.DragMove();
            }


        }
    }
}