using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace HotelReservationApp.UserControls
{
    /// <summary>
    /// Interaction logic for TitleBar.xaml
    /// </summary>
    public partial class TitleBar : UserControl
    {

        Window parentWindow;
        Canvas restoreDownIcon;
        Border restoreDownIconFront;
        Border restoreDownIconBack;
        SolidColorBrush restoreDownIconBackground = (SolidColorBrush)new BrushConverter().ConvertFrom("#1E1E1E");
        Point mouseStartingLocation;
        double windowNormalHeight;
        double windowNormalWidth;



        public TitleBar()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(TitleBar_Loaded);
        }

        private void TitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            parentWindow = Window.GetWindow(this);
            windowName.Content = parentWindow.Title;
            windowNormalHeight = parentWindow.Height;
            windowNormalWidth = parentWindow.Width;
        }


        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {

            parentWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            windowNormalHeight = parentWindow.Height;
            windowNormalWidth = parentWindow.Width;
            WindowState_Switch();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.Close();
        }



        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && parentWindow.WindowState == WindowState.Maximized)
            {
                Point mouseNewLocation = e.GetPosition(parentWindow);
                parentWindow.Top = getNewTop(mouseNewLocation.Y);
                parentWindow.Left = getNewLeft(mouseNewLocation.X);
                WindowState_Switch();
                parentWindow.DragMove();
            }
        }

        private void WindowState_Switch()
        {
            if (parentWindow.WindowState == WindowState.Normal)
            {
                parentWindow.WindowState = WindowState.Maximized;
                parentWindow.BorderThickness = new Thickness(8);
                MaximizeIcon_Switch();


            }
            else if (parentWindow.WindowState == WindowState.Maximized)
            {
                parentWindow.WindowState = WindowState.Normal;
                parentWindow.BorderThickness = new Thickness(0.65);
                MaximizeIcon_Switch();
            }
        }

        private void MaximizeIcon_Switch()
        {
            // If restore down icon is null, create it
            if (restoreDownIcon == null)
            {
                restoreDownIcon = new Canvas
                {
                    Width = 15,
                    Height = 15
                };
                restoreDownIconBack = new Border
                {
                    Background = Brushes.Transparent,
                    Width = 10,
                    Height = 10,
                    Margin = new Thickness(3, 1, 0, 0),
                    BorderBrush = Brushes.White,
                    BorderThickness = new Thickness(1, 1, 1, 1)
                };
                restoreDownIconFront = new Border
                {
                    Background = restoreDownIconBackground,
                    Width = 10,
                    Height = 10,
                    Margin = new Thickness(0, 4, 0, 0),
                    BorderBrush = Brushes.White,
                    BorderThickness = new Thickness(1, 1, 1, 1)
                };
                restoreDownIcon.Children.Add(restoreDownIconBack);
                restoreDownIcon.Children.Add(restoreDownIconFront);
            }

            if (parentWindow.WindowState == WindowState.Maximized)
            {
                maximizeButtonControl.Content = restoreDownIcon;
            }

            else if (parentWindow.WindowState == WindowState.Normal)
            {
                maximizeButtonControl.Content = "◻";
            }
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (parentWindow.WindowState == WindowState.Maximized)
            {
                mouseStartingLocation = e.GetPosition(parentWindow);
            }
        }

        private double getNewTop(double mouseDeltaY)
        {
            double mouseYAnchor = mouseStartingLocation.Y;
            double windowMaximizedHeight = parentWindow.ActualHeight;
            double newTop = mouseYAnchor - ((mouseYAnchor / windowMaximizedHeight) * windowNormalHeight) + (mouseDeltaY - mouseYAnchor);
            return newTop;
        }

        private double getNewLeft(double mouseDeltaX)
        {
            double mouseXAnchor = mouseStartingLocation.X;
            double windowMaximizedWidth = parentWindow.ActualWidth;
            double newLeft = mouseXAnchor - ((mouseXAnchor / windowMaximizedWidth) * windowNormalWidth) + (mouseDeltaX - mouseXAnchor);
            return newLeft;
        }
    }
}