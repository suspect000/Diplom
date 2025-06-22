using CustomControlsForDiplomFramework;
using Dickplom1.DataFolder;
using Dickplom1.Pages.Manager;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

                    case "logs":
                    img.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/Selected/IcnLogsWhite.png", UriKind.Relative));
                    win.MainFrameScrollOff.Navigate(new Pages.Manager.Logs());

                    win.MainFrame.Visibility = Visibility.Collapsed;
                    win.scrollMainWin.Visibility = Visibility.Collapsed;
                    win.MainFrameScrollOff.Visibility = Visibility.Visible;
                    break;
            }            
        }

        // Преобразование фотографии из byte[] -> ImageSource
        public static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;

            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }


        // Преобразование фотографии из ImageSource -> byte[]  
        public static byte[] BitmapImageToByteArray(BitmapImage bitmapImage)
        {
            if (bitmapImage == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder(); // или PngBitmapEncoder
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(ms);
                return ms.ToArray();
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

            //Логи
            win.borderLogs.Background = new SolidColorBrush(Colors.Transparent);
            win.navIcnLogs.Source = new BitmapImage(new Uri("..//Resources/Images/MainWin/Navigation/IcnLogsBlue.png", UriKind.Relative));
            win.navTboxLogs.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#636C7F"));
        }

        public static void SearchSelect()
        {
            var mainWin = Application.Current.MainWindow as MainWindow;
            if (mainWin != null && mainWin.SearchingPage != null)
            {
                if (mainWin.MainFrameScrollOff != null)
                {
                    if (mainWin.MainFrameScrollOff.Content is Pages.Manager.ClientsNaturalPersons)
                        mainWin.SearchingPage = "ClientsNaturalPersons";

                    else if (mainWin.MainFrameScrollOff.Content is Pages.Manager.ClientsLegalEntities)
                        mainWin.SearchingPage = "ClientsLegalEntities";

                    else if (mainWin.MainFrameScrollOff.Content is OrdersNaturalPersons)
                        mainWin.SearchingPage = "OrdersNaturalPersons";

                    else if (mainWin.MainFrameScrollOff.Content is Pages.Manager.OrdersLegalEntities)
                        mainWin.SearchingPage = "OrdersLegalEntities";

                    else if (mainWin.MainFrameScrollOff.Content is Pages.Manager.Staff)
                        mainWin.SearchingPage = "Staff";

                    else if (mainWin.MainFrameScrollOff.Content is Pages.Manager.Subscriptions)
                        mainWin.SearchingPage = "Subscriptions";

                    else if (mainWin.MainFrameScrollOff.Content is Pages.Manager.Logs)
                        mainWin.SearchingPage = "Logs";
                }
            }
        }
    }
}
