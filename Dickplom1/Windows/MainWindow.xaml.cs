using System;
using System.Collections.Generic;
using System.Linq;
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
using Dickplom1.DataFolder;
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
        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Если фокус сейчас в tbSearch, и клик был не по нему — убираем фокус
            if (Keyboard.FocusedElement is TextBox tb && tb.Name == "tbSearch")
            {
                // Пробуем найти, куда именно кликнули
                var clickedElement = Mouse.DirectlyOver as DependencyObject;
                if (clickedElement != null)
                {
                    // Если клик был ВНЕ tbSearch
                    if (!IsDescendantOf(tbSearch, clickedElement))
                    {
/*                        if (tbSearch.Text != "Найти" && string.IsNullOrWhiteSpace(tbSearch.Text))
                            tbSearch.Text = string.Empty;*/
                        // Сброс фокуса — переносим на "пустое" место (например, на окно)
                        FocusManager.SetFocusedElement(this, this);
                    }
                }
            }
        }

        private bool IsDescendantOf(DependencyObject parent, DependencyObject child)
        {
            while (child != null)
            {
                if (child == parent)
                    return true;
                child = VisualTreeHelper.GetParent(child);
            }
            return false;
        }
        public string SearchingPage { get; set; } = string.Empty; 
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
                tbSearch.Padding = new Thickness(45, 0, 0, 0);
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

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var mainWin = Application.Current.MainWindow as MainWindow;

            if (mainWin.MainFrameScrollOff != null)
            {
                if (mainWin.MainFrameScrollOff.Content is Pages.Manager.ClientsNaturalPersons clientsNatural) // Для клиентов (физ. лиц)
                {
                    

                    if (tbSearch.Text != "Найти" && !string.IsNullOrWhiteSpace(tbSearch.Text))
                    {
                        if (btnSearch != null)
                        {
                            btnSearch.Visibility = Visibility.Visible;

                        }
                    }
                    else if (string.IsNullOrWhiteSpace(tbSearch.Text))
                    {
                        btnSearch.Visibility = Visibility.Collapsed;
                        if (clientsNatural != null)
                        {
                            if (clientsNatural.ComboboxesFilter.firstCombobox.SelectedIndex == -1 || clientsNatural.ComboboxesFilter.firstCombobox.SelectedIndex == 0)
                                clientsNatural.RefreshItemsList();
                            else
                                clientsNatural.ApplyFilters();
                        }
                    }
                }
                if (mainWin.MainFrameScrollOff.Content is Pages.Manager.ClientsLegalEntities clientsLegal) // Для клиентов (юр. лиц)
                {
                    if (tbSearch.Text != "Найти" && !string.IsNullOrWhiteSpace(tbSearch.Text))
                    {
                        if (btnSearch != null)
                        {
                            btnSearch.Visibility = Visibility.Visible;

                        }
                    }
                    else if (string.IsNullOrWhiteSpace(tbSearch.Text))
                    {
                        btnSearch.Visibility = Visibility.Collapsed;
                        if (clientsLegal != null)
                        {
                            if (clientsLegal.ComboboxesFilter.firstCombobox.SelectedIndex == -1 || clientsLegal.ComboboxesFilter.firstCombobox.SelectedIndex == 0)
                                clientsLegal.RefreshItemsList();
                            else
                                clientsLegal.ApplyFilters();
                        }
                    }
                }
                if (mainWin.MainFrameScrollOff.Content is Pages.Manager.OrdersNaturalPersons ordersNatural) // Для заказов (физ. лиц)
                {
                    if (tbSearch.Text != "Найти" && !string.IsNullOrWhiteSpace(tbSearch.Text))
                    {
                        if (btnSearch != null)
                        {
                            btnSearch.Visibility = Visibility.Visible;

                        }
                    }
                    else if (string.IsNullOrWhiteSpace(tbSearch.Text))
                    {
                        btnSearch.Visibility = Visibility.Collapsed;
                        if (ordersNatural != null)
                        {
                            if (ordersNatural.ComboboxesFilter.firstCombobox.SelectedIndex == -1 | ordersNatural.ComboboxesFilter.firstCombobox.SelectedIndex == 0
                                && ordersNatural.comboboxStatusValue == -1 | ordersNatural.comboboxStatusValue == 0)
                                ordersNatural.RefreshItems();
                            else
                                ordersNatural.ApplyFilters();
                        }
                    }
                }
                if (mainWin.MainFrameScrollOff.Content is Pages.Manager.OrdersLegalEntities ordersLegal) // Для заказов (юр. лиц)
                {
                    if (tbSearch.Text != "Найти" && !string.IsNullOrWhiteSpace(tbSearch.Text))
                    {
                        if (btnSearch != null)
                        {
                            btnSearch.Visibility = Visibility.Visible;

                        }
                    }
                    else if (string.IsNullOrWhiteSpace(tbSearch.Text))
                    {
                        btnSearch.Visibility = Visibility.Collapsed;
                        if (ordersLegal != null)
                        {
                            if (ordersLegal.ComboboxesFilter.firstCombobox.SelectedIndex == -1 |ordersLegal.ComboboxesFilter.firstCombobox.SelectedIndex == 0
                                && ordersLegal.comboboxStatusValue == -1 | ordersLegal.comboboxStatusValue == 0)
                                ordersLegal.ItemsRefresh();
                            else
                                ordersLegal.ApplyFilters();
                        }
                    }
                }
                if (mainWin.MainFrameScrollOff.Content is Pages.Manager.Subscriptions subs) // Для заказов (юр. лиц)
                {
                    if (tbSearch.Text != "Найти" && !string.IsNullOrWhiteSpace(tbSearch.Text))
                    {
                        if (btnSearch != null)
                        {
                            btnSearch.Visibility = Visibility.Visible;

                        }
                    }
                    else if (string.IsNullOrWhiteSpace(tbSearch.Text))
                    {
                        btnSearch.Visibility = Visibility.Collapsed;
                        if (subs != null)
                        {
                            if (subs.ComboboxesFilter.firstCombobox.SelectedIndex == -1 | subs.ComboboxesFilter.firstCombobox.SelectedIndex == 0
                                && subs.comboboxTypeValue == -1 | subs.comboboxTypeValue == 0)
                                subs.ItemsRefresh();
                            else
                                subs.ApplyFilters();
                        }
                    }
                }
                if (mainWin.MainFrameScrollOff.Content is Pages.Manager.Staff staff) // Для заказов (юр. лиц)
                {
                    if (tbSearch.Text != "Найти" && !string.IsNullOrWhiteSpace(tbSearch.Text))
                    {
                        if (btnSearch != null)
                        {
                            btnSearch.Visibility = Visibility.Visible;

                        }
                    }
                    else if (string.IsNullOrWhiteSpace(tbSearch.Text))
                    {
                        btnSearch.Visibility = Visibility.Collapsed;
                        if (staff != null)
                        {
                            if (staff.ComboboxesFilter.firstCombobox.SelectedIndex == -1 || staff.ComboboxesFilter.firstCombobox.SelectedIndex == 0)
                                staff.RefreshItems();
                            else
                                staff.ApplyFilters();
                        }
                    }
                }
            }
        }

        private void btnSearch_Loaded(object sender, RoutedEventArgs e)
        {
            btnSearch.btnWithBorder.Click += BtnWithBorder_Click;
        }

        private void BtnWithBorder_Click(object sender, RoutedEventArgs e)
        {                    
            //Поиск на странице
            if (tbSearch.Text != "Найти" && !string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                string searchQuery = tbSearch.Text?.ToLower() ?? "";
                if (SearchingPage != string.Empty)
                { 
                    try
                    {
                        var context = DBEntities.GetContext();
                        var mainWin = Application.Current.MainWindow as MainWindow;

                        if (mainWin != null && mainWin.SearchingPage != string.Empty)
                        {
                            if (mainWin.SearchingPage == "ClientsNaturalPersons")
                            {
                                var selectedPage = mainWin.MainFrameScrollOff.Content as Pages.Manager.ClientsNaturalPersons;

                                if (selectedPage != null)
                                {
                                    selectedPage.allClients = selectedPage.allClients
                                        .Where(w =>
                                        (w.FullName != null && w.FullName.ToLower().Contains(searchQuery)) ||
                                        (w.Email != null && w.Email.ToLower().Contains(searchQuery)) ||
                                        (w.PhoneNumber != null && w.PhoneNumber.ToLower().Contains(searchQuery)))
                                        .ToList();

                                    selectedPage.SetPaggination(); 
                                }
                            }
                        }
                        if (mainWin.SearchingPage == "ClientsLegalEntities")
                        {
                            var selectedPage = mainWin.MainFrameScrollOff.Content as Pages.Manager.ClientsLegalEntities;

                            if (selectedPage != null)
                            {
                                selectedPage.allClientsLegal = selectedPage.allClientsLegal
                                    .Where(w => 
                                    w.FullName.ToLower().Contains(searchQuery)
                                    || w.Email.ToLower().Contains(searchQuery)
                                    || context.ClientsLegalEntitiesContactPerson.FirstOrDefault(f=>
                                    f.IsActive == true && f.ClientsLegalEntitiesCompanyData.CompanyName == w.CompanyName).Phone
                                    .ToLower().Contains(searchQuery)
                                    || w.CompanyName.ToLower().Contains(searchQuery))
                                    .ToList();

                                selectedPage.SetPaggination();
                            }
                        }
                        if (mainWin.SearchingPage == "OrdersNaturalPersons")
                        {
                            var selectedPage = mainWin.MainFrameScrollOff.Content as Pages.Manager.OrdersNaturalPersons;

                            if (selectedPage != null)
                            {
                                selectedPage.allOrders = selectedPage.allOrders
                                    .Where(w =>
                                    w.SubscriptionName.ToLower().Contains(searchQuery)
                                    || w.FullNameClient.ToLower().Contains(searchQuery)
                                    || w.StartDate.ToLower().Contains(searchQuery)
                                    || w.EndDate.ToLower().Contains(searchQuery)
                                    || context.ClientsNaturalPersons.FirstOrDefault(f=>f.ClientNaturalPersonsId == w.ClientId).Email.ToLower().Contains(searchQuery)
                                    || context.ClientsNaturalPersons.FirstOrDefault(f => f.ClientNaturalPersonsId == w.ClientId).PhoneNumber.ToLower().Contains(searchQuery))
                                    .ToList();

                                selectedPage.SetPaggination();
                            }
                        }
                        if (mainWin.SearchingPage == "OrdersLegalEntities")
                        {
                            var selectedPage = mainWin.MainFrameScrollOff.Content as Pages.Manager.OrdersLegalEntities;

                            if (selectedPage != null)
                            {
                                selectedPage.allOrders = selectedPage.allOrders
                                    .Where(w =>
                                    w.SubscriptionName.ToLower().Contains(searchQuery)
                                    || w.FullNameClient.ToLower().Contains(searchQuery)
                                    || w.StartDate.ToLower().Contains(searchQuery)
                                    || w.EndDate.ToLower().Contains(searchQuery)
                                    || w.CompanyName.ToLower().Contains(searchQuery)
                                    || context.ClientsLegalEntitiesContactPerson.FirstOrDefault(f=>f.IsActive == true && f.ClientsLegalEntitiesCompanyData.CompanyName == w.CompanyName).Email.ToLower().Contains(searchQuery)
                                    || context.ClientsLegalEntitiesContactPerson.FirstOrDefault(f => f.IsActive == true && f.ClientsLegalEntitiesCompanyData.CompanyName == w.CompanyName).Phone.ToLower().Contains(searchQuery))
                                    .ToList();

                                selectedPage.SetPaggination();
                            }
                        }
                        if (mainWin.SearchingPage == "Subscriptions")
                        {
                            var selectedPage = mainWin.MainFrameScrollOff.Content as Pages.Manager.Subscriptions;

                            if (selectedPage != null)
                            {
                                selectedPage.allSubscriptions = selectedPage.allSubscriptions
                                    .Where(w =>
                                    w.SubscriptionName.ToLower().Contains(searchQuery)
                                    || w.SubscriptionPeriod.ToLower().Contains(searchQuery)
                                    || w.PriceForMonth.ToLower().Contains(searchQuery)
                                    || w.PriceFull.ToLower().Contains(searchQuery)
                                    || w.Comment.ToLower().Contains(searchQuery))
                                    .ToList();

                                selectedPage.SetPaggination();
                            }
                        }
                        if (mainWin.SearchingPage == "Staff")
                        {
                            var selectedPage = mainWin.MainFrameScrollOff.Content as Pages.Manager.Staff;

                            if (selectedPage != null)
                            {
                                selectedPage.allStaff = selectedPage.allStaff
                                    .Where(w =>
                                    w.FIOStaff.ToLower().Contains(searchQuery)
                                    || w.Email.ToLower().Contains(searchQuery)
                                    || w.PhoneNumber.ToLower().Contains(searchQuery))
                                    .ToList();

                                selectedPage.SetPaggination();
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        //-----------------------------------------------------------------------------------
    }
}