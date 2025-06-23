using Dickplom1.Class;
using Dickplom1.DataFolder;
using Dickplom1.Windows.Others;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace Dickplom1.Windows
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void tboxName_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void pbPassword_Loaded(object sender, RoutedEventArgs e)
        {
            pbPassword.Pb.Password = "Пароль";
            pbPassword.Pb.PasswordChanged += Pb_PasswordChanged;
        }

        private void Pb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbPassword.Password == "Пароль")
            {
                pbPassword.Pb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B25B5D61"));

            }
            else
            {
                pbPassword.Pb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#687183"));
            }
        }

        private void ButtonClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void pbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (pbPassword.Pb.Password == "Пароль")
            {
                pbPassword.Pb.Password = string.Empty;
            }
        }
        private void pbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(pbPassword.Pb.Password))
            {
                pbPassword.Pb.Password = "Пароль";
            }
        }

        private void ButtomWithBorder_Loaded(object sender, RoutedEventArgs e)
        {
            btnJoin.btnWithBorder.Click += BtnWithBorder_Click;
        }

        public Users ActiveUser { get; private set; } = null;
        private void BtnWithBorder_Click(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();
            string login = tboxLogin.tb.Text;
            string password = pbPassword.Pb.Password;


            if (tboxLogin.tb.Text == "Логин" 
                | string.IsNullOrWhiteSpace(tboxLogin.tb.Text) 
                | pbPassword.Pb.Password == "Пароль" 
                | string.IsNullOrWhiteSpace(pbPassword.Pb.Password))
            {
                MessageBox.Show("Необходимо заполнить все поля");
                return;
            }

            var user = context.Users.FirstOrDefault(u => u.Login == login);

            if (user != null && Dickplom1.Class.PasswordHelper.VerifyPassword(password, user.PasswordHash))
            {
 
                if (user.AccountStatusId == 3)
                {
                    MessageBox.Show("Ваша учетная запись заблокирована\nОбратитесь к администратору за помощью");
                    return;
                }

                if ((bool)user.IsTemporaryPassword)
                {
                    // Открываем окно смены пароля
                    this.ActiveUser = user;
                    var changePassWindow = new ChangePasswordWin();
                    changePassWindow.ActiveUser = user;
                    MessageBox.Show("Необходимо сбросить пароль");
                    changePassWindow.ShowDialog();
                    this.Close();
                }
                else if (user.AccountStatusId == 2)
                {
                    MessageBox.Show("Ваша учетная запись неактивна\nОбратитесь к администратору за помощью");
                    return;
                }
                else
                {
                    var mainWin = Application.Current.MainWindow as MainWindow;
                    mainWin.ActiveUser = user;
                    mainWin.Join();
                    //this.ActiveUser = user;
                    this.DialogResult = true;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
    }
}
