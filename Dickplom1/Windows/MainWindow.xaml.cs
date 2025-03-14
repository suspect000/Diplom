using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Dickplom1.Class;


namespace Dickplom1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HidenRect.Focus();
        }

        //Header
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text == "Найти")
            {
                tbSearch.Text = "";
                imgLupa.Visibility = Visibility.Collapsed;
                Animations.WidthAnimation(tbSearch, tbSearch.Width, 610, 0.2);
            }
        }

        private void tbSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                tbSearch.Text = "Найти";
                imgLupa.Visibility = Visibility.Visible;
                Animations.WidthAnimation(tbSearch, tbSearch.Width, 228, 0.2);
            }
        }

        //Анимация tBox поисковика
        private void tbSearch_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (tbSearch, (Color)ColorConverter.ConvertFromString(tbSearch.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.2);
        }

        private void tbSearch_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (tbSearch, (Color)ColorConverter.ConvertFromString(tbSearch.BorderBrush.ToString()), Colors.Transparent, 0.2);
        }

        //Анимация кнопки уведомлений
        private void btnNotification_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (btnNotification, (Color)ColorConverter.ConvertFromString(btnNotification.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.2);
        }

        private void btnNotification_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (btnNotification, (Color)ColorConverter.ConvertFromString(btnNotification.BorderBrush.ToString()), Colors.Transparent, 0.2);
        }

        //Анимация кнопки профиля
        private void btnProfile_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (btnProfile, (Color)ColorConverter.ConvertFromString(btnProfile.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.2);
        }

        private void btnProfile_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (btnProfile, (Color)ColorConverter.ConvertFromString(btnProfile.BorderBrush.ToString()), Colors.Transparent, 0.2);
        }
        //Header-----------------------------------------------------------------------------------


        //Navigation
        //Анимация навигации (Аналитика)
        private void gridDashboard_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                            (borderDashboard, (Color)ColorConverter.ConvertFromString(borderDashboard.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.2);
        }

        private void gridDashboard_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                            (borderDashboard, (Color)ColorConverter.ConvertFromString(borderDashboard.BorderBrush.ToString()), Colors.Transparent, 0.2);
        }
        //-----------------------------------------------------------------------------------

        //Анимация клиентов (навигация)
        private void gridClients_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                            (borderClients, (Color)ColorConverter.ConvertFromString(borderClients.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.2);
        }
        private void gridClients_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                            (borderClients, (Color)ColorConverter.ConvertFromString(borderClients.BorderBrush.ToString()), Colors.Transparent, 0.2);
        }

        private void gridClients_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SPClientsItems.Visibility == Visibility.Collapsed)
            {
                //Раскрытие топиков
                Animations.MovingAnimation(SPClientsItems, new Thickness(0,-30,0,0), new Thickness(0,0,0,0), 0.3);
                Animations.RotationAnimation(imgArrowClients, 0, 180, 0.2);
                SPClientsItems.Visibility = Visibility.Visible;
                Animations.OpacityAnimation(SPClientsItems, 0, 1, 0.2);
                Animations.HeightAnimation(SPClientsItems, 0, 138, 0.2);
            }
            else
            {
                //Сворачивание топиков
                Animations.MovingAnimation(SPClientsItems, SPClientsItems.Margin, new Thickness(0, -30, 0, 0), 0.3);
                Animations.RotationAnimation(imgArrowClients, 180, 0, 0.2);
                Animations.OpacityAnimation(SPClientsItems, 1, 0, 0.2);
                Animations.HeightAnimation(SPClientsItems, 138, 0, 0.3, SPClientsItems);
            }
        }
        //-----------------------------------------------------------------------------------

        //Анимация вложений клиентов (навигация)
        private void gridClientsItem1_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderClientsItem1, (Color)ColorConverter.ConvertFromString(borderClientsItem1.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.2);
        }

        private void gridClientsItem1_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderClientsItem1, (Color)ColorConverter.ConvertFromString(borderClientsItem1.BorderBrush.ToString()), Colors.Transparent, 0.2);
        }

        private void gridClientsItem2_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderClientsItem2, (Color)ColorConverter.ConvertFromString(borderClientsItem2.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.2);
        }

        private void gridClientsItem2_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderClientsItem2, (Color)ColorConverter.ConvertFromString(borderClientsItem2.BorderBrush.ToString()), Colors.Transparent, 0.2);
        }
        //-----------------------------------------------------------------------------------

        //Анимация заказы (навигация)
        private void gridOrders_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderOrders, (Color)ColorConverter.ConvertFromString(borderOrders.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.2);
        }

        private void gridOrders_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderOrders, (Color)ColorConverter.ConvertFromString(borderOrders.BorderBrush.ToString()), Colors.Transparent, 0.2);
        }

        private void gridOrders_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SPOrdersItems.Visibility == Visibility.Collapsed)
            {
                //Раскрытие топиков
                Animations.MovingAnimation(SPOrdersItems, new Thickness(0, -30, 0, 0), new Thickness(0, 0, 0, 0), 0.3);
                Animations.RotationAnimation(imgArrowOrders, 0, 180, 0.2);
                SPOrdersItems.Visibility = Visibility.Visible;
                Animations.OpacityAnimation(SPOrdersItems, 0, 1, 0.2);
                Animations.HeightAnimation(SPOrdersItems, 0, 138, 0.2);
            }
            else
            {
                //Сворачивание топиков
                Animations.MovingAnimation(SPOrdersItems, SPOrdersItems.Margin, new Thickness(0, -30, 0, 0), 0.3);
                Animations.RotationAnimation(imgArrowOrders, 180, 0, 0.2);
                Animations.OpacityAnimation(SPOrdersItems, 1, 0, 0.2);
                Animations.HeightAnimation(SPOrdersItems, 138, 0, 0.3, SPOrdersItems);
            }
        }
        //-----------------------------------------------------------------------------------


        //Анимация вложений заказов (навигация)
        private void gridOrdersItem1_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderOrdersItem1, (Color)ColorConverter.ConvertFromString(borderOrdersItem1.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.2);
        }

        private void gridOrdersItem1_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderOrdersItem1, (Color)ColorConverter.ConvertFromString(borderOrdersItem1.BorderBrush.ToString()), Colors.Transparent, 0.2);
        }

        private void gridOrdersItem2_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderOrdersItem2, (Color)ColorConverter.ConvertFromString(borderOrdersItem2.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.2);
        }

        private void gridOrdersItem2_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderOrdersItem2, (Color)ColorConverter.ConvertFromString(borderOrdersItem2.BorderBrush.ToString()), Colors.Transparent, 0.2);
        }
        //-----------------------------------------------------------------------------------



        //Navigation-----------------------------------------------------------------------------------
    }
}