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
    /// Логика взаимодействия для Subscriptions.xaml
    /// </summary>
    public partial class Subscriptions : Page
    {
        public Subscriptions()
        {
            InitializeComponent();

            List<Person> people = new List<Person>
            {
                new Person {Number = "1", SubscriptionName = "Корпоративная", SubscriptionPeriod = "1 месяц", SubscriptionType = "Корпоративная", PriceForMonth = "777 руб", Comment="-", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", SubscriptionPeriod = "1 месяц", SubscriptionType = "Корпоративная", PriceForMonth = "777 руб", Comment="-", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", SubscriptionPeriod = "1 месяц", SubscriptionType = "Корпоративная", PriceForMonth = "777 руб", Comment="-", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", SubscriptionPeriod = "1 месяц", SubscriptionType = "Корпоративная", PriceForMonth = "777 руб", Comment="-", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", SubscriptionPeriod = "1 месяц", SubscriptionType = "Корпоративная", PriceForMonth = "777 руб", Comment="-", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", SubscriptionPeriod = "1 месяц", SubscriptionType = "Корпоративная", PriceForMonth = "777 руб", Comment="-", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", SubscriptionPeriod = "1 месяц", SubscriptionType = "Корпоративная", PriceForMonth = "777 руб", Comment="-", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", SubscriptionPeriod = "1 месяц", SubscriptionType = "Корпоративная", PriceForMonth = "777 руб", Comment="-", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", SubscriptionPeriod = "1 месяц", SubscriptionType = "Корпоративная", PriceForMonth = "777 руб", Comment="-", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", SubscriptionPeriod = "1 месяц", SubscriptionType = "Корпоративная", PriceForMonth = "777 руб", Comment="-", FIOManager="Сапожников В. И."}

            };
            dataGrid.dg.ItemsSource = people;
        }
        public class Person
        {
            public string Number { get; set; }
            public string SubscriptionName { get; set; }
            public string SubscriptionPeriod { get; set; }
            public string SubscriptionType { get; set; }
            public string PriceForMonth { get; set; }
            public string Comment { get; set; }
            public string FIOManager { get; set; }

        }

        private void btnAddOrder_Loaded(object sender, RoutedEventArgs e)
        {
            btnAddOrder.btnWithBorder.Click += BtnWithBorder_Click;
        }

        private void BtnWithBorder_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionAddWin win = new SubscriptionAddWin();
            win.ShowDialog();
        }
    }
}