using Dickplom1.Class;
using Dickplom1.DataFolder;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Xml.Linq;
using static MaterialDesignThemes.Wpf.Theme;


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
        public int ClientId { get; set; } = 0;
        /*public  string Surname { get; set; } = null;
        public  string Name { get; set; } = null;
        public  string Middlename { get; set; } = null;
        public  string PhoneNumber { get; set; } = null;
        public  string Email { get; set; } = null;*/
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
            var context = DBEntities.GetContext().ClientsNaturalPersons
                .Where(c=>c.ClientNaturalPersonsId == ClientId)
                .FirstOrDefault();

            if (context != null)
            {
                if (context.ClientPhoto != null)
                {
                    PhotoPath = LoadImage(context.ClientPhoto);
                }

                //Загрузка данных клиента в текстовые поля и изображение
                if (ClientId != 0)
                {
                    tboxSurname.tb.Text = context.Surname;
                    tboxName.tb.Text = context.Name;
                    tboxMiddleName.tb.Text = context.MiddleName;
                    tboxPhoneNumber.tb.Text = context.PhoneNumber;
                    tboxEmail.tb.Text = context.Email;
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
        public void ForEditWin()
        {
            Dickplom1.Class.Musor.ShowElement(edit1);
            Dickplom1.Class.Musor.ShowElement(edit2);
            Dickplom1.Class.Musor.ShowElement(edit3);
            Dickplom1.Class.Musor.ShowElement(edit4);
            Dickplom1.Class.Musor.ShowElement(edit5);

            Dickplom1.Class.Musor.HideElement(btnSave.btnWithBorder);

            tboxSurname.IsEnabled = false;
            tboxName.IsEnabled = false;
            tboxMiddleName.IsEnabled = false;
            tboxPhoneNumber.IsEnabled = false;
            tboxEmail.IsEnabled = false;
        }
        public void ForCreateWin()
        {
            Dickplom1.Class.Musor.HideElement(edit1);
            Dickplom1.Class.Musor.HideElement(edit2);
            Dickplom1.Class.Musor.HideElement(edit3);
            Dickplom1.Class.Musor.HideElement(edit4);
            Dickplom1.Class.Musor.HideElement(edit5);

            Dickplom1.Class.Musor.ShowElement(btnSave.btnWithBorder);

            tboxSurname.IsEnabled = true;
            tboxName.IsEnabled = true;
            tboxMiddleName.IsEnabled = true;
            tboxPhoneNumber.IsEnabled = true;
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
                Dickplom1.Class.Musor.ShowElement(btnSave.btnWithBorder);
            }
                

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
            MessageBoxButton btns = MessageBoxButton.YesNo;
            MessageBoxResult mb = MessageBox.Show("Вы уверенны?", "Внимание", btns);
            if (mb == MessageBoxResult.Yes)
            {
                ClientPhoto.Source = null;
                imgDelete.Visibility = Visibility.Collapsed;
                ClientPhotoFI.Visibility = Visibility.Visible;
                Dickplom1.Class.Musor.ShowElement(btnSave.btnWithBorder);
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

        private void ButtomWithBorder_Loaded(object sender, RoutedEventArgs e)
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
                || tboxPhoneNumber.tb.Text == "Номер телефона"
                || tboxEmail.tb.Text == "Электронная почта")
                {
                    MessageBox.Show("Необходимо заполнить все поля");
                    return;
                }
                //Проверки
                if (tboxPhoneNumber.tb.Text.Length < 11)
                {
                    MessageBox.Show("Номер телефона должен содержать 11 цифр");
                    return;
                }
                if (!tboxPhoneNumber.tb.Text.StartsWith("8") && !tboxPhoneNumber.tb.Text.StartsWith("7"))
                {
                    MessageBox.Show("Номер телефона должен начинаться на 7 или 8");
                    return;
                }
                if (!tboxEmail.tb.Text.Contains("@") | !tboxEmail.tb.Text.Contains("."))
                {
                    MessageBox.Show("Неправильный формат электронной почты");
                    return;
                }
                //________________________________________________________________________________

                if (ClientId != 0) // При редактировании клиента
                {
                    ClientsNaturalPersons selectedClient = context.ClientsNaturalPersons
                        .Where(c => c.ClientNaturalPersonsId == ClientId)
                        .FirstOrDefault();

                    var clientConflictPhoneNumber = context.ClientsNaturalPersons.FirstOrDefault(f => f.ClientNaturalPersonsId != selectedClient.ClientNaturalPersonsId && f.PhoneNumber == tboxPhoneNumber.tb.Text);
                    if (clientConflictPhoneNumber != null)
                    {
                        MessageBox.Show("Клиент с таким номером телефона уже есть в системе");
                        return;
                    }

                    var clientConflictEmail = context.ClientsNaturalPersons.FirstOrDefault(f => f.ClientNaturalPersonsId != selectedClient.ClientNaturalPersonsId && f.Email == tboxEmail.tb.Text);
                    if (clientConflictEmail != null)
                    {
                        MessageBox.Show("Клиент с такой электронной почтой уже есть в системе");
                        return;
                    }

                    selectedClient.ClientNaturalPersonsId = ClientId;
                    selectedClient.Surname = tboxSurname.tb.Text;
                    selectedClient.Name = tboxName.tb.Text;
                    selectedClient.MiddleName = tboxMiddleName.tb.Text;
                    selectedClient.PhoneNumber = tboxPhoneNumber.tb.Text;
                    selectedClient.Email = tboxEmail.tb.Text;

                    if (ClientPhoto.Source != null)
                        selectedClient.ClientPhoto = BitmapImageToByteArray(PhotoPath);
                    else
                        selectedClient.ClientPhoto = null;



                    context.SaveChanges();
                    MessageBox.Show("Запись успешно обновлена");
                    this.Close();
                }
                else
                {
                    ClientsNaturalPersons selectedClient = new ClientsNaturalPersons()
                    {
                        Surname = tboxSurname.tb.Text,
                        Name = tboxName.tb.Text,
                        MiddleName = tboxMiddleName.tb.Text,
                        PhoneNumber = tboxPhoneNumber.tb.Text,
                        Email = tboxEmail.tb.Text,
                        CreatorId = mainWin?.ActiveUser.UserId ?? 0
                    };

                    var clientConflictPhoneNumber = context.ClientsNaturalPersons.FirstOrDefault(f => f.PhoneNumber == selectedClient.PhoneNumber);
                    if (clientConflictPhoneNumber != null)
                    {
                        MessageBox.Show("Клиент с таким номером телефона уже есть в системе");
                        return;
                    }

                    var clientConflictEmail = context.ClientsNaturalPersons.FirstOrDefault(f => f.Email == selectedClient.Email);
                    if (clientConflictEmail != null)
                    {
                        MessageBox.Show("Клиент с такой электронной почтой уже есть в системе");
                        return;
                    }

                    if (ClientPhoto.Source != null)
                        selectedClient.ClientPhoto = BitmapImageToByteArray(PhotoPath);
                    else
                        selectedClient.ClientPhoto = null;

                    context.ClientsNaturalPersons.Add(selectedClient);
                    context.SaveChanges();
                    MessageBox.Show("Запись успешно добавлена");
                    this.Close();
                }
            }
            catch (Exception)
            {

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

        private void edit1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tboxSurname.IsEnabled = true;

            Dickplom1.Class.Musor.ShowElement(save1);
            Dickplom1.Class.Musor.HideElement(edit1);

            if (btnSave.btnWithBorder.Visibility == Visibility.Collapsed)
                Dickplom1.Class.Musor.ShowElement(btnSave.btnWithBorder);
        }

        private void save1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tboxSurname.IsEnabled = false;
            Dickplom1.Class.Musor.ShowElement(edit1);
            Dickplom1.Class.Musor.HideElement(save1);
        }

        private void edit2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tboxName.IsEnabled = true;

            Dickplom1.Class.Musor.ShowElement(save2);
            Dickplom1.Class.Musor.HideElement(edit2);

            if (btnSave.btnWithBorder.Visibility == Visibility.Collapsed)
                Dickplom1.Class.Musor.ShowElement(btnSave.btnWithBorder);
        }

        private void save2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tboxName.IsEnabled = false;
            Dickplom1.Class.Musor.ShowElement(edit2);
            Dickplom1.Class.Musor.HideElement(save2);
        }

        private void edit3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tboxMiddleName.IsEnabled = true;

            Dickplom1.Class.Musor.ShowElement(save3);
            Dickplom1.Class.Musor.HideElement(edit3);

            if (btnSave.btnWithBorder.Visibility == Visibility.Collapsed)
                Dickplom1.Class.Musor.ShowElement(btnSave.btnWithBorder);
        }

        private void save3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tboxMiddleName.IsEnabled = false;
            Dickplom1.Class.Musor.ShowElement(edit3);
            Dickplom1.Class.Musor.HideElement(save3);
        }

        private void edit4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tboxPhoneNumber.IsEnabled = true;

            Dickplom1.Class.Musor.ShowElement(save4);
            Dickplom1.Class.Musor.HideElement(edit4);

            if (btnSave.btnWithBorder.Visibility == Visibility.Collapsed)
                Dickplom1.Class.Musor.ShowElement(btnSave.btnWithBorder);
        }

        private void save4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tboxPhoneNumber.IsEnabled = false;
            Dickplom1.Class.Musor.ShowElement(edit4);
            Dickplom1.Class.Musor.HideElement(save4);
        }

        private void edit5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tboxEmail.IsEnabled = true;

            Dickplom1.Class.Musor.ShowElement(save5);
            Dickplom1.Class.Musor.HideElement(edit5);

            if (btnSave.btnWithBorder.Visibility == Visibility.Collapsed)
                Dickplom1.Class.Musor.ShowElement(btnSave.btnWithBorder);
        }

        private void save5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tboxEmail.IsEnabled = false;
            Dickplom1.Class.Musor.ShowElement(edit5);
            Dickplom1.Class.Musor.HideElement(save5);
        }

        private void tboxSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Zа-яА-ЯёЁ]+$");
        }

        private void tboxSurname_Loaded(object sender, RoutedEventArgs e)
        {
            tboxSurname.tb.MaxLength = 50;
        }

        private void tboxName_Loaded(object sender, RoutedEventArgs e)
        {
            tboxName.tb.MaxLength = 50;
        }

        private void tboxMiddleName_Loaded(object sender, RoutedEventArgs e)
        {
            tboxMiddleName.tb.MaxLength = 50;
        }

        private void tboxPhoneNumber_Loaded(object sender, RoutedEventArgs e)
        {
            tboxPhoneNumber.tb.MaxLength = 11;
        }

        private void tboxPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");

            if (e.Text != "8" & e.Text != "7" && tboxPhoneNumber.tb.SelectionStart == 0)
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

        private void tboxName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Zа-яА-ЯёЁ]+$");
        }

        private void tboxMiddleName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[a-zA-Zа-яА-ЯёЁ]+$");
        }
    }
}
