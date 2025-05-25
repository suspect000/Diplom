using Dickplom1.Windows.Others;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dickplom1.Pages.Manager
{
    /// <summary>
    /// Логика взаимодействия для Staff.xaml
    /// </summary>
    public partial class Staff : Page
    {
        public Staff()
        {
            InitializeComponent();
            var mainWin = Application.Current.MainWindow as MainWindow;
            mainWin.scrollMainWin.Visibility = Visibility.Collapsed;

            List<Person> people = new List<Person>
            {
                new Person {Number = "1", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FIO = "Сапожников Владислав Игоревич", Email= "sapozhnikov.slawa@gmail.ru", Post = "Старший менеджер", AccountStatus="Активен"},
                new Person {Number = "1", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FIO = "Сапожников Владислав Игоревич", Email= "sapozhnikov.slawa@gmail.ru", Post = "Старший менеджер", AccountStatus="Активен"},
                new Person {Number = "1", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FIO = "Сапожников Владислав Игоревич", Email= "sapozhnikov.slawa@gmail.ru", Post = "Старший менеджер", AccountStatus="Активен"},
                new Person {Number = "1", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FIO = "Сапожников Владислав Игоревич", Email= "sapozhnikov.slawa@gmail.ru", Post = "Старший менеджер", AccountStatus="Активен"},
                new Person {Number = "1", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FIO = "Сапожников Владислав Игоревич", Email= "sapozhnikov.slawa@gmail.ru", Post = "Старший менеджер", AccountStatus="Активен"},
                new Person {Number = "1", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FIO = "Сапожников Владислав Игоревич", Email= "sapozhnikov.slawa@gmail.ru", Post = "Старший менеджер", AccountStatus="Активен"},
                new Person {Number = "1", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FIO = "Сапожников Владислав Игоревич", Email= "sapozhnikov.slawa@gmail.ru", Post = "Старший менеджер", AccountStatus="Активен"},
                new Person {Number = "1", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FIO = "Сапожников Владислав Игоревич", Email= "sapozhnikov.slawa@gmail.ru", Post = "Старший менеджер", AccountStatus="Активен"},
                new Person {Number = "1", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FIO = "Сапожников Владислав Игоревич", Email= "sapozhnikov.slawa@gmail.ru", Post = "Старший менеджер", AccountStatus="Активен"},
                new Person {Number = "1", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FIO = "Сапожников Владислав Игоревич", Email= "sapozhnikov.slawa@gmail.ru", Post = "Старший менеджер", AccountStatus="Активен"}

            };
            dataGrid.dg.ItemsSource = people;
        }
        public class Person
        {
            public string Number { get; set; }
            public string ClientPhoto { get; set; }
            public string FIO { get; set; }
            public string Email { get; set; }
            public string Post { get; set; }
            public string AccountStatus { get; set; }
        }
    }
}