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
using static Dickplom1.Pages.Manager.Dashboards;

namespace Dickplom1.Pages.Manager
{
    /// <summary>
    /// Логика взаимодействия для ClientsLegalEntities.xaml
    /// </summary>
    public partial class ClientsLegalEntities : Page
    {
        public ClientsLegalEntities()
        {
            InitializeComponent();

            List<Person> people = new List<Person>
            {
                new Person {Number = "1", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FullName = "Аюпов В. И.", CompanyName = $"ООО {"Ромашка"}", Email="sapozhnikov@gmail.com", SubscriptionStatus="Не оформлена"},
                new Person {Number = "2", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FullName = "Коженков В. И.", CompanyName = $"ООО {"Ромашка"}", Email="sapozhnikov@gmail.com", SubscriptionStatus="Не оформлена"},
                new Person {Number = "3", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FullName = "Никольский В. И.", CompanyName = $"ООО {"Ромашка"}", Email="sapozhnikov@gmail.com", SubscriptionStatus="Не оформлена"},
                new Person {Number = "4", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FullName = "Сапожников В. И.", CompanyName = $"ООО {"Ромашка"}", Email="sapozhnikov@gmail.com", SubscriptionStatus="Не оформлена"},
                new Person {Number = "5", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FullName = "Сапожников В. И.", CompanyName = $"ООО {"Ромашка"}", Email="sapozhnikov@gmail.com", SubscriptionStatus="Не оформлена"},
                new Person {Number = "6", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FullName = "Сапожников В. И.", CompanyName = $"ООО {"Ромашка"}", Email="sapozhnikov@gmail.com", SubscriptionStatus="Не оформлена"},
                new Person {Number = "7", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FullName = "Сапожников В. И.", CompanyName = $"ООО {"Ромашка"}", Email="sapozhnikov@gmail.com", SubscriptionStatus="Не оформлена"},
                new Person {Number = "8", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FullName = "Сапожников В. И.", CompanyName = $"ООО {"Ромашка"}", Email="sapozhnikov@gmail.com", SubscriptionStatus="Не оформлена"},
                new Person {Number = "9", ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FullName = "Сапожников В. И.", CompanyName = $"ООО {"Ромашка"}", Email="sapozhnikov@gmail.com", SubscriptionStatus="Не оформлена"},
                new Person {Number = "10",ClientPhoto = "..//..//Resources/Images/Dashboards/Sidny.png", FullName = "Сапожников В. И.", CompanyName = $"ООО {"Ромашка"}", Email="sapozhnikov@gmail.com", SubscriptionStatus="Не оформлена"},

            };
            DataGridCustomForClients.dgForClients.ItemsSource = people;
        }
        public class Person
        {
            public string Number { get; set; }
            public string ClientPhoto { get; set; }
            public string FullName { get; set; }
            public string CompanyName { get; set; }
            public string Email { get; set; }
            public string SubscriptionStatus { get; set; }

        }

        private void ButtomWithBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Windows.Others.ClientsLegalEntitiesAddWin win = new Windows.Others.ClientsLegalEntitiesAddWin();
            win.ShowDialog();
        }

        private void Page_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var thisWin = Application.Current.MainWindow as MainWindow;

            if (ComboboxesFilter.gridFilter.Visibility == Visibility.Visible && !ComboboxesFilter.gridFilter.IsMouseOver)
            {
                Dickplom1.Class.Animations.MinimazedReports(ComboboxesFilter.imageArrow, ComboboxesFilter.gridFilter);
            }
        }
    }
}
