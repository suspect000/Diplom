using Dickplom1.Class;
using Dickplom1.DataFolder;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для ClientsLegalEntitiesAddWin.xaml
    /// </summary>
    public partial class ClientsLegalEntitiesAddWin : Window
    {
        public ClientsLegalEntitiesAddWin()
        {
            InitializeComponent();
            tboxSurname.tb.TextChanged += tboxSurname_TextChanged;
            tboxName.tb.TextChanged += tboxName_TextChanged;
        }
        public int ClientId { get; set; } = 0;
        public BitmapImage PhotoPath { get; set; } = null;

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

        public void ForEditWin()
        {

            Dickplom1.Class.Musor.HideElement(btnSave.btnWithBorder);
            Dickplom1.Class.Musor.ShowElement(btnEdit.btnWithBorder);

            tboxSurname.IsEnabled = false;
            tboxName.IsEnabled = false;
            tboxMiddlename.IsEnabled = false;
            tboxPhoneNumber.IsEnabled = false;
            tboxEmail.IsEnabled = false;
            tboxCompanyName.IsEnabled = false;
            tboxINN.IsEnabled = false;
            tboxKPP.IsEnabled = false;
            tboxOGRN.IsEnabled = false;
            tboxAddressCountry.IsEnabled = false;
            tboxAddressCity.IsEnabled = false;
            tboxAddressStreet.IsEnabled = false;
            tboxAddressHouse.IsEnabled = false;
            tboxBankName.IsEnabled = false;
            tboxBankBIK.IsEnabled = false;
            tboxBankAccount.IsEnabled = false;
            tboxBankCorrAccount.IsEnabled = false;
            tboxEmployeeCount.IsEnabled = false;
            tboxRegistrationDate.IsEnabled = false;
        }

        public void ForCreateWin()
        {
            Dickplom1.Class.Musor.ShowElement(btnSave.btnWithBorder);
            Dickplom1.Class.Musor.HideElement(btnEdit.btnWithBorder);

            tboxSurname.IsEnabled = true;
            tboxName.IsEnabled = true;
            tboxMiddlename.IsEnabled = true;
            tboxPhoneNumber.IsEnabled = true;
            tboxEmail.IsEnabled = true;
            tboxCompanyName.IsEnabled = true;
            tboxINN.IsEnabled = true;
            tboxKPP.IsEnabled = true;
            tboxOGRN.IsEnabled = true;
            tboxAddressCountry.IsEnabled = true;
            tboxAddressCity.IsEnabled = true;
            tboxAddressStreet.IsEnabled = true;
            tboxAddressHouse.IsEnabled = true;
            tboxBankName.IsEnabled = true;
            tboxBankBIK.IsEnabled = true;
            tboxBankAccount.IsEnabled = true;
            tboxBankCorrAccount.IsEnabled = true;
            tboxEmployeeCount.IsEnabled = true;
            tboxRegistrationDate.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext().ClientsLegalEntities
                .Where(c => c.ClientsLegalEntitiesId == ClientId)
                .FirstOrDefault();

            if (context != null)
            {
                if (context.ClientsLegalEntitiesContactPerson.Photo != null)
                {
                    PhotoPath = LoadImage(context.ClientsLegalEntitiesContactPerson.Photo);
                }

                //Загрузка данных клиента в текстовые поля и изображение
                if (ClientId != 0)
                {
                    tboxSurname.tb.Text = context.ClientsLegalEntitiesContactPerson.Surname;
                    tboxName.tb.Text = context.ClientsLegalEntitiesContactPerson.Name;
                    tboxMiddlename.tb.Text = context.ClientsLegalEntitiesContactPerson.Middlename;
                    tboxPhoneNumber.tb.Text = context.ClientsLegalEntitiesContactPerson.Phone;
                    tboxEmail.tb.Text = context.ClientsLegalEntitiesContactPerson.Email;

                    tboxCompanyName.tb.Text = context.ClientsLegalEntitiesCompanyData.CompanyName;
                    tboxINN.tb.Text = context.ClientsLegalEntitiesCompanyData.INN;
                    tboxKPP.tb.Text = context.ClientsLegalEntitiesCompanyData.KPP;
                    tboxOGRN.tb.Text = context.ClientsLegalEntitiesCompanyData.OGRN;
                    tboxAddressCountry.tb.Text = context.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Street.City.Country.CountryName;
                    tboxAddressCity.tb.Text = context.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Street.City.CityName;
                    tboxAddressStreet.tb.Text = context.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Street.StreetName;
                    tboxAddressHouse.tb.Text = context.ClientsLegalEntitiesCompanyData.AddressLegalEntities.HouseNumber;
                    tboxBankName.tb.Text = context.ClientsLegalEntitiesBankData.BankName;
                    tboxBankBIK.tb.Text = context.ClientsLegalEntitiesBankData.BankBik;
                    tboxBankAccount.tb.Text = context.ClientsLegalEntitiesBankData.BankAccount;
                    tboxBankCorrAccount.tb.Text = context.ClientsLegalEntitiesBankData.BankCorrAccount;
                    tboxEmployeeCount.tb.Text = context.ClientsLegalEntitiesCompanyData.EmployeeCount.ToString();
                    tboxRegistrationDate.tb.Text = context.ClientsLegalEntitiesCompanyData.RegistrationDate?.ToString("d");


                    ForEditWin();
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
            else
            {
                ForCreateWin();

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

            EditableSettingOn();
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
            MessageBoxButton btns = MessageBoxButton.YesNo;
            MessageBoxResult mb = MessageBox.Show("Вы уверенны?", "Внимание", btns);
            if (mb == MessageBoxResult.Yes)
            {
                ClientPhoto.Source = null;
                Dickplom1.Class.Musor.HideElement(imgDelete);
                Dickplom1.Class.Musor.ShowElement(ClientPhotoFI);
                EditableSettingOn();
            }
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

        private void tboxSurname_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ButtomWithBorder_Loaded(object sender, RoutedEventArgs e)
        {
            btnEdit.btnWithBorder.Height = 30;
            btnEdit.btnWithBorder.Width = 120;
            btnEdit.btnWithBorder.Click += BtnWithBorder_Click;
        }


        private void EditableSettingOn()
        {
            Dickplom1.Class.Musor.HideElement(btnEdit.btnWithBorder);
            Dickplom1.Class.Musor.ShowElement(btnSave.btnWithBorder);

            tboxSurname.IsEnabled = true;
            tboxName.IsEnabled = true;
            tboxMiddlename.IsEnabled = true;
            tboxPhoneNumber.IsEnabled = true;
            tboxEmail.IsEnabled = true;
            tboxCompanyName.IsEnabled = true;
            tboxINN.IsEnabled = true;
            tboxKPP.IsEnabled = true;
            tboxOGRN.IsEnabled = true;
            tboxAddressCountry.IsEnabled = true;
            tboxAddressCity.IsEnabled = true;
            tboxAddressStreet.IsEnabled = true;
            tboxAddressHouse.IsEnabled = true;
            tboxBankName.IsEnabled = true;
            tboxBankBIK.IsEnabled = true;
            tboxBankAccount.IsEnabled = true;
            tboxBankCorrAccount.IsEnabled = true;
            tboxEmployeeCount.IsEnabled = true;
            tboxRegistrationDate.IsEnabled = true;
        }
        private void BtnWithBorder_Click(object sender, RoutedEventArgs e)
        {
            EditableSettingOn();
        }

        private void btnSave_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.btnWithBorder.Click += BtnWithBorder_Click1;
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

        private void BtnWithBorder_Click1(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();


            if (tboxSurname.tb.Text == "Фамилия"
                || tboxName.tb.Text == "Имя"
                || tboxMiddlename.tb.Text == "Отчество"
                || tboxPhoneNumber.tb.Text == "Номер телефона"
                || tboxEmail.tb.Text == "Электронная почта"
                || tboxCompanyName.tb.Text == "Название компании"
                || tboxINN.tb.Text == "ИНН"
                || tboxKPP.tb.Text == "КПП"
                || tboxOGRN.tb.Text == "ОГРН"
                || tboxAddressCountry.tb.Text == "Страна"
                || tboxAddressCity.tb.Text == "Город"
                || tboxAddressStreet.tb.Text == "Улица"
                || tboxAddressHouse.tb.Text == "Дом"
                || tboxBankName.tb.Text == "Название банка"
                || tboxBankBIK.tb.Text == "БИК банка"
                || tboxBankAccount.tb.Text == "Рос. счет банка"
                || tboxBankCorrAccount.tb.Text == "Кор. счет банка"
                || tboxEmployeeCount.tb.Text == "Количество сотрудников"
                || tboxRegistrationDate.tb.Text == "Дата регистрации компании")
            {
                MessageBox.Show("Необходимо заполнить все поля");
                return;
            }

            if (ClientId != 0) // При редактировании клиента
            {
                ClientsLegalEntities selectedClient = context.ClientsLegalEntities
                    .Where(c => c.ClientsLegalEntitiesId == ClientId)
                    .FirstOrDefault();

                selectedClient.ClientsLegalEntitiesId = ClientId;
                selectedClient.ClientsLegalEntitiesContactPerson.Surname = tboxSurname.tb.Text;
                selectedClient.ClientsLegalEntitiesContactPerson.Name = tboxName.tb.Text;
                selectedClient.ClientsLegalEntitiesContactPerson.Middlename = tboxMiddlename.tb.Text;
                selectedClient.ClientsLegalEntitiesContactPerson.Phone = tboxPhoneNumber.tb.Text;
                selectedClient.ClientsLegalEntitiesContactPerson.Email= tboxEmail.tb.Text;

                selectedClient.ClientsLegalEntitiesCompanyData.CompanyName = tboxCompanyName.tb.Text;
                selectedClient.ClientsLegalEntitiesCompanyData.INN = tboxINN.tb.Text;
                selectedClient.ClientsLegalEntitiesCompanyData.KPP = tboxKPP.tb.Text;
                selectedClient.ClientsLegalEntitiesCompanyData.OGRN = tboxOGRN.tb.Text;
                selectedClient.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Street.City.Country.CountryName = tboxAddressCountry.tb.Text;
                selectedClient.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Street.City.CityName = tboxAddressCity.tb.Text;
                selectedClient.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Street.StreetName = tboxAddressStreet.tb.Text;
                selectedClient.ClientsLegalEntitiesCompanyData.AddressLegalEntities.HouseNumber = tboxAddressHouse.tb.Text;
                try
                {
                    selectedClient.ClientsLegalEntitiesCompanyData.EmployeeCount = Convert.ToInt32(tboxEmployeeCount.tb.Text);

                    DateTime.TryParse(tboxRegistrationDate.tb.Text, out DateTime res);
                    selectedClient.ClientsLegalEntitiesCompanyData.RegistrationDate = res;
                }
                catch (Exception)
                {
                }
                selectedClient.ClientsLegalEntitiesBankData.BankName = tboxBankName.tb.Text;
                selectedClient.ClientsLegalEntitiesBankData.BankBik = tboxBankBIK.tb.Text;
                selectedClient.ClientsLegalEntitiesBankData.BankAccount = tboxBankAccount.tb.Text;
                selectedClient.ClientsLegalEntitiesBankData.BankCorrAccount = tboxBankCorrAccount.tb.Text;

                if (ClientPhoto.Source != null)
                    selectedClient.ClientsLegalEntitiesContactPerson.Photo = BitmapImageToByteArray(PhotoPath);
                else
                    selectedClient.ClientsLegalEntitiesContactPerson.Photo = null;

                context.SaveChanges();
                this.Close();
            }
            else
            {
                //Создание данных контактного лица
                ClientsLegalEntitiesContactPerson newContactPeson = new ClientsLegalEntitiesContactPerson()
                {
                    Surname = tboxSurname.tb.Text,
                    Name = tboxName.tb.Text,
                    Middlename = tboxMiddlename.tb.Text,
                    Phone = tboxPhoneNumber.tb.Text,
                    Email = tboxEmail.tb.Text,
                };
                if (ClientPhoto.Source != null)
                    newContactPeson.Photo = BitmapImageToByteArray(PhotoPath);
                else
                    newContactPeson.Photo = null;

                context.ClientsLegalEntitiesContactPerson.Add(newContactPeson);
                context.SaveChanges();


                //Адресс
                var countryName = tboxAddressCountry.Text.Trim();
                var country = context.Country.FirstOrDefault(c => c.CountryName == countryName);
                if (country == null)
                {
                    country = new Country { CountryName = countryName };
                    context.Country.Add(country);
                    context.SaveChanges();
                }

                // 2. Ищем или создаём город
                var cityName = tboxAddressCity.Text.Trim();
                var city = context.City.FirstOrDefault(c => c.CityName == cityName && c.CountryId == country.CountryId);
                if (city == null)
                {
                    city = new City { CityName = cityName, CountryId = country.CountryId };
                    context.City.Add(city);
                    context.SaveChanges();
                }

                // 3. Ищем или создаём улицу
                var streetName = tboxAddressStreet.Text.Trim();
                var street = context.Street.FirstOrDefault(s => s.StreetName == streetName && s.CityId == city.CityId);
                if (street == null)
                {
                    street = new Street { StreetName = streetName, CityId = city.CityId };
                    context.Street.Add(street);
                    context.SaveChanges();
                }

                // 4. Добавляем AddressLegalEntities
                var address = new AddressLegalEntities
                {
                    StreetId = street.StreetId,
                    HouseNumber = tboxAddressHouse.Text
                };
                context.AddressLegalEntities.Add(address);
                context.SaveChanges();

                DateTime.TryParse(tboxRegistrationDate.tb.Text, out DateTime res);

                //Создание компании
                ClientsLegalEntitiesCompanyData newCompanyData = new ClientsLegalEntitiesCompanyData()
                {
                    CompanyName = tboxCompanyName.tb.Text,
                    INN = tboxINN.tb.Text,
                    KPP = tboxKPP.tb.Text,
                    OGRN = tboxOGRN.tb.Text,
                    EmployeeCount = Convert.ToInt32(tboxEmployeeCount.tb.Text),
                    RegistrationDate = res,
                    AddressLegalEntitiesId = address.AddressLegalEntitiesId
                };
                context.ClientsLegalEntitiesCompanyData.Add(newCompanyData);
                context.SaveChanges();

                //Создание банковских данных
                ClientsLegalEntitiesBankData newCompanyBank = new ClientsLegalEntitiesBankData()
                {
                    BankName = tboxBankName.tb.Text,
                    BankAccount = tboxBankAccount.tb.Text,
                    BankCorrAccount = tboxBankCorrAccount.tb.Text,
                    BankBik = tboxBankBIK.tb.Text,
                };
                context.ClientsLegalEntitiesBankData.Add(newCompanyBank);
                context.SaveChanges();

                ClientsLegalEntities newClientLegalEntitites = new ClientsLegalEntities()
                {
                    CompanyId = newCompanyData.CompanyId,
                    ContactPersonId = newContactPeson.ContactPersonId,
                    BankDataId = newCompanyBank.BankDataId
                    //Здесь добавить creatorId = указать авторизованный id менеджера
                };

                context.ClientsLegalEntities.Add(newClientLegalEntitites);
                context.SaveChanges();
                this.Close();
            }
        }
    }
}
