using Dickplom1.Class;
using Dickplom1.DataFolder;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public Users SelectedUser { get; set; } = null;
        public BitmapImage PhotoPath { get; set; } = null;

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
            EditableMode();
        }

        public void EditableMode()
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
        public void OffEditableMode()
        {
            Class.Musor.HideElement(btnSave);
            Class.Musor.ShowElement(btnEdit);

            tboxSurname.IsEnabled = false;
            tboxName.IsEnabled = false;
            tboxMiddlename.IsEnabled = false;
            tboxDateOfBirth.IsEnabled = false;
            tboxPhone.IsEnabled = false;
            tboxEmail.IsEnabled = false;
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
            EditableMode();
        }
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
            LoadDataToWin();
        }
        public void LoadDataToWin()
        {
            var mainWin = Application.Current.MainWindow as MainWindow;
            var context = DBEntities.GetContext();
            var selectedUserFromDB = context.Users.FirstOrDefault(f=>f.UserId == SelectedUser.UserId);
            //Проверки
            if (SelectedUser.UserId == mainWin.ActiveUser.UserId)
            {
                Class.Musor.ShowElement(btnEdit);
                Class.Musor.ShowElement(btnChangePassword);
                Class.Musor.ShowElement(ChangePhoto);
                imgDelete.IsHitTestVisible = true;
            }
            else
            {
                Class.Musor.HideElement(btnEdit);
                Class.Musor.HideElement(btnChangePassword);
                Class.Musor.HideElement(ChangePhoto);
                imgDelete.IsHitTestVisible = false;
            }
            //____________


            if (SelectedUser != null)
            {
                try
                {
                    if (selectedUserFromDB.UserData.UserPhoto != null)
                    {
                        PhotoPath = LoadImage(selectedUserFromDB.UserData.UserPhoto);
                    }
                    else
                    {
                        PhotoPath = null;
                    }

                    tbFullNameStaff.Text = SelectedUser?.UserData.Surname + " " + SelectedUser?.UserData.Name + " " + SelectedUser?.UserData.MiddleName;
                    tbPostStaff.Text = selectedUserFromDB?.Roles.NameRole;

                    //Тб данные пользователя
                    tboxSurname.Text = SelectedUser.UserData.Surname;
                    tboxName.Text = SelectedUser.UserData.Name;
                    tboxMiddlename.Text = SelectedUser.UserData.MiddleName;
                    tboxDateOfBirth.Text = SelectedUser.UserData.DateOfBirth?.ToString("d");
                    tboxPhone.Text = SelectedUser.UserData.PhoneNumber;
                    tboxEmail.Text = SelectedUser.UserData.Email;

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
                catch (Exception)
                {

                }
            }
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
                EditableMode();
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
            var context = DBEntities.GetContext();
            try
            {
                if (tboxSurname.tb.Text == "Фамилия"
                                
                    || tboxName.tb.Text == "Имя"
                    || tboxDateOfBirth.tb.Text == "Дата рождения"
                    || tboxPhone.tb.Text == "Номер телефона"
                    || tboxEmail.tb.Text == "Электронная почта"

                    || string.IsNullOrWhiteSpace(tboxSurname.tb.Text)
                    || string.IsNullOrWhiteSpace(tboxName.tb.Text)
                    || string.IsNullOrWhiteSpace(tboxDateOfBirth.tb.Text)
                    || string.IsNullOrWhiteSpace(tboxPhone.tb.Text)
                    || string.IsNullOrWhiteSpace(tboxEmail.tb.Text))
                {
                    MessageBox.Show("Необходимо заполнить все поля");
                    return;
                }
                //Проверки
                if (tboxPhone.tb.Text.Length < 11)
                {
                    MessageBox.Show("Номер телефона должен содержать 11 цифр");
                    return;
                }
                if (!tboxPhone.tb.Text.StartsWith("8") && !tboxPhone.tb.Text.StartsWith("7"))
                {
                    MessageBox.Show("Номер телефона должен начинаться на 7 или 8");
                    return;
                }
                if (!tboxEmail.tb.Text.Contains("@") | !tboxEmail.tb.Text.Contains("."))
                {
                    MessageBox.Show("Неправильный формат электронной почты");
                    return;
                }
                if (DateTime.TryParse(tboxDateOfBirth.tb.Text, out DateTime date))
                {
                    if (date != null)
                    {
                        if (date < DateTime.Now.AddYears(-100) || date > DateTime.Now.AddYears(-16)) // Возраст типо
                        {
                            MessageBox.Show("Некорректная дата рождения");
                            return;
                        }
                        else if (date > DateTime.Now)
                        {
                            MessageBox.Show("Некорректная дата рождения");
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Некорректная дата рождения");
                    return;
                }
                if (SelectedUser != null) // Действия и сохранение
                {
                    var selectedUserWithNewData = context.Users.FirstOrDefault(f => f.UserId == SelectedUser.UserId);
                    if (selectedUserWithNewData != null)
                    {
                        selectedUserWithNewData.UserData.Surname = tboxSurname.tb.Text;
                        selectedUserWithNewData.UserData.Name = tboxName.tb.Text;
                        selectedUserWithNewData.UserData.MiddleName = tboxMiddlename.tb.Text ?? "-";
                        selectedUserWithNewData.UserData.DateOfBirth = DateTime.Parse(tboxDateOfBirth.tb.Text);
                        selectedUserWithNewData.UserData.PhoneNumber = tboxPhone.Text;
                        selectedUserWithNewData.UserData.Email = tboxEmail.tb.Text;

                        if (ClientPhoto.Source != null)
                            selectedUserWithNewData.UserData.UserPhoto = BitmapImageToByteArray(PhotoPath);
                        else
                            selectedUserWithNewData.UserData.UserPhoto = null;

                        context.SaveChanges();
                        MessageBox.Show("Данные успешно применены");
                        OffEditableMode();
                        LoadDataToWin();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void ButtonBackgroundOff_MouseEnter(object sender, MouseEventArgs e)
        {
            Class.Animations.OpacityAnimation(btnChangePassword, btnChangePassword.Opacity, 0.8, 0.2);
        }

        private void ButtonBackgroundOff_MouseLeave(object sender, MouseEventArgs e)
        {
            Class.Animations.OpacityAnimation(btnChangePassword, btnChangePassword.Opacity, 1, 0.2);
        }

        private void ButtonBackgroundOff_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            var mainWin = Application.Current.MainWindow as MainWindow;
            ChangePasswordWin win = new ChangePasswordWin();
            if (mainWin.ActiveUser != null)
                win.ActiveUser = mainWin.ActiveUser;
            win.ShowDialog();
        }

        private void tboxSurname_Loaded(object sender, RoutedEventArgs e)
        {
            tboxSurname.tb.MaxLength = 30;
        }

        private void tboxSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Zа-яА-ЯёЁ]+$");
        }

        private void tboxName_Loaded(object sender, RoutedEventArgs e)
        {
            tboxName.tb.MaxLength = 30;

        }

        private void tboxName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Zа-яА-ЯёЁ]+$");
        }

        private void tboxMiddlename_Loaded(object sender, RoutedEventArgs e)
        {
            tboxMiddlename.tb.MaxLength = 30;
        }

        private void tboxMiddlename_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Zа-яА-ЯёЁ]+$");
        }

        private void tboxDateOfBirth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9.]$");
        }

        private void tboxDateOfBirth_Loaded(object sender, RoutedEventArgs e)
        {
            tboxDateOfBirth.tb.MaxLength = 10;
        }

        private void tboxPhone_Loaded(object sender, RoutedEventArgs e)
        {
            tboxPhone.tb.MaxLength = 11;
        }

        private void tboxPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");

            if (e.Text != "8" & e.Text != "7" && tboxPhone.tb.SelectionStart == 0)
                e.Handled = true;
        }

        private void tboxEmail_Loaded(object sender, RoutedEventArgs e)
        {
            tboxEmail.tb.MaxLength = 70;
        }

        private void tboxEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Z@.]+$");
        }
    }
}
