using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Xml;
using Dickplom1.Class;
using Dickplom1.Pages.Manager;


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

            Musor.Navigation("dashboards", borderDashboard, navImgDashboards, navTboxDashboards);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (gridSelectReportsToMake.Visibility == Visibility.Visible)
            {
                if (!gridSelectReportsToMake.IsMouseOver && !spReports.IsMouseOver)
                {
                    Animations.MinimazedReports(ImgReportsArrowDown, gridSelectReportsToMake);
                }
            }

            if (gridSelectWindowThemes.Visibility == Visibility.Visible)
            {
                if (!gridSelectWindowThemes.IsMouseOver && !gridWindowThemes.IsMouseOver)
                {
                    gridSelectWindowThemes.Visibility = Visibility.Collapsed;
                }
            }

            if (gridMiniProfile.Visibility == Visibility.Visible)
            {
                if (!gridMiniProfile.IsMouseOver)
                {
                    gridMiniProfile.Visibility = Visibility.Collapsed;
                    gridSelectWindowThemes.Visibility = Visibility.Collapsed;
                }
            }

            if (gridNotifications.Visibility == Visibility.Visible)
            {
                if (!gridNotifications.IsMouseOver && !btnNotification.IsMouseOver)
                {
                    gridNotifications.Visibility = Visibility.Collapsed;
                }
            }

        }

        //Header
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        { 
            Musor.HideElement(gridMiniProfile);
            Musor.HideElement(gridSelectWindowThemes);
            Musor.HideElement(gridNotifications);

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
                            (navBorderClients, (Color)ColorConverter.ConvertFromString(navBorderClients.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }
        private void gridClients_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                            (navBorderClients, (Color)ColorConverter.ConvertFromString(navBorderClients.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }

        private void gridClients_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SPClientsItems.Visibility == Visibility.Collapsed)
            {
                //Сворачивание всех остальных топиков
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

        private void gridOrders_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SPOrdersItems.Visibility == Visibility.Collapsed)
            {
                //Сворачивание всех остальных топиков
                if (SPClientsItems.Visibility == Visibility.Visible)
                    Animations.MinimazedNavTopics(SPClientsItems, imgArrowClients);

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


        //Анимация подписок (навигация)
        private void gridServices_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderSubscriptions, (Color)ColorConverter.ConvertFromString(borderSubscriptions.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.15);
        }

        private void gridServices_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (borderSubscriptions, (Color)ColorConverter.ConvertFromString(borderSubscriptions.BorderBrush.ToString()), Colors.Transparent, 0.35);
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
            Animations.MakeAnimBackground(btnMakeReportOrders, (Color)ColorConverter.ConvertFromString(btnMakeReportOrders.Background.ToString()), (Color)ColorConverter.ConvertFromString("#E8E8E8"), 0.15);
        }

        private void btnMakeReportOrders_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.MakeAnimBackground(btnMakeReportOrders, (Color)ColorConverter.ConvertFromString(btnMakeReportOrders.Background.ToString()), Colors.Transparent, 0.3);
        }

        private void btnMakeReportStaff_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.MakeAnimBackground(btnMakeReportStaff, (Color)ColorConverter.ConvertFromString(btnMakeReportStaff.Background.ToString()), (Color)ColorConverter.ConvertFromString("#E8E8E8"), 0.15);
        }

        private void btnMakeReportStaff_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.MakeAnimBackground(btnMakeReportStaff, (Color)ColorConverter.ConvertFromString(btnMakeReportStaff.Background.ToString()), Colors.Transparent, 0.3);
        }
        //-----------------------------------------------------------------------------------

        //Минипрофиль вложенные топики(Header)
        private void gridMiniProfileWindowThemes_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.MakeAnimBackground
                            (borderWindowThemes, (Color)ColorConverter.ConvertFromString(borderWindowThemes.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#ECEDF1"), 0.15);
        }

        private void gridMiniProfileWindowThemes_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.MakeAnimBackground
                            (borderWindowThemes, (Color)ColorConverter.ConvertFromString(borderWindowThemes.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }

        private void gridMiniProfileExit_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.MakeAnimBackground
                            (borderMiniProfileExit, (Color)ColorConverter.ConvertFromString(borderMiniProfileExit.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#ECEDF1"), 0.15);
        }

        private void gridMiniProfileExit_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.MakeAnimBackground
                            (borderMiniProfileExit, (Color)ColorConverter.ConvertFromString(borderMiniProfileExit.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }

        private void gridWindowThemes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (gridSelectWindowThemes.Visibility != Visibility.Visible)
            {
                Animations.OpacityAnimation(gridSelectWindowThemes, 0, 1, 0.15);
                gridSelectWindowThemes.Visibility = Visibility.Visible;
            }
        }

        //Минипрофиль топики выбора темы (Header)
        private void gridSetWhiteTheme_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.MakeAnimBackground
                            (borderSetWhiteTheme, (Color)ColorConverter.ConvertFromString(borderSetWhiteTheme.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#E8E8E8"), 0.15);
        }

        private void gridSetWhiteTheme_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.MakeAnimBackground
                            (borderSetWhiteTheme, (Color)ColorConverter.ConvertFromString(borderSetWhiteTheme.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }

        private void gridSetBlackTheme_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.MakeAnimBackground
                            (borderSetBlackTheme, (Color)ColorConverter.ConvertFromString(borderSetBlackTheme.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#E8E8E8"), 0.15);
        }

        private void gridSetBlackTheme_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.MakeAnimBackground
                            (borderSetBlackTheme, (Color)ColorConverter.ConvertFromString(borderSetBlackTheme.BorderBrush.ToString()), Colors.Transparent, 0.35);
        }
            //-----------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------

        //Переключатель тем окна (Header)
        private void gridSetWhiteTheme_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Musor.HideElement(gridSelectWindowThemes);
            rbtnBlackTheme.IsChecked = false;
            rbtnWhiteTheme.IsChecked = true;
        }

        private void gridSetBlackTheme_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Musor.HideElement(gridSelectWindowThemes);
            rbtnWhiteTheme.IsChecked = false;
            rbtnBlackTheme.IsChecked = true;
        }
        //-----------------------------------------------------------------------------------

        //Уведомления (Header)
        private void btnNotification_Click(object sender, RoutedEventArgs e)
        {
            if (gridNotifications.Visibility != Visibility.Visible)
            {
                Animations.OpacityAnimation(gridNotifications, 0, 1, 0.15);
                Musor.ShowElement(gridNotifications);

                if (gridMiniProfile.Visibility == Visibility.Visible)
                {
                     Musor.HideElement(gridMiniProfile);
                }
            }
        }
        //-----------------------------------------------------------------------------------

        //Кнопка минипрофиля (открыть) (Header)
        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            if (gridMiniProfile.Visibility != Visibility.Visible)
            {
                Animations.OpacityAnimation(gridMiniProfile, 0, 1, 0.15);
                Musor.ShowElement(gridMiniProfile);

                if (gridNotifications.Visibility == Visibility.Visible)
                {
                    Musor.HideElement(gridNotifications);
                    Musor.HideElement(gridSelectWindowThemes);
                }
            }
        }

        private void gridDashboard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Musor.Navigation("dashboards", borderDashboard, navImgDashboards, navTboxDashboards);
        }

        private void gridClientsItem1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Musor.Navigation("clientsNatural", borderClientsItem1, navIcnClientNaturalPerson, navTboxClientNaturalPerson);

        }

        private void gridClientsItem2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Musor.Navigation("clientsLegal", borderClientsItem2, navIcnClientLegalEntities, navTboxClientLegalEntities);

        }

        private void gridOrdersItem1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Musor.Navigation("ordersNatural", borderOrdersItem1, navIcnOrdersNaturalPersons, navTboxOrdersNaturalPersons);
        }

        private void gridOrdersItem2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Musor.Navigation("ordersLegal", borderOrdersItem2, navIcnOrderLegalEntities, navTboxOrdersLegalEntities);
        }

        private void gridServices_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Musor.Navigation("subscriptions", borderSubscriptions, navIcnSubscriptions, navTboxSubscriptions);
        }

        private void gridStaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Musor.Navigation("staff", borderStaff, navIcnStaff, navTboxStaff);
        }
        //-----------------------------------------------------------------------------------
    }
}