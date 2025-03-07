using Dickplom1.Class;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void ExpanderHeader_Click(object sender, MouseButtonEventArgs e) // Экспандер в навигации
        {
            if (sender is Expander expander)
            {
                expander.IsExpanded = !expander.IsExpanded;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HidenRect.Focus();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text == "Найти")
            {
                tbSearch.Text = "";
                imgLupa.Visibility = Visibility.Collapsed;
                Animations.WidthAnimation(tbSearch, tbSearch.Width, 610);
            }
        }

        private void tbSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                tbSearch.Text = "Найти";
                imgLupa.Visibility = Visibility.Visible;
                Animations.WidthAnimation(tbSearch, tbSearch.Width, 228);
            }
        }

        private void tbSearch_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (tbSearch, (Color)ColorConverter.ConvertFromString(tbSearch.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.3);
        }
    }
}