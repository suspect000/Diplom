using CustomControlsForDiplomFramework;
using Dickplom1.DataFolder;
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



        //Загрузка данных в датагрид и паггинация
        private List<OrdersViewModel> allOrders;
        private int currentPage = 1;
        private int itemsPerPage = 10;
        private int totalPages = 1;
        private void DataGridCustomForOrdersNaturalPersons_Loaded(object sender, RoutedEventArgs e)
        { 
                
            var context = DBEntities.GetContext();

                
            allOrders = context.Orders.Where(o=> o.ClientTypeId == 1)
                    .Select(o => new OrdersViewModel
                    {
                        SubscriptionName = o.Subscription.SubscriptionName,
                        FullNameClient = o.ClientsNaturalPersons.Surname 
                        + " " + o.ClientsNaturalPersons.Name 
                        + " " + o.ClientsNaturalPersons.MiddleName,
                        StartDate = o.StartDate.Value,
                        EndDate = o.EndDate.Value,
                        OrderStatus = o.OrderStatus.StatusValue,
                        FIOManager = o.Users.UserData.Surname + " " + o.Users.UserData.Name + " " + o.Users.UserData.MiddleName
                    })
                    .ToList();

                
            totalPages = (int)Math.Ceiling((double)allOrders.Count / 10);
            currentPage = 1;
                
            LoadCurrentPage();
            GeneratePaginationButtons();
        }
        private void GeneratePaginationButtons()
        {
            spPaggination.Children.Clear();

            void AddButton(int pageNumber, bool isCurrent = false)
            {
                var btn = new PagginationButtons();
                btn.rbtnPag.Content = pageNumber.ToString();
                btn.rbtnPag.Tag = pageNumber;
                btn.rbtnPag.IsChecked = isCurrent;
                btn.rbtnPag.Click += RbtnPag_Click;
                spPaggination.Children.Add(btn);
            }

            void AddEllipsis()
            {
                var textBlock = new TextBlock
                {
                    Text = "...",
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(5, 0, 5, 0)
                };
                spPaggination.Children.Add(textBlock);
            }

            const int maxVisibleButtons = 15;

            if (totalPages <= maxVisibleButtons)
            {
                // Показываем все страницы
                for (int i = 1; i <= totalPages; i++)
                {
                    AddButton(i, i == currentPage);
                }
            }
            else
            {
                AddButton(1, currentPage == 1);

                // Левая сторона
                if (currentPage > 4)
                    AddEllipsis();

                int start = Math.Max(2, currentPage - 2);
                int end = Math.Min(totalPages - 1, currentPage + 2);

                for (int i = start; i <= end; i++)
                {
                    AddButton(i, i == currentPage);
                }

                // Правая сторона
                if (currentPage < totalPages - 3)
                    AddEllipsis();

                AddButton(totalPages, currentPage == totalPages);
            }
        }

        private void RbtnPag_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rbtn && int.TryParse(rbtn.Tag.ToString(), out int page))
            {
                currentPage = page;
                LoadCurrentPage();
                GeneratePaginationButtons();
            }
        }
        private void LoadCurrentPage()
        {
            var itemsToShow = allOrders
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select((c, index) => {
                    c.Number = (currentPage - 1) * itemsPerPage + index + 1;
                    return c;
                })
                .ToList();

            if (itemsToShow.Count <= 0)
                tbInfo.Visibility = Visibility.Visible;
            else
                tbInfo.Visibility = Visibility.Collapsed;

            DataGridCustomForOrdersNaturalPersons.dg.ItemsSource = itemsToShow;
        }
    } 
}
