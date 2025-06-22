using Dickplom1.Class;
using Dickplom1.DataFolder;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Xml.Linq;


namespace Dickplom1.Windows.Others
{
    /// <summary>
    /// Логика взаимодействия для StaffManagerMiniProfile.xaml
    /// </summary>
    public partial class StaffManagerMiniProfile : Window
    {
        public StaffManagerMiniProfile()
        {
            InitializeComponent();
            tboxSurname.tb.TextChanged += tboxSurname_TextChanged;
            tboxName.tb.TextChanged += tboxName_TextChanged;
        }
        public int StaffId { get; set; } = 0;
        public BitmapImage PhotoPath { get; set; } = null;


        // Преобразование фотографии в ImageSource
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var mainWin = Application.Current.MainWindow as MainWindow;
            if (mainWin != null)
            {
                if (mainWin.ActiveUser != null)
                {
                    if (mainWin.ActiveUser.RoleId == 1)
                    {
                        gridStaffStatistic.Visibility = Visibility.Collapsed;
                        mainBorder.Height = 600;
                        this.Height = 700;

                    }
                    else if (mainWin.ActiveUser.RoleId == 2)
                    {
                        gridStaffStatistic.Visibility = Visibility.Visible;
                        mainBorder.Height = 734;
                        this.Height = 834;
                    }
                }
            }
            var context = DBEntities.GetContext().UserData
                .Where(c=>c.UserDataId == StaffId)
                .FirstOrDefault();
            var selectedUser = DBEntities.GetContext().Users
                .FirstOrDefault(f=>f.UserDataId == StaffId);

            if (context == null)
            return;

            if (context.UserPhoto != null)
            {
                PhotoPath = LoadImage(context.UserPhoto);
            }

            //Загрузка данных сотрудника в текстовые поля и изображение
            if (StaffId != 0)
            {
                tboxSurname.tb.Text = context.Surname;
                tboxName.tb.Text = context.Name;
                tboxMiddleName.tb.Text = context.MiddleName;
                tboxPhoneNumber.tb.Text = context.PhoneNumber;
                tboxEmail.tb.Text = context.Email;
                tboxRole.tb.Text = selectedUser.Roles.NameRole;
            }
            //Изображение
            if (PhotoPath != null)
            {
                try
                {
                    ProfilePhoto.Source = PhotoPath;
                }
                catch (Exception)
                {
                }
            }
            if (ProfilePhoto.Source != null)
            {
                ClientPhotoFI.Visibility = Visibility.Collapsed;
            }
            else
            {
                ClientPhotoFI.Visibility = Visibility.Visible;
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
                ProfilePhoto.Source = new BitmapImage(new Uri(op.FileName));
                PhotoPath = new BitmapImage(new Uri(op.FileName));
            }
                

            ClientPhotoFI.Visibility = Visibility.Collapsed;
        }
        private void ClientPhoto_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            if (ProfilePhoto.Source != null)
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











        //Пригодится для окна стафа от админской роли!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        /*private void ButtomWithBorder_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.btnWithBorder.Click += BtnWithBorder_Click;
        }

        private void BtnWithBorder_Click(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            if (StaffId != 0) // При редактировании клиента
            {
                UserData selectedPerson = context.UserData
                    .Where(c => c.UserDataId == StaffId)
                    .FirstOrDefault();

                selectedPerson.UserDataId = StaffId;
                selectedPerson.Surname = tboxSurname.tb.Text;
                selectedPerson.Name = tboxName.tb.Text;
                selectedPerson.MiddleName = tboxMiddleName.tb.Text;
                selectedPerson.PhoneNumber = tboxPhoneNumber.tb.Text;
                selectedPerson.Email = tboxEmail.tb.Text;

                if (ProfilePhoto.Source != null)
                    selectedPerson.UserPhoto = BitmapImageToByteArray(PhotoPath);
                else
                    selectedPerson.UserPhoto = null;

                context.SaveChanges();
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
        }*/
    }
}
