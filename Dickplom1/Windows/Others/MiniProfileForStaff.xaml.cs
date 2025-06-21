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

namespace Dickplom1.Windows.Others
{
    /// <summary>
    /// Логика взаимодействия для MiniProfileForStaff.xaml
    /// </summary>
    public partial class MiniProfileForStaff : Window
    {
        public MiniProfileForStaff()
        {
            InitializeComponent();
        }
        public BitmapImage PhotoPath { get; set; } = null;

        private void tboxSurname_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void tboxName_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_MouseEnter(object sender, MouseEventArgs e)
        {
            Class.Animations.OpacityAnimation(btnEdit, btnEdit.Opacity, 0.8, 0.2);
        }

        private void btnEdit_MouseLeave(object sender, MouseEventArgs e)
        {
            Class.Animations.OpacityAnimation(btnEdit, btnEdit.Opacity, 1, 0.2);
        }

        private void btnEdit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Class.Musor.HideElement(btnEdit);
            Class.Musor.ShowElement(btnSave);
            
            tboxSurname.IsEnabled = true;
            tboxName.IsEnabled = true;
            tboxMiddlename.IsEnabled = true;
            tboxDateOfBirth.IsEnabled = true;
            tboxPhone.IsEnabled = true;
            tboxEmail.IsEnabled = true;
        }

        private void ButtonBackgroundOff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Выберите новую фотографию";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                ClientPhoto.Source = new BitmapImage(new Uri(op.FileName));
                PhotoPath = new BitmapImage(new Uri(op.FileName));
            }

            imgDelete.Visibility = Visibility.Visible; // Включаем кнопку отмены выбора фотографии пользователя
            ClientPhotoFI.Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void imgDelete_MouseEnter(object sender, MouseEventArgs e)
        {
            Class.Animations.OpacityAnimation(imgDelete, imgDelete.Opacity, 0.7, 0.2);
        }

        private void imgDelete_MouseLeave(object sender, MouseEventArgs e)
        {
            Class.Animations.OpacityAnimation(imgDelete, imgDelete.Opacity, 0.7, 0.2);
        }

        private void imgDelete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxButton btns = MessageBoxButton.YesNo;
            MessageBoxResult mb = MessageBox.Show("Вы уверенны?", "Внимание", btns);
            if (mb == MessageBoxResult.Yes)
            {
                ClientPhoto.Source = null;
                Dickplom1.Class.Musor.HideElement(imgDelete);
                Dickplom1.Class.Musor.ShowElement(ClientPhotoFI);
            }
        }

        private void ButtonClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.btnWithBorder.Click += BtnWithBorder_Click;
        }

        private void BtnWithBorder_Click(object sender, RoutedEventArgs e)
        {
            Class.Musor.HideElement(btnSave);
            Class.Musor.ShowElement(btnEdit);
        }
    }
}
