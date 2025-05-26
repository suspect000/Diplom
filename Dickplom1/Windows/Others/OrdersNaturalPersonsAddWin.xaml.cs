using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dickplom1.Windows.Others
{
    /// <summary>
    /// Логика взаимодействия для OrdersNaturalPersonsAddWin.xaml
    /// </summary>
    public partial class OrdersNaturalPersonsAddWin : Window
    {
        public OrdersNaturalPersonsAddWin()
        {
            InitializeComponent();
        }

        private void gridMovingWin_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void mainGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            gridFocus.Focus();
            Keyboard.ClearFocus();
        }

/*        private void cboxSubscription_Loaded(object sender, RoutedEventArgs e)
        {
            cboxSubscription.Text = "Выберите подписку";
        }

        private void cboxClient_Loaded(object sender, RoutedEventArgs e)
        {
            cboxClient.Text = "Выберите клиента";
        }

        private void orderStatus_Loaded(object sender, RoutedEventArgs e)
        {
            cboxOrderStatus.Text = "Выберите статус заказа";
        }*/
    }
}
