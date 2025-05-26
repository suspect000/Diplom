using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dickplom1.Class
{
    public class Musor
    {
        public static void HideElement(UIElement element)
        {
            element.Visibility = Visibility.Collapsed;
        }
        public static void ShowElement(UIElement element)
        {
            element.Visibility = Visibility.Visible;
        }

        public static void Navigation(string name, Border border, System.Windows.Controls.Image img, TextBlock tbox)
        {
            var win = Application.Current.MainWindow as MainWindow;
            OffOtherNavigation();

            border.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#C3C7D2"));
            tbox.Foreground = new SolidColorBrush(Colors.White);


            switch (name)
            {
                case "dashboards":
                    img.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/Selected/IcnDashbordWhite.png", UriKind.Relative));
                    win.MainFrame.Navigate(new Pages.Manager.Dashboards());

                    win.MainFrame.Visibility = Visibility.Visible;
                    win.scrollMainWin.Visibility = Visibility.Visible;
                    win.MainFrameScrollOff.Visibility = Visibility.Collapsed;
                    break;

                    case "clientsNatural":
                    img.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/Selected/IcnClientsWhite.png", UriKind.Relative));
                    win.MainFrameScrollOff.Navigate(new Pages.Manager.ClientsNaturalPersons());

                    win.MainFrame.Visibility = Visibility.Collapsed;
                    win.scrollMainWin.Visibility = Visibility.Collapsed;
                    win.MainFrameScrollOff.Visibility = Visibility.Visible;
                    break;

                    case "clientsLegal":
                    img.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/Selected/IcnClientsWhite.png", UriKind.Relative));
                    win.MainFrameScrollOff.Navigate(new Pages.Manager.ClientsLegalEntities());

                    win.MainFrame.Visibility = Visibility.Collapsed;
                    win.scrollMainWin.Visibility = Visibility.Collapsed;
                    win.MainFrameScrollOff.Visibility = Visibility.Visible;
                    break;

                    case "ordersNatural":
                    img.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/Selected/IcnOrdersWhite.png", UriKind.Relative));
                    win.MainFrameScrollOff.Navigate(new Pages.Manager.OrdersNaturalPersons());

                    win.MainFrame.Visibility = Visibility.Collapsed;
                    win.scrollMainWin.Visibility = Visibility.Collapsed;
                    win.MainFrameScrollOff.Visibility = Visibility.Visible;
                    break;

                    case "ordersLegal":
                    img.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/Selected/IcnOrdersWhite.png", UriKind.Relative));
                    win.MainFrameScrollOff.Navigate(new Pages.Manager.OrdersLegalEntities());

                    win.MainFrame.Visibility = Visibility.Collapsed;
                    win.scrollMainWin.Visibility = Visibility.Collapsed;
                    win.MainFrameScrollOff.Visibility = Visibility.Visible;
                    break;

                    case "subscriptions":
                    img.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/Selected/IcnSubscritionsWhite.png", UriKind.Relative));
                    win.MainFrameScrollOff.Navigate(new Pages.Manager.Subscriptions());

                    win.MainFrame.Visibility = Visibility.Collapsed;
                    win.scrollMainWin.Visibility = Visibility.Collapsed;
                    win.MainFrameScrollOff.Visibility = Visibility.Visible;
                    break;

                    case "staff":
                    img.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/Selected/IcnStaffWhite.png", UriKind.Relative));
                    win.MainFrameScrollOff.Navigate(new Pages.Manager.Staff());

                    win.MainFrame.Visibility = Visibility.Collapsed;
                    win.scrollMainWin.Visibility = Visibility.Collapsed;
                    win.MainFrameScrollOff.Visibility = Visibility.Visible;
                    break;
            }

            
        }
        public static void OffOtherNavigation()
        {
            var win = Application.Current.MainWindow as MainWindow;

            //Дашборды
            win.borderDashboard.Background = new SolidColorBrush(Colors.Transparent);
            win.navImgDashboards.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/IcnAnalBlue.png", UriKind.Relative));
            win.navTboxDashboards.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#636C7F"));

            //Клиенты
            win.borderClientsItem1.Background = new SolidColorBrush(Colors.Transparent);
            win.navIcnClientNaturalPerson.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/IcnClients.png", UriKind.Relative));
            win.navTboxClientNaturalPerson.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#636C7F"));

            win.borderClientsItem2.Background = new SolidColorBrush(Colors.Transparent);
            win.navIcnClientLegalEntities.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/IcnClients.png", UriKind.Relative));
            win.navTboxClientLegalEntities.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#636C7F"));

            //Заказы
            win.borderOrdersItem1.Background = new SolidColorBrush(Colors.Transparent);
            win.navIcnOrdersNaturalPersons.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/IcnOrder.png", UriKind.Relative));
            win.navTboxOrdersNaturalPersons.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#636C7F"));

            win.borderOrdersItem2.Background = new SolidColorBrush(Colors.Transparent);
            win.navIcnOrderLegalEntities.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/IcnOrder.png", UriKind.Relative));
            win.navTboxOrdersLegalEntities.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#636C7F"));

            //Подписки
            win.borderSubscriptions.Background = new SolidColorBrush(Colors.Transparent);
            win.navIcnSubscriptions.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/IcnServices.png", UriKind.Relative));
            win.navTboxSubscriptions.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#636C7F"));

            //Сотрудники
            win.borderStaff.Background = new SolidColorBrush(Colors.Transparent);
            win.navIcnStaff.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/IcnStaff.png", UriKind.Relative));
            win.navTboxStaff.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#636C7F"));
        }
    }
}
