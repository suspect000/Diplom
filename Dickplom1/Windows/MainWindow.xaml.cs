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


        //Анимация tBox поисковика
        private void tbSearch_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (tbSearch, (Color)ColorConverter.ConvertFromString(tbSearch.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.3);
        }

        private void tbSearch_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (tbSearch, (Color)ColorConverter.ConvertFromString(tbSearch.BorderBrush.ToString()), Colors.Transparent, 0.3);
        }
        //-----------------------------------------------------------------------------------



        //Анимация кнопки уведомлений
        private void btnNotification_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (btnNotification, (Color)ColorConverter.ConvertFromString(btnNotification.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.3);
        }

        private void btnNotification_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (btnNotification, (Color)ColorConverter.ConvertFromString(btnNotification.BorderBrush.ToString()), Colors.Transparent, 0.3);
        }
        //-----------------------------------------------------------------------------------



        //Анимация кнопки профиля
        private void btnProfile_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (btnProfile, (Color)ColorConverter.ConvertFromString(btnProfile.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.3);
        }

        private void btnProfile_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (btnProfile, (Color)ColorConverter.ConvertFromString(btnProfile.BorderBrush.ToString()), Colors.Transparent, 0.3);
        }
        //-----------------------------------------------------------------------------------



        //Анимация навигации (Аналитика)
        private void ListBoxItem_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                            (lIDashboard, (Color)ColorConverter.ConvertFromString(lIDashboard.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.3);
        }

        private void ListBoxItem_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (lIDashboard, (Color)ColorConverter.ConvertFromString(lIDashboard.BorderBrush.ToString()), Colors.Transparent, 0.3);
        }
        //-----------------------------------------------------------------------------------



        //Анимация навигации (Клиенты)
        private void Expander_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (expClients, (Color)ColorConverter.ConvertFromString(expClients.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.3);
        }

        private void expClients_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (expClients, (Color)ColorConverter.ConvertFromString(expClients.BorderBrush.ToString()), Colors.Transparent, 0.3);
        }
        //-----------------------------------------------------------------------------------




        //Анимация навигации (Клиенты дочерка 1)
        private void LBIClient1_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush(LBIClient1, (Color)ColorConverter.ConvertFromString(expClients.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.3);
        }

        private void LBIClient1_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush(LBIClient1, (Color)ColorConverter.ConvertFromString(expClients.BorderBrush.ToString()), Colors.Transparent, 0.3);
        }
        //-----------------------------------------------------------------------------------



        //Анимация навигации (Клиенты дочерка 2)
        private void LBIClient2_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush(LBIClient2, (Color)ColorConverter.ConvertFromString(expClients.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.3);
        }

        private void LBIClient2_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush(LBIClient2, (Color)ColorConverter.ConvertFromString(expClients.BorderBrush.ToString()), Colors.Transparent, 0.3);
        }
        //-----------------------------------------------------------------------------------

        private void GridExpanderClients_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (expClients.IsExpanded == false)
            {
                expClients.IsExpanded = true;
                Animations.HeightAnimation(expClients, expClients.ActualHeight, 187, 0.3);

            }
            else
            {
                Animations.HeightAnimation(expClients, expClients.ActualHeight, 50, 0.3, expClients);
            }
        }

        private void expOrders_MouseEnter(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (expOrders, (Color)ColorConverter.ConvertFromString(expOrders.BorderBrush.ToString()), (Color)ColorConverter.ConvertFromString("#9C9FA6"), 0.3);
        }

        private void expOrders_MouseLeave(object sender, MouseEventArgs e)
        {
            Animations.AnimateBorderBrush
                (expOrders, (Color)ColorConverter.ConvertFromString(expOrders.BorderBrush.ToString()), Colors.Transparent, 0.3);
        }

        private void GridExpanderOrders_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (expOrders.IsExpanded == false)
            {
                expOrders.IsExpanded = true;
                Animations.HeightAnimation(expOrders, expOrders.ActualHeight, 187, 0.3);

            }
            else
            {
                Animations.HeightAnimation(expOrders, expOrders.ActualHeight, 50, 0.3, expOrders);
            }
        }

        private void ExpanderHeaderPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is DockPanel headerPanel && headerPanel.TemplatedParent is Expander expander)
            {
                expander.IsExpanded = !expander.IsExpanded;
            }
        }
    }
}