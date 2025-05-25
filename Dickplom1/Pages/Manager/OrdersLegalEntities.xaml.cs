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
    /// Логика взаимодействия для OrdersLegalEntities.xaml
    /// </summary>
    public partial class OrdersLegalEntities : Page
    {
        public OrdersLegalEntities()
        {
            InitializeComponent();
            var mainWin = Application.Current.MainWindow as MainWindow;
            mainWin.scrollMainWin.Visibility = Visibility.Collapsed;

            List<Person> people = new List<Person>
            {
                new Person {Number = "1", SubscriptionName = "Корпоративная", CompanyName= "ООО 'Ромашка'", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", CompanyName= "ООО 'Ромашка'", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", CompanyName= "ООО 'Ромашка'", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", CompanyName= "ООО 'Ромашка'", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", CompanyName= "ООО 'Ромашка'", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", CompanyName= "ООО 'Ромашка'", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", CompanyName= "ООО 'Ромашка'", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", CompanyName= "ООО 'Ромашка'", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", CompanyName= "ООО 'Ромашка'", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."},
                new Person {Number = "1", SubscriptionName = "Корпоративная", CompanyName= "ООО 'Ромашка'", StartDate = "22.05.25", EndDate = "22.06.25", OrderStatus="Оплата", FIOManager="Сапожников В. И."}

            };
            dataGrid.dg.ItemsSource = people;
        }
        public class Person
        {
            public string Number { get; set; }
            public string SubscriptionName { get; set; }
            public string CompanyName { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string OrderStatus { get; set; }
            public string FIOManager { get; set; }

        }

    }
}