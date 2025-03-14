using System;
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
                Animations.WidthAnimation(tbSearch, tbSearch.Width, 610, 0.3);
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
                (tbSearch, (Color)ColorConverter.ConvertFromString(tbSearch.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }

        private void tbSearch_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (tbSearch, (Color)ColorConverter.ConvertFromString(tbSearch.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }

        //Анимация кнопки уведомлений
        private void btnNotification_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (btnNotification, (Color)ColorConverter.ConvertFromString(btnNotification.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }

        private void btnNotification_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (btnNotification, (Color)ColorConverter.ConvertFromString(btnNotification.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }

        //Анимация кнопки профиля
        private void btnProfile_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (btnProfile, (Color)ColorConverter.ConvertFromString(btnProfile.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }

        private void btnProfile_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (btnProfile, (Color)ColorConverter.ConvertFromString(btnProfile.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }
        //Header-----------------------------------------------------------------------------------


        //Navigation
        //Анимация навигации (Аналитика)
        private void gridDashboard_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                            (borderDashboard, (Color)ColorConverter.ConvertFromString(borderDashboard.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }

        private void gridDashboard_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                            (borderDashboard, (Color)ColorConverter.ConvertFromString(borderDashboard.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }
        //-----------------------------------------------------------------------------------

        //Анимация клиентов (навигация)
        private void gridClients_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                            (borderClients, (Color)ColorConverter.ConvertFromString(borderClients.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }
        private void gridClients_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                            (borderClients, (Color)ColorConverter.ConvertFromString(borderClients.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }

        private void gridClients_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SPClientsItems.Visibility == Visibility.Collapsed)
            {
                //Сворачивание всех остальных топиков
                if (SPServicesItems.Visibility == Visibility.Visible)
                    Animations.MinimazedNavTopics(SPServicesItems, imgArrowServices);

                if (SPOrdersItems.Visibility == Visibility.Visible)
                    Animations.MinimazedNavTopics(SPOrdersItems, imgArrowOrders);

                //Раскрытие топиков
                Animations.MaximazedNavTopics(SPClientsItems, imgArrowClients);
            }
            else
            {
                //Сворачивание топиков
                Animations.MinimazedNavTopics(SPClientsItems, imgArrowClients);
            }
        }
        //-----------------------------------------------------------------------------------

        //Анимация вложений клиентов (навигация)
        private void gridClientsItem1_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderClientsItem1, (Color)ColorConverter.ConvertFromString(borderClientsItem1.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }

        private void gridClientsItem1_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderClientsItem1, (Color)ColorConverter.ConvertFromString(borderClientsItem1.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }

        private void gridClientsItem2_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderClientsItem2, (Color)ColorConverter.ConvertFromString(borderClientsItem2.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }

        private void gridClientsItem2_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderClientsItem2, (Color)ColorConverter.ConvertFromString(borderClientsItem2.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }
        //-----------------------------------------------------------------------------------

        //Анимация заказы (навигация)
        private void gridOrders_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderOrders, (Color)ColorConverter.ConvertFromString(borderOrders.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }

        private void gridOrders_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderOrders, (Color)ColorConverter.ConvertFromString(borderOrders.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }

        private void gridOrders_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SPOrdersItems.Visibility == Visibility.Collapsed)
            {
                //Сворачивание всех остальных топиков
                if (SPClientsItems.Visibility == Visibility.Visible)
                    Animations.MinimazedNavTopics(SPClientsItems, imgArrowClients);

                if (SPServicesItems.Visibility == Visibility.Visible)
                    Animations.MinimazedNavTopics(SPServicesItems, imgArrowServices);
                //Раскрытие топиков
                Animations.MaximazedNavTopics(SPOrdersItems, imgArrowOrders);
            }
            else
            {
                //Сворачивание топиков
                Animations.MinimazedNavTopics(SPOrdersItems, imgArrowOrders);
            }
        }
        //-----------------------------------------------------------------------------------


        //Анимация вложений заказов (навигация)
        private void gridOrdersItem1_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderOrdersItem1, (Color)ColorConverter.ConvertFromString(borderOrdersItem1.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }

        private void gridOrdersItem1_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderOrdersItem1, (Color)ColorConverter.ConvertFromString(borderOrdersItem1.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }

        private void gridOrdersItem2_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderOrdersItem2, (Color)ColorConverter.ConvertFromString(borderOrdersItem2.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }

        private void gridOrdersItem2_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderOrdersItem2, (Color)ColorConverter.ConvertFromString(borderOrdersItem2.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }
        //-----------------------------------------------------------------------------------


        //Анимация услуг (навигация)
        private void gridServices_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderServices, (Color)ColorConverter.ConvertFromString(borderServices.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }

        private void gridServices_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderServices, (Color)ColorConverter.ConvertFromString(borderServices.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }

        private void gridServices_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SPServicesItems.Visibility == Visibility.Collapsed)
            {
                //Сворачивание всех остальных топиков
                if (SPClientsItems.Visibility == Visibility.Visible)
                    Animations.MinimazedNavTopics(SPClientsItems, imgArrowClients);

                if (SPOrdersItems.Visibility == Visibility.Visible)
                    Animations.MinimazedNavTopics(SPOrdersItems, imgArrowOrders);

                //Раскрытие топиков
                Animations.MaximazedNavTopics(SPServicesItems, imgArrowServices);
            }
            else
            {
                //Сворачивание топиков
                Animations.MinimazedNavTopics(SPServicesItems, imgArrowServices);
            }
        }
        //-----------------------------------------------------------------------------------

        //Анимация вложений услуг (навигация)
        private void gridServicesItem1_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderServicesItem1, (Color)ColorConverter.ConvertFromString(borderServicesItem1.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }

        private void gridServicesItem1_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderServicesItem1, (Color)ColorConverter.ConvertFromString(borderServicesItem1.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }

        private void gridServicesItem2_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderServicesItem2, (Color)ColorConverter.ConvertFromString(borderServicesItem2.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }

        private void gridServicesItem2_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderServicesItem2, (Color)ColorConverter.ConvertFromString(borderServicesItem2.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }
        //-----------------------------------------------------------------------------------

        //Анимация сотрудников (навигация)
        private void gridStaff_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderStaff, (Color)ColorConverter.ConvertFromString(borderStaff.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }

        private void gridStaff_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderStaff, (Color)ColorConverter.ConvertFromString(borderStaff.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }
        //-----------------------------------------------------------------------------------
        //Navigation-----------------------------------------------------------------------------------


        //Закрепленные задачи (нижняя навигация)
        private void spZakrepGoToOrders_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.OpacityAnimation(spZakrepGoToOrders, spZakrepGoToOrders.Opacity, 0.3, 0.3);
        }

        private void spZakrepGoToOrders_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.OpacityAnimation(spZakrepGoToOrders, spZakrepGoToOrders.Opacity, 0.5, 0.3);
        }
        //-----------------------------------------------------------------------------------

        //Отчеты (Header)
        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.OpacityAnimation(spReports, spReports.Opacity, 0.5, 0.3);
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.OpacityAnimation(spReports, spReports.Opacity, 1, 0.3);
        }

        private void spReports_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (gridSelectReportsToMake.Visibility == Visibility.Collapsed)
                Animations.MaximazedReports(ImgReportsArrowDown, gridSelectReportsToMake);
            else
                Animations.MinimazedReports(ImgReportsArrowDown, gridSelectReportsToMake);
        }
        //-----------------------------------------------------------------------------------

        //Отчеты топики (Header)
        private void btnMakeReportOrders_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBackgroundBrush(btnMakeReportOrders, (Color)ColorConverter.ConvertFromString(btnMakeReportOrders.Background.ToString()), (Color)ColorConverter.ConvertFromString("#E8E8E8"), 0.15);
        }

        private void btnMakeReportOrders_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBackgroundBrush(btnMakeReportOrders, (Color)ColorConverter.ConvertFromString(btnMakeReportOrders.Background.ToString()), Colors.Transparent, 0.3);
        }

        private void btnMakeReportStaff_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBackgroundBrush(btnMakeReportStaff, (Color)ColorConverter.ConvertFromString(btnMakeReportStaff.Background.ToString()), (Color)ColorConverter.ConvertFromString("#E8E8E8"), 0.15);
        }

        private void btnMakeReportStaff_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBackgroundBrush(btnMakeReportStaff, (Color)ColorConverter.ConvertFromString(btnMakeReportStaff.Background.ToString()), Colors.Transparent, 0.3);
        }
        //-----------------------------------------------------------------------------------
    }
}