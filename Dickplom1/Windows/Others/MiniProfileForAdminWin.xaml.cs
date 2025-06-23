using CustomControlsForDiplomFramework;
using Dickplom1.Class;
using Dickplom1.DataFolder;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для MiniProfileForAdminWin.xaml
    /// </summary>
    public partial class MiniProfileForAdminWin : Window
    {
        public MiniProfileForAdminWin()
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

            tbLogin.IsEnabled = true;
            cbPost.IsEnabled = true;

            tboxSurname.IsEnabled = true;
            tboxName.IsEnabled = true;
            tboxMiddlename.IsEnabled = true;
            tboxDateOfBirth.IsEnabled = true;
            tboxPhone.IsEnabled = true;
            tboxEmail.IsEnabled = true;

            tbPasCountry.IsEnabled = true;
            tbDepartmentCode.IsEnabled = true;
            tbPlaceOfBirth.IsEnabled = true;
            cbSex.IsEnabled = true;
            tbPasNumber.IsEnabled = true;
            tbPasSeries.IsEnabled = true;
            tbDateOfIssued.IsEnabled = true;
            tbAddresCountry.IsEnabled = true;
            tbAddresCity.IsEnabled = true;
            tbAddresStreet.IsEnabled = true;
            tbAddresHouseNumber.IsEnabled = true;
            tbAddresAppartmentNumber.IsEnabled = true;
        }
        public void OffEditableMode()
        {
            Class.Musor.HideElement(btnSave);
            Class.Musor.ShowElement(btnEdit);

            tbLogin.IsEnabled = false;
            cbPost.IsEnabled = true;

            tboxSurname.IsEnabled = false;
            tboxName.IsEnabled = false;
            tboxMiddlename.IsEnabled = false;
            tboxDateOfBirth.IsEnabled = false;
            tboxPhone.IsEnabled = false;
            tboxEmail.IsEnabled = false;

            tbPasCountry.IsEnabled = false;
            tbDepartmentCode.IsEnabled = false;
            tbPlaceOfBirth.IsEnabled = false;
            cbSex.IsEnabled = false;
            tbPasNumber.IsEnabled = false;
            tbPasSeries.IsEnabled = false;
            tbDateOfIssued.IsEnabled = false;
            tbAddresCountry.IsEnabled = false;
            tbAddresCity.IsEnabled = false;
            tbAddresStreet.IsEnabled = false;
            tbAddresHouseNumber.IsEnabled = false;
            tbAddresAppartmentNumber.IsEnabled = false;
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
            //LoadDataToWin();
        }
        public void LoadDataToWin()
        {
            var mainWin = Application.Current.MainWindow as MainWindow;
            var context = DBEntities.GetContext();

            if (SelectedUser == null)
                return;

            var selectedUserFromDB = context.Users.FirstOrDefault(f=>f.UserId == SelectedUser.UserId);


            if (SelectedUser != null)
            {
                try
                {
                    if (selectedUserFromDB.UserData.UserPhoto != null)
                    {
                        PhotoPath = LoadImage(selectedUserFromDB.UserData.UserPhoto);
                    }
                    tbLogin.tb.Text = selectedUserFromDB.Login ?? "";

                    tbFullNameStaff.Text = SelectedUser?.UserData.Surname + " " + SelectedUser?.UserData.Name + " " + SelectedUser?.UserData.MiddleName;
                    cbPost.cbox.SelectedValue = selectedUserFromDB?.RoleId;

                    //Тб данные пользователя
                    tboxSurname.Text = SelectedUser.UserData.Surname;
                    tboxName.Text = SelectedUser.UserData.Name;
                    tboxMiddlename.Text = SelectedUser.UserData.MiddleName;
                    tboxDateOfBirth.Text = SelectedUser.UserData.DateOfBirth?.ToString("d");
                    tboxPhone.Text = SelectedUser.UserData.PhoneNumber;
                    tboxEmail.Text = SelectedUser.UserData.Email;

                    tbPasCountry.Text = SelectedUser.UserPassportData.PassportCountry;
                    tbDepartmentCode.Text = SelectedUser.UserPassportData.PassportDepartmentСode;
                    tbPlaceOfBirth.Text = SelectedUser.UserPassportData.PassportPlaceOfBirth;
                    cbSex.cbox.SelectedValue = SelectedUser.UserPassportData.SexId;
                    tbPasNumber.Text = SelectedUser.UserPassportData.PassportNumber;
                    tbPasSeries.Text = SelectedUser.UserPassportData.PassportSeries;
                    tbDateOfIssued.Text = SelectedUser.UserPassportData.PassportDateOfIssue.Value.ToString("d") ?? "";

                    tbAddresCountry.Text = SelectedUser.UserPassportData.Address.Street.City.Country.CountryName;
                    tbAddresCity.Text = SelectedUser.UserPassportData.Address.Street.City.CityName;
                    tbAddresStreet.Text = SelectedUser.UserPassportData.Address.Street.StreetName;
                    tbAddresHouseNumber.Text = SelectedUser.UserPassportData.Address.HouseNumber;
                    tbAddresAppartmentNumber.Text = SelectedUser.UserPassportData.Address.Apartment;

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
            var mainWin = Application.Current.MainWindow as MainWindow;
            var context = DBEntities.GetContext();
            try
            {
                if (tboxSurname.tb.Text == "Фамилия"
                                
                    || tboxName.tb.Text == "Имя"
                    || tboxDateOfBirth.tb.Text == "Дата рождения"
                    || tboxPhone.tb.Text == "Номер телефона"
                    || tboxEmail.tb.Text == "Электронная почта"

                    || tbPasCountry.tb.Text == "Паспорт страны"
                    || tbDepartmentCode.tb.Text == "Код департамента"
                    || tbPlaceOfBirth.tb.Text == "Место рождения"
                    || cbSex.cbox.SelectedIndex == 0
                    || tbPasNumber.tb.Text == "Номер паспорта"
                    || tbPasSeries.tb.Text == "Серия паспорта"
                    || tbDateOfIssued.tb.Text == "Дата выдачи"
                    || tbAddresCountry.tb.Text == "Страна регистрации"
                    || tbAddresCity.tb.Text == "Город регистрации"
                    || tbAddresStreet.tb.Text == "Улица регистрации"
                    || tbAddresHouseNumber.tb.Text == "Номер дома регистрации"
                    || tbAddresAppartmentNumber.tb.Text == "Номер квартиры регистрации"
                    
                    || tbLogin.tb.Text == "Логин"
                    || cbPost.cbox.SelectedIndex == 0

                    || string.IsNullOrWhiteSpace(tboxSurname.tb.Text)
                    || string.IsNullOrWhiteSpace(tboxName.tb.Text)
                    || string.IsNullOrWhiteSpace(tboxDateOfBirth.tb.Text)
                    || string.IsNullOrWhiteSpace(tboxPhone.tb.Text)
                    || string.IsNullOrWhiteSpace(tboxEmail.tb.Text)   
                    || string.IsNullOrWhiteSpace(tbPasCountry.tb.Text)
                    || string.IsNullOrWhiteSpace(tbDepartmentCode.tb.Text)
                    || string.IsNullOrWhiteSpace(tbPlaceOfBirth.tb.Text)
                    || string.IsNullOrWhiteSpace(tbPasNumber.tb.Text)
                    || string.IsNullOrWhiteSpace(tbPasSeries.tb.Text)
                    || string.IsNullOrWhiteSpace(tbDateOfIssued.tb.Text)
                    || string.IsNullOrWhiteSpace(tbAddresCountry.tb.Text)
                    || string.IsNullOrWhiteSpace(tbAddresCity.tb.Text)
                    || string.IsNullOrWhiteSpace(tbAddresStreet.tb.Text)
                    || string.IsNullOrWhiteSpace(tbAddresHouseNumber.tb.Text)
                    || string.IsNullOrWhiteSpace(tbAddresAppartmentNumber.tb.Text)
                    || string.IsNullOrWhiteSpace(tbLogin.tb.Text))
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

                        selectedUserWithNewData.Login = tbLogin.tb.Text;

                        selectedUserWithNewData.UserPassportData.PassportCountry = tbPasCountry.tb.Text;
                        selectedUserWithNewData.UserPassportData.PassportDepartmentСode = tbDepartmentCode.tb.Text;
                        selectedUserWithNewData.UserPassportData.PassportPlaceOfBirth = tbPlaceOfBirth.tb.Text;
                        selectedUserWithNewData.UserPassportData.SexId = (int)cbSex.cbox.SelectedValue;
                        selectedUserWithNewData.UserPassportData.PassportNumber = tbPasNumber.Text;
                        selectedUserWithNewData.UserPassportData.PassportSeries = tbPasSeries.Text;
                        selectedUserWithNewData.UserPassportData.PassportDateOfIssue = DateTime.Parse(tbDateOfIssued.Text);
                        selectedUserWithNewData.UserPassportData.Address.Street.City.Country.CountryName = tbAddresCountry.tb.Text;
                        selectedUserWithNewData.UserPassportData.Address.Street.City.CityName = tbAddresCity.tb.Text;
                        selectedUserWithNewData.UserPassportData.Address.Street.StreetName = tbAddresStreet.tb.Text;
                        selectedUserWithNewData.UserPassportData.Address.HouseNumber = tbAddresHouseNumber.tb.Text;
                        selectedUserWithNewData.UserPassportData.Address.Apartment = tbAddresAppartmentNumber.tb.Text;

                        if (ClientPhoto.Source != null)
                            selectedUserWithNewData.UserData.UserPhoto = BitmapImageToByteArray(PhotoPath);
                        else
                            selectedUserWithNewData.UserData.UserPhoto = null;
                    }
                }
                else
                {

                    var newUserData = new UserData
                    {
                        Surname = tboxSurname.tb.Text,
                        Name = tboxName.tb.Text,
                        MiddleName = tboxMiddlename.tb.Text ?? "-",
                        DateOfBirth = DateTime.Parse(tboxDateOfBirth.tb.Text),
                        PhoneNumber = tboxPhone.Text,
                        Email = tboxEmail.tb.Text,
                        UserPhoto = ClientPhoto.Source != null ? BitmapImageToByteArray(PhotoPath) : null
                    };
                    var newAddress = new Address
                    {
                        Street = new Street
                        {
                            StreetName = tbAddresStreet.tb.Text,
                            City = new City
                            {
                                CityName = tbAddresCity.tb.Text,
                                Country = new Country
                                {
                                    CountryName = tbAddresCountry.tb.Text
                                }
                            }
                        },
                        HouseNumber = tbAddresHouseNumber.tb.Text,
                        Apartment = tbAddresAppartmentNumber.tb.Text
                    };

                    var newPassportData = new UserPassportData
                    {
                        PassportCountry = tbPasCountry.tb.Text,
                        PassportDepartmentСode = tbDepartmentCode.tb.Text,
                        PassportPlaceOfBirth = tbPlaceOfBirth.tb.Text,
                        SexId = (int)cbSex.cbox.SelectedValue,
                        PassportNumber = tbPasNumber.Text,
                        PassportSeries = tbPasSeries.Text,
                        PassportDateOfIssue = DateTime.Parse(tbDateOfIssued.Text),
                        Address = newAddress
                    };

                    // Создание самого пользователя
                    var newUser = new Users
                    {
                        AccountStatusId = 2, 
                        UserData = newUserData,
                        UserPassportData = newPassportData,
                        RoleId = (int)cbPost.cbox.SelectedValue, 
                        Login = tbLogin.tb.Text, 
                        CreatorId = mainWin.ActiveUser?.UserId, 
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    context.Users.Add(newUser);
                }
                context.SaveChanges();
                MessageBox.Show("Данные успешно сохранены");
                OffEditableMode();
                LoadDataToWin();
            }
            catch (Exception)
            {
            }
        }

        private void ButtonBackgroundOff_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            var mainWin = Application.Current.MainWindow as MainWindow;
            ChangePasswordWin win = new ChangePasswordWin();
            if (mainWin?.ActiveUser != null)
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

        private void tbPasCountry_Loaded(object sender, RoutedEventArgs e)
        {
            tbPasCountry.tb.MaxLength = 40;
        }

        private void tbPasCountry_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Zа-яА-ЯёЁ]+$");
        }

        private void tbDepartmentCode_Loaded(object sender, RoutedEventArgs e)
        {
            tbDepartmentCode.tb.MaxLength = 10;
        }

        private void tbDepartmentCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9-]$");
        }

        private void tbPlaceOfBirth_Loaded(object sender, RoutedEventArgs e)
        {
            tbPlaceOfBirth.tb.MaxLength = 50;
        }

        private void tbPlaceOfBirth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Zа-яА-ЯёЁ.]+$");
        }

        private void cbSex_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();
            ComboboxMaterialDesignWithBorder cbox = new ComboboxMaterialDesignWithBorder();

            var items = new List<object>();
            items.Add(new { SexId = 0, SexName = "Пол сотрудника" });

            items.AddRange(context.Sexes
                .Select(u => new
                {
                    u.SexId,
                    u.SexName,
                }));

            cbSex.Name = "cbox";
            cbSex.cbox.ItemsSource = items;
            cbSex.cbox.DisplayMemberPath = "SexName";
            cbSex.cbox.SelectedValuePath = "SexId";
            cbSex.cbox.SelectedIndex = 0;

            LoadDataToWin();
        }

        private void tbPasNumber_Loaded(object sender, RoutedEventArgs e)
        {
            tbPasNumber.tb.MaxLength = 4;

        }

        private void tbPasNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void tbPasSeries_Loaded(object sender, RoutedEventArgs e)
        {
            tbPasSeries.tb.MaxLength = 6;

        }

        private void tbPasSeries_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void tbDateOfIssued_Loaded(object sender, RoutedEventArgs e)
        {
            tbDateOfIssued.tb.MaxLength = 10;

        }

        private void tbDateOfIssued_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9.]$");
        }

        private void tbAddresCountry_Loaded(object sender, RoutedEventArgs e)
        {
            tbAddresCountry.tb.MaxLength = 40;

        }

        private void tbAddresCountry_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Zа-яА-ЯёЁ]+$");
        }

        private void tbAddresCity_Loaded(object sender, RoutedEventArgs e)
        {
            tbAddresCity.tb.MaxLength = 40;

        }

        private void tbAddresCity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Zа-яА-ЯёЁ]+$");
        }

        private void tbAddresStreet_Loaded(object sender, RoutedEventArgs e)
        {
            tbAddresStreet.tb.MaxLength = 50;

        }

        private void tbAddresStreet_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Zа-яА-ЯёЁ]+$");
        }

        private void tbAddresHouseNumber_Loaded(object sender, RoutedEventArgs e)
        {
            tbAddresHouseNumber.tb.MaxLength = 5;

        }

        private void tbAddresHouseNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[а-яА-ЯёЁ0-9/]+$");
        }

        private void tbAddresAppartmentNumber_Loaded(object sender, RoutedEventArgs e)
        {
            tbAddresAppartmentNumber.tb.MaxLength = 5;

        }

        private void tbAddresAppartmentNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]+$");
        }

        private void tboxLogin_Loaded(object sender, RoutedEventArgs e)
        {
            tbLogin.tb.MaxLength = 70;
        }

        private void tboxLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Z@./!#]+$");
        }

        private void cbPost_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();
            ComboboxMaterialDesignWithBorder cbox = new ComboboxMaterialDesignWithBorder();

            var items = new List<object>();
            items.Add(new { RoleId = 0, NameRole = "Должность" });

            items.AddRange(context.Roles
                .Select(u => new
                {
                    u.RoleId,
                    u.NameRole,
                }));

            cbPost.Name = "cboxPost";
            cbPost.cbox.ItemsSource = items;
            cbPost.cbox.DisplayMemberPath = "NameRole";
            cbPost.cbox.SelectedValuePath = "RoleId";
            cbPost.cbox.SelectedIndex = 0;
        }
    }
}
