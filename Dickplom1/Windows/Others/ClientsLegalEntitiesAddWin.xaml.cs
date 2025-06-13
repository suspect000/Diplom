using Dickplom1.Class;
using Dickplom1.DataFolder;
using Dickplom1.Resources.Images.OtherWins;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
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
            tboxAddressApartment.IsEnabled = false;
            tboxAddressOffice.IsEnabled = false;
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
            tboxAddressApartment.IsEnabled = true;
            tboxAddressOffice.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext().ClientsLegalEntities
                .Where(c => c.ClientsLegalEntitiesId == ClientId)
                .FirstOrDefault();


            if (context != null)
            {
                var clientContactPerson = DBEntities.GetContext()
                    .ClientsLegalEntitiesContactPerson
                    .Where(w => w.IsActive == true && w.CompanyId == context.CompanyId)
                    .FirstOrDefault();

                if (clientContactPerson.Photo != null)
                {
                    PhotoPath = LoadImage(clientContactPerson.Photo);
                }

                //Загрузка данных клиента в текстовые поля и изображение
                if (ClientId != 0)
                {
                    tboxSurname.tb.Text = clientContactPerson.Surname;
                    tboxName.tb.Text = clientContactPerson.Name;
                    tboxPhoneNumber.tb.Text = clientContactPerson.Phone;
                    tboxEmail.tb.Text = clientContactPerson.Email;

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

                    if (clientContactPerson.Middlename != null)
                        tboxMiddlename.tb.Text = clientContactPerson.Middlename;

                    if (context.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Apartment != null)
                        tboxAddressApartment.tb.Text = context.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Apartment;

                    if (context.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Office != null)
                        tboxAddressOffice.tb.Text = context.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Office;

                    OnSelecttorActiveContactPersons();
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
                OffSelecttorActiveContactPersons();
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
                //this.DragMove();
            } 

            
        }

        private void gridMovingWin_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //e.Handled = true;
        }

        private void mainGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /*gridFocus.Focus();
            Keyboard.ClearFocus();*/
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
            tboxAddressApartment.IsEnabled = true;
            tboxAddressOffice.IsEnabled = true;

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
                || tboxRegistrationDate.tb.Text == "Дата регистрации компании"

                || string.IsNullOrWhiteSpace(tboxSurname.tb.Text)
                || string.IsNullOrWhiteSpace(tboxName.tb.Text)
                || string.IsNullOrWhiteSpace(tboxPhoneNumber.tb.Text)
                || string.IsNullOrWhiteSpace(tboxEmail.tb.Text)
                || string.IsNullOrWhiteSpace(tboxCompanyName.tb.Text)
                || string.IsNullOrWhiteSpace(tboxINN.tb.Text)
                || string.IsNullOrWhiteSpace(tboxKPP.tb.Text)
                || string.IsNullOrWhiteSpace(tboxOGRN.tb.Text)
                || string.IsNullOrWhiteSpace(tboxAddressCountry.tb.Text)
                || string.IsNullOrWhiteSpace(tboxAddressCity.tb.Text)
                || string.IsNullOrWhiteSpace(tboxAddressStreet.tb.Text)
                || string.IsNullOrWhiteSpace(tboxAddressHouse.tb.Text)
                || string.IsNullOrWhiteSpace(tboxBankName.tb.Text)
                || string.IsNullOrWhiteSpace(tboxBankBIK.tb.Text)
                || string.IsNullOrWhiteSpace(tboxBankAccount.tb.Text)
                || string.IsNullOrWhiteSpace(tboxBankCorrAccount.tb.Text)
                || string.IsNullOrWhiteSpace(tboxEmployeeCount.tb.Text)
                || string.IsNullOrWhiteSpace(tboxRegistrationDate.tb.Text))
            {
                MessageBox.Show("Необходимо заполнить все поля");
                return;
            }

            if (ClientId != 0) // При редактировании клиента
            {
                ClientsLegalEntities selectedClient = context.ClientsLegalEntities
                    .Where(c => c.ClientsLegalEntitiesId == ClientId)
                    .FirstOrDefault();

                ClientsLegalEntitiesContactPerson firstClientContactPerson = selectedClient
                    .ClientsLegalEntitiesCompanyData
                    .ClientsLegalEntitiesContactPerson
                    .FirstOrDefault(f=>f.IsActive == true);

                ClientsLegalEntitiesContactPerson selectedClientContactPerson = context
                    .ClientsLegalEntitiesContactPerson
                    .Where(w=>w.ContactPersonId == (int)cboxActiveContactPerson.cbox.SelectedValue)
                    .FirstOrDefault();


                //selectedClient.ClientsLegalEntitiesId = ClientId;

                if (firstClientContactPerson.ContactPersonId != selectedClientContactPerson.ContactPersonId)
                {
                    var clients = context.ClientsLegalEntitiesContactPerson
                        .Where(c => c.CompanyId == selectedClient.CompanyId)
                        .ToList();

                    foreach (var client in clients)
                    {
                        client.IsActive = false;
                    }
                }

                selectedClientContactPerson.Surname = tboxSurname.tb.Text;
                selectedClientContactPerson.Name = tboxName.tb.Text;

                if (tboxMiddlename.tb.Text != "Отчество" && !string.IsNullOrWhiteSpace(tboxMiddlename.tb.Text))
                    selectedClientContactPerson.Middlename = tboxMiddlename.tb.Text;
                else
                    selectedClientContactPerson.Middlename = "-";

                selectedClientContactPerson.Phone = tboxPhoneNumber.tb.Text;
                selectedClientContactPerson.Email= tboxEmail.tb.Text;
                selectedClientContactPerson.IsActive = true;

                selectedClient.ClientsLegalEntitiesCompanyData.CompanyName = tboxCompanyName.tb.Text;
                selectedClient.ClientsLegalEntitiesCompanyData.INN = tboxINN.tb.Text;
                selectedClient.ClientsLegalEntitiesCompanyData.KPP = tboxKPP.tb.Text;
                selectedClient.ClientsLegalEntitiesCompanyData.OGRN = tboxOGRN.tb.Text;
                selectedClient.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Street.City.Country.CountryName = tboxAddressCountry.tb.Text;
                selectedClient.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Street.City.CityName = tboxAddressCity.tb.Text;
                selectedClient.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Street.StreetName = tboxAddressStreet.tb.Text;
                selectedClient.ClientsLegalEntitiesCompanyData.AddressLegalEntities.HouseNumber = tboxAddressHouse.tb.Text;

                if (tboxAddressApartment.tb.Text != "Квартира" && !string.IsNullOrWhiteSpace(tboxAddressApartment.tb.Text))
                    selectedClientContactPerson.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Apartment = tboxAddressApartment.tb.Text;
                else
                    selectedClientContactPerson.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Apartment = "-";

                if (tboxAddressOffice.tb.Text != "Офис" && !string.IsNullOrWhiteSpace(tboxAddressOffice.tb.Text))
                    selectedClientContactPerson.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Office = tboxAddressOffice.tb.Text;
                else
                    selectedClientContactPerson.ClientsLegalEntitiesCompanyData.AddressLegalEntities.Office = "-";

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
                    selectedClientContactPerson.Photo = BitmapImageToByteArray(PhotoPath);
                else
                    selectedClientContactPerson.Photo = null;

                context.SaveChanges();
                Thread.Sleep(100);
                MessageBox.Show("Запись успешно обновлена");
                this.Close();
            }
            else // При создании клиента
            {
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

                if (tboxAddressApartment.Text != "Квартира" && !string.IsNullOrWhiteSpace(tboxAddressApartment.Text))
                    address.Apartment = tboxAddressApartment.Text;

                if (tboxAddressOffice.Text != "Офис" && !string.IsNullOrWhiteSpace(tboxAddressOffice.Text))
                    address.Office = tboxAddressOffice.Text;

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
                    //ContactPersonId = newContactPeson.ContactPersonId,
                    BankDataId = newCompanyBank.BankDataId
                    //Здесь добавить creatorId = указать авторизованный id менеджера
                };

                context.ClientsLegalEntities.Add(newClientLegalEntitites);
                context.SaveChanges();

                //Создание данных контактного лица
                ClientsLegalEntitiesContactPerson newContactPeson = new ClientsLegalEntitiesContactPerson()
                {
                    Surname = tboxSurname.tb.Text,
                    Name = tboxName.tb.Text,
                    Phone = tboxPhoneNumber.tb.Text,
                    Email = tboxEmail.tb.Text,
                    IsActive = true,
                    CompanyId = context.ClientsLegalEntitiesCompanyData.FirstOrDefault(f=>f.CompanyName == newCompanyData.CompanyName).CompanyId
                };
                if (tboxMiddlename.Text != "Отчество" && !string.IsNullOrWhiteSpace(tboxMiddlename.Text))
                    newContactPeson.Middlename = tboxMiddlename.tb.Text;

                if (ClientPhoto.Source != null)
                    newContactPeson.Photo = BitmapImageToByteArray(PhotoPath);
                else
                    newContactPeson.Photo = null;

                context.ClientsLegalEntitiesContactPerson.Add(newContactPeson);
                context.SaveChanges();
                Thread.Sleep(200);
                MessageBox.Show("Запись успешно добавлена");
                this.Close();
            }
        }

        private void cboxActiveContactPerson_Loaded(object sender, RoutedEventArgs e)
        {
            cboxContactPersonRefresh();
        }

        //Загрузка данных в комбобокс выбора представителей (должен быть выбран активный представитель)
        public void cboxContactPersonRefresh()
        {
            var context = DBEntities.GetContext();

            var selectedClientLegal = context.ClientsLegalEntities
                .FirstOrDefault(f => f.ClientsLegalEntitiesId == ClientId);

            if (ClientId != null && ClientId != 0 && selectedClientLegal != null)
            {
                var items = new List<object>();

                try
                {
                    // Заглушка
                    items.Add(new { ContactPersonId = 0, ContactPersonName = "Активный представитель" });

                    items.AddRange(context
                        .ClientsLegalEntitiesContactPerson
                        .Where(w => w.ClientsLegalEntitiesCompanyData.CompanyId == selectedClientLegal.CompanyId)
                        .Select(u => new
                        {
                            ContactPersonId = u.ContactPersonId,
                            ContactPersonName = u.Surname
                            + " " + u.Name
                            + " " + u.Middlename
                        }));

                    cboxActiveContactPerson.cbox.ItemsSource = items;
                    cboxActiveContactPerson.cbox.DisplayMemberPath = "ContactPersonName";
                    cboxActiveContactPerson.cbox.SelectedValuePath = "ContactPersonId";
                    cboxActiveContactPerson.cbox.SelectedValue = context
                        .ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.CompanyId == selectedClientLegal.CompanyId
                        && w.IsActive == true)
                        .ContactPersonId;
                    cboxActiveContactPerson.cbox.SelectionChanged += Cbox_SelectionChanged;


                }
                catch (Exception ex)
                {

                }
            }
        }

        private void Cbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboxActiveContactPerson.cbox.SelectedValue != null && (int)cboxActiveContactPerson.cbox.SelectedValue != 0)
            {
                var clientContactPerson = DBEntities.GetContext().ClientsLegalEntitiesContactPerson
               .Where(c => c.ContactPersonId == (int)cboxActiveContactPerson.cbox.SelectedValue)
               .FirstOrDefault();

                try
                {
                    if (clientContactPerson != null)
                    {
                        if (clientContactPerson.Photo != null)
                            PhotoPath = LoadImage(clientContactPerson.Photo);
                        else
                        {
                            PhotoPath = null;
                            ClientPhoto.Source = null;
                        }
                        

                        //Загрузка данных клиента в текстовые поля и изображение
                        if (ClientId != 0)
                        {
                            tboxSurname.tb.Text = clientContactPerson.Surname;
                            tboxName.tb.Text = clientContactPerson.Name;
                            tboxMiddlename.tb.Text = clientContactPerson.Middlename;
                            tboxPhoneNumber.tb.Text = clientContactPerson.Phone;
                            tboxEmail.tb.Text = clientContactPerson.Email;
                            OnTbocks();
                            EditableSettingOn();
                        }
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
                catch (Exception)
                {

                }
            }
            else if (cboxActiveContactPerson.cbox.SelectedValue != null 
                && (int)cboxActiveContactPerson.cbox.SelectedValue == 0)
            {
                PhotoPath = null;
                tboxSurname.tb.Text = null;
                tboxName.tb.Text = null;
                tboxMiddlename.tb.Text = null;
                tboxPhoneNumber.tb.Text = null;
                tboxEmail.tb.Text = null;
                OffTbocks();

                EditableSettingOn();
            }
        }
        public void OffTbocks()
        {
            tboxSurname.tb.IsEnabled = false;
            tboxName.tb.IsEnabled = false;
            tboxMiddlename.tb.IsEnabled = false;
            tboxPhoneNumber.tb.IsEnabled = false;
            tboxEmail.tb.IsEnabled = false;

            tboxSurname.tb.Opacity = 0.5;
            tboxName.tb.Opacity = 0.5;
            tboxMiddlename.tb.Opacity = 0.5;
            tboxPhoneNumber.tb.Opacity = 0.5;
            tboxEmail.tb.Opacity = 0.5;
        }
        public void OnTbocks()
        {
            tboxSurname.tb.IsEnabled = true;
            tboxName.tb.IsEnabled = true;
            tboxMiddlename.tb.IsEnabled = true;
            tboxPhoneNumber.tb.IsEnabled = true;
            tboxEmail.tb.IsEnabled = true;

            tboxSurname.tb.Opacity = 1;
            tboxName.tb.Opacity = 1;
            tboxMiddlename.tb.Opacity = 1;
            tboxPhoneNumber.tb.Opacity = 1;
            tboxEmail.tb.Opacity = 1;
        }
        public void OffSelecttorActiveContactPersons()
        {
            selectorActiveContactPersons.IsEnabled = false;
            selectorActiveContactPersons.Opacity = 0.5;

            
        }
        public void OnSelecttorActiveContactPersons()
        {
            selectorActiveContactPersons.IsEnabled = true;
            selectorActiveContactPersons.Opacity = 1;
        }
        private void btnAddPlus_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ClientId != 0)
            {
                ContactPersonAdd win = new ContactPersonAdd();
                win.CompanyId = (int)DBEntities.GetContext().ClientsLegalEntities
                    .FirstOrDefault(f=>f.ClientsLegalEntitiesId == ClientId)
                    .CompanyId;
                win.Closed += Win_Closed;
                win.ShowDialog();
            }
        }

        private void Win_Closed(object sender, EventArgs e)
        {
            cboxContactPersonRefresh();
        }
    }
}
