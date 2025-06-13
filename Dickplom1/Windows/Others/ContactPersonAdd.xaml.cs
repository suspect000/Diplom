using Dickplom1.DataFolder;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
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
    /// Логика взаимодействия для ContactPersonAdd.xaml
    /// </summary>
    public partial class ContactPersonAdd : Window
    {
        public ContactPersonAdd()
        {
            InitializeComponent();
        }
        public int CompanyId { get; set; } = 0;
        public BitmapImage PhotoPath { get; set; } = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CompanyId != 0)
            {
                labCompanyName.Content = DBEntities.GetContext().ClientsLegalEntitiesCompanyData
                    .FirstOrDefault(f=>f.CompanyId == CompanyId).CompanyName;
            }
            //Изображение
            if (PhotoPath != null)
            {
                try
                {
                    ClientPhoto.Source = PhotoPath;
                }
                catch (Exception)
                {
                }
            }
            if (ClientPhoto.Source != null)
            {
                Dickplom1.Class.Musor.HideElement(ClientPhotoFI);
                Dickplom1.Class.Musor.ShowElement(imgDelete); // Включаем кнопку отмены выбора фотографии пользователя
            }
            else
            {
                Dickplom1.Class.Musor.ShowElement(ClientPhotoFI);
                Dickplom1.Class.Musor.HideElement(imgDelete);
            }
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
            MessageBoxButton btns = MessageBoxButton.YesNo;
            MessageBoxResult mb = MessageBox.Show("Вы уверенны?", "Внимание", btns);
            if (mb == MessageBoxResult.Yes)
            {
                ClientPhoto.Source = null;
                Dickplom1.Class.Musor.HideElement(imgDelete);
                Dickplom1.Class.Musor.ShowElement(ClientPhotoFI);
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
            {
                ClientPhoto.Source = new BitmapImage(new Uri(op.FileName));
                PhotoPath = new BitmapImage(new Uri(op.FileName));
            }

            imgDelete.Visibility = Visibility.Visible; // Включаем кнопку отмены выбора фотографии пользователя
            ClientPhotoFI.Visibility = Visibility.Collapsed;
        }

        private void tboxSurname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tboxName.tb.Text == "Имя" && tboxSurname.tb.Text == "Фамилия")
            {
                ClientPhotoFI.Text = "НН";
            }
        }

        private void tboxName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tboxName.tb.Text == "Имя" && tboxSurname.tb.Text == "Фамилия")
            {
                ClientPhotoFI.Text = "НН";
            }
        }

        private void btnSave_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.btnWithBorder.Click += BtnWithBorder_Click; ;
        }

        private void BtnWithBorder_Click(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();


            if (tboxSurname.tb.Text == "Фамилия представителя"
                || tboxName.tb.Text == "Имя представителя"
                || tboxPhoneNumber.tb.Text == "Номер телефона представителя"
                || tboxEmail.tb.Text == "Электронная почта представителя"

                || string.IsNullOrWhiteSpace(tboxSurname.tb.Text)
                || string.IsNullOrWhiteSpace(tboxName.tb.Text)
                || string.IsNullOrWhiteSpace(tboxPhoneNumber.tb.Text)
                || string.IsNullOrWhiteSpace(tboxEmail.tb.Text))
            {
                MessageBox.Show("Необходимо заполнить все поля");
                return;
            }
            else if (CompanyId != 0)
            {
                ClientsLegalEntitiesContactPerson newContactPerson = new ClientsLegalEntitiesContactPerson()
                    {
                        Name = tboxName.Text,
                        Surname = tboxSurname.Text,
                        Middlename = tboxMiddlename.Text,
                        Phone = tboxPhoneNumber.Text,
                        Email = tboxEmail.Text,
                        IsActive = false,
                        CompanyId = CompanyId
                    };

                if (ClientPhoto.Source != null)
                    newContactPerson.Photo = BitmapImageToByteArray(PhotoPath);
                else
                    newContactPerson.Photo = null;

                context.ClientsLegalEntitiesContactPerson.Add(newContactPerson);
                context.SaveChanges();
                MessageBox.Show("Запись успешно добавлена");
                this.Close();
            }
        }
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
    }
}
