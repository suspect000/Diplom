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
    /// Логика взаимодействия для CustomPasswordWindow.xaml
    /// </summary>
    public partial class CustomPasswordWindow : Window
    {
        public CustomPasswordWindow()
        {
            InitializeComponent();
        }
        public string FullName { get; set;} = null;
        public string TempPassword { get; set; } = null;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (FullName != null)
            {
                tbFullName.Text = FullName;
            }
            if (TempPassword != null)
            {
                tbPassword.Text = TempPassword;
            }
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(tbPassword.Text);
            MessageBox.Show("Пароль скопирован в буфер обмена.");
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Class.Animations.OpacityAnimation(btnCopy, btnCopy.Opacity, 0.8, 0.2);
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Class.Animations.OpacityAnimation(btnCopy, btnCopy.Opacity, 1, 0.2);
        }

        private void Button_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Class.Animations.OpacityAnimation(btnOk, btnOk.Opacity, 0.8, 0.2);
        }

        private void Button_MouseLeave_1(object sender, MouseEventArgs e)
        {
            Class.Animations.OpacityAnimation(btnOk, btnOk.Opacity, 1, 0.2);
        }
    }
}
