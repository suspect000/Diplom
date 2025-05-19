using Dickplom1.Class;
using Microsoft.Win32;
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
using System.Xml.Linq;


namespace Dickplom1.Windows.Others
{
    /// <summary>
    /// Логика взаимодействия для ClientsNaturalPersonAddWin.xaml
    /// </summary>
    public partial class ClientsNaturalPersonAddWin : Window
    {
        public ClientsNaturalPersonAddWin()
        {
            InitializeComponent();
            tboxSurname.tb.TextChanged += tboxSurname_TextChanged;
            tboxName.tb.TextChanged += tboxName_TextChanged;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ClientPhoto.Source != null)
            {
                ClientPhotoFI.Visibility = Visibility.Collapsed;
                imgDelete.Visibility = Visibility.Visible; // Включаем кнопку отмены выбора фотографии пользователя
            }
            else
            {
                ClientPhotoFI.Visibility = Visibility.Visible;
                imgDelete.Visibility = Visibility.Collapsed;
            }
                
        }

        private void ButtonBackgroundOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Выберите новую фотографию";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
                ClientPhoto.Source = new BitmapImage(new Uri(op.FileName));

            imgDelete.Visibility = Visibility.Visible; // Включаем кнопку отмены выбора фотографии пользователя
            ClientPhotoFI.Visibility = Visibility.Collapsed;
        }
        private void ClientPhoto_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            if (ClientPhoto.Source != null)
                ClientPhotoFI.Visibility = Visibility.Collapsed;
        }
        private void tboxSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!tboxSurname.tb.Text.Contains("Фамилия") && !string.IsNullOrWhiteSpace(tboxSurname.tb.Text))
            {
                string surname = tboxSurname.tb.Text;

                if (!tboxName.tb.Text.Contains("Имя") && !string.IsNullOrWhiteSpace(tboxName.tb.Text))
                {
                    string name = tboxName.tb.Text;
                    ClientPhotoFI.Text = "";
                    ClientPhotoFI.Text = $"{surname.Remove(1, surname.Length - 1)}{name.Remove(1, name.Length - 1)}";
                }
                else
                {
                    try
                    {
                        ClientPhotoFI.Text = "";
                        ClientPhotoFI.Text = $"{surname.Remove(1, surname.Length - 1)}{ClientPhotoFI.Text}";
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        private void tboxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!tboxName.tb.Text.Contains("Имя") && !string.IsNullOrWhiteSpace(tboxName.tb.Text))
            {
                string name = tboxName.tb.Text;

                if (!tboxSurname.tb.Text.Contains("Фамилия") && !string.IsNullOrWhiteSpace(tboxSurname.tb.Text))
                {
                    string surname = tboxSurname.tb.Text;
                    ClientPhotoFI.Text = "";
                    ClientPhotoFI.Text = $"{surname.Remove(1, surname.Length - 1)}{name.Remove(1, name.Length - 1)}";
                }
                else
                {
                    try
                    {
                        ClientPhotoFI.Text = "";
                        ClientPhotoFI.Text = $"{ClientPhotoFI.Text}{name.Remove(1, name.Length - 1)}";
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (gridMovingWin.IsMouseOver)
            {
                this.DragMove();
            } 

            
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

        private void imgDelete_MouseEnter(object sender, MouseEventArgs e)
        {
            Class.Animations.OpacityAnimation(imgDelete, imgDelete.Opacity, 0.7, 0.2);
        }

        private void imgDelete_MouseLeave(object sender, MouseEventArgs e)
        {
            Class.Animations.OpacityAnimation(imgDelete, imgDelete.Opacity, 1, 0.2);
        }

        private void imgDelete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClientPhoto.Source = null;
            imgDelete.Visibility = Visibility.Collapsed;
            ClientPhotoFI.Visibility = Visibility.Visible;
        }

        private void tboxName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tboxName.tb.Text == "Имя" && tboxSurname.tb.Text == "Фамилия")
            {
                ClientPhotoFI.Text = "НН";
            }

        }

        private void tboxSurname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tboxName.tb.Text == "Имя" && tboxSurname.tb.Text == "Фамилия")
            {
                ClientPhotoFI.Text = "НН";
            }
        }
    }
}
