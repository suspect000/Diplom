using Dickplom1.Class;
using Dickplom1.DataFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для SubscriptionPeriodAdd.xaml
    /// </summary>
    public partial class ChangePasswordWin : Window
    {
        public ChangePasswordWin()
        {
            InitializeComponent();
        }
        public Users ActiveUser { get; set; } = null;

        private void btnSave_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.btnWithBorder.Click += BtnWithBorder_Click;
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void pbNewPassword_Loaded(object sender, RoutedEventArgs e)
        {
            pbNewPassword.Pb.Password = "Пароль";
            pbNewPassword.Pb.PasswordChanged += Pb_PasswordChanged;
        }

        private void Pb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbNewPassword.Password == "Пароль")
            {
                pbNewPassword.Pb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B25B5D61"));

            }
            else
            {
                pbNewPassword.Pb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#687183"));
            }
        }

        private void pbNewPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (pbNewPassword.Pb.Password == "Пароль")
            {
                pbNewPassword.Pb.Password = string.Empty;
            }
        }

        private void pbNewPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(pbNewPassword.Pb.Password))
            {
                pbNewPassword.Pb.Password = "Пароль";
            }
        }

        private void pbNewPasswordRepeat_GotFocus(object sender, RoutedEventArgs e)
        {
            if (pbNewPasswordRepeat.Pb.Password == "Пароль")
            {
                pbNewPasswordRepeat.Pb.Password = string.Empty;
            }
        }

        private void pbNewPasswordRepeat_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(pbNewPassword.Pb.Password))
            {
                pbNewPasswordRepeat.Pb.Password = "Пароль";
            }
        }

        private void pbNewPasswordRepeat_Loaded_1(object sender, RoutedEventArgs e)
        {
            pbNewPasswordRepeat.Pb.Password = "Пароль";
            pbNewPasswordRepeat.Pb.PasswordChanged += Pb_PasswordChanged1;
        }

        private void Pb_PasswordChanged1(object sender, RoutedEventArgs e)
        {
            if (pbNewPasswordRepeat.Password == "Пароль")
            {
                pbNewPasswordRepeat.Pb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B25B5D61"));

            }
            else
            {
                pbNewPasswordRepeat.Pb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#687183"));
            }
        }
        private void BtnWithBorder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var context = DBEntities.GetContext();
                string newPassword = pbNewPassword.Password;
                string confirmPassword = pbNewPasswordRepeat.Password;

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("Пароли не совпадают");
                    return;
                }

                if (ActiveUser != null)
                {
                    ActiveUser.PasswordHash = Dickplom1.Class.PasswordHelper.HashPassword(newPassword);
                    ActiveUser.IsTemporaryPassword = false;
                    ActiveUser.AccountStatusId = 1;

                    context.SaveChanges();

                    MessageBox.Show("Пароль успешно изменён");

                    var mainWindow = new MainWindow();
                    mainWindow.ActiveUser = ActiveUser;
                    MessageBox.Show("Данные применятся после перезапуска приложения");
                    mainWindow.ShowDialog();
                    this.Close();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
