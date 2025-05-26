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
    /// Логика взаимодействия для OrdersNaturalPersons.xaml
    /// </summary>
    public partial class OrdersNaturalPersons : Page
    {
        public OrdersNaturalPersons()
        {
            InitializeComponent();

            List<Person> people = new List<Person>
            {
                new Person {Number = "1", SubscriptionName = "Базовая", FullNameClient = "Сапожников Владислав Игоревич", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Базовая", FullNameClient = "Сапожников Владислав Игоревич", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Базовая", FullNameClient = "Сапожников Владислав Игоревич", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Базовая", FullNameClient = "Сапожников Владислав Игоревич", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Базовая", FullNameClient = "Сапожников Владислав Игоревич", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Базовая", FullNameClient = "Сапожников Владислав Игоревич", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Базовая", FullNameClient = "Сапожников Владислав Игоревич", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Базовая", FullNameClient = "Сапожников Владислав Игоревич", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Базовая", FullNameClient = "Сапожников Владислав Игоревич", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Базовая", FullNameClient = "Сапожников Владислав Игоревич", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."}

            };
            DataGridCustomForOrdersNaturalPersons.dg.ItemsSource = people;
        }
        public class Person
        {
            public string Number { get; set; }
            public string SubscriptionName { get; set; }
            public string FullNameClient { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string OrderStatus { get; set; }
            public string FIOManager { get; set; }

        }

        private void ButtomWithBorder_Loaded(object sender, RoutedEventArgs e)
        {
            btnAddOrder.btnWithBorder.Click += BtnWithBorder_Click;
        }

        private void BtnWithBorder_Click(object sender, RoutedEventArgs e)
        {
            OrdersNaturalPersonsAddWin win = new OrdersNaturalPersonsAddWin();
            win.ShowDialog();
        }
    }
}
