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
    /// Логика взаимодействия для SubscriptionAddWin.xaml
    /// </summary>
    public partial class SubscriptionAddWin : Window
    {
        public SubscriptionAddWin()
        {
            InitializeComponent();
        }
        public int SubscriptionId { get; set; } = 0;

        private void mainGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            gridFocus.Focus();
            Keyboard.ClearFocus();
        }

        private void gridMovingWin_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void TextboxWithBorder_Loaded(object sender, RoutedEventArgs e)
        {
            tboxComment.tb.Height = 215;
            Padding = new Thickness(5,5,5,5);
            tboxComment.tb.VerticalAlignment = VerticalAlignment.Top;
        }
    }
}
