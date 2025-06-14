using CustomControlsForDiplomFramework;
using Dickplom1.DataFolder;
using Dickplom1.Windows.Others;
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
            win.Closed += Win_Closed;
            win.ShowDialog();
        }

        private void Win_Closed(object sender, EventArgs e)
        {
            RefreshItems();
            GeneratePaginationButtons();

        }

        private void RefreshItems()
        {
            allOrders = DBEntities.GetContext().Orders
               .Where(c => c.IsDeleted == false)
               .ToList()
               .Select(o => new OrdersViewModel
               {
                   OrderId = o.OrderId,
                   SubscriptionName = o.Subscription.SubscriptionName,
                   FullNameClient = o.ClientsNaturalPersons.Surname
                   + " " + o.ClientsNaturalPersons.Name
                   + " " + o.ClientsNaturalPersons.MiddleName,
                   StartDate = o.StartDate.Value.ToString("d"),
                   EndDate = o.EndDate.Value.ToString("d"),
                   OrderStatus = o.OrderStatus.StatusValue,
                   FIOManager = o.Users?.UserData.Surname + " " + o.Users?.UserData.Name + " " + o.Users?.UserData.MiddleName,
                   CreatorId = o.CreatorId?? 0
               })
               .ToList();

            LoadCurrentPage();
        }



        //Загрузка данных в датагрид и паггинация
        private List<OrdersViewModel> allOrders;
        private int currentPage = 1;
        private int itemsPerPage = 10;
        private int totalPages = 1;
        private void DataGridCustomForOrdersNaturalPersons_Loaded(object sender, RoutedEventArgs e)
        { 
                
            var context = DBEntities.GetContext();
          
            allOrders = context.Orders
                .Where(c=>c.IsDeleted == false)  
                .ToList()
                .Select(o => new OrdersViewModel  
                { 
                    OrderId = o.OrderId,
                    SubscriptionName = o.Subscription.SubscriptionName,
                    FullNameClient = o.ClientsNaturalPersons.Surname + " " + o.ClientsNaturalPersons.Name + " " + o.ClientsNaturalPersons.MiddleName,
                    StartDate = o.StartDate.Value.ToString("d"),
                    EndDate = o.EndDate.Value.ToString("d"),
                    OrderStatus = o.OrderStatus.StatusValue,
                    FIOManager = o.Users ?.UserData.Surname + " " + o.Users ?.UserData.Name + " " + o.Users ?.UserData.MiddleName,
                    CreatorId = o.CreatorId?? 0
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

        private void ComboboxesFilter_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            var items = new List<object>();

            // Заглушка — объект с FullName и UserDataId = 0 или null
            items.Add(new { UserDataId = 0, FullName = "Создатель записи" });

            items.AddRange(context.UserData
                .Select(u => new
                {
                    u.UserDataId,
                    FullName = u.Surname + " " + u.Name + " " + u.MiddleName
                }));

            ComboboxesFilter.firstCombobox.ItemsSource = items;
            ComboboxesFilter.firstCombobox.DisplayMemberPath = "FullName";
            ComboboxesFilter.firstCombobox.SelectedValuePath = "UserDataId";
            ComboboxesFilter.firstCombobox.SelectedIndex = 0;
            ComboboxesFilter.firstCombobox.SelectionChanged += FirstCombobox_SelectionChanged; ;


            //Добавить 2-ой комбобокс
            ComboboxMaterialDesignWithBorder cbox = new ComboboxMaterialDesignWithBorder();

            var items2 = new List<object>();
            items2.Add(new { StatusId = 0, StatusValue = "Статус подписки" });

            items2.AddRange(context.OrderStatus
                .Select(u => new
                {
                    u.StatusId,
                    u.StatusValue,
                }));


            cbox.cbox.ItemsSource = items2;
            cbox.cbox.DisplayMemberPath = "StatusValue";
            cbox.cbox.SelectedValuePath = "StatusId";
            cbox.cbox.SelectedIndex = 0;
            cbox.cbox.SelectionChanged += Cbox_SelectionChanged;
            cbox.Margin = new Thickness(15,0,15,0);

            ComboboxesFilter.spCboxes.Children.Add(cbox);
        }
        public int comboboxCreatorValue { get; set; } = 0;
        public int comboboxStatusValue { get; set; } = 0;

        private void FirstCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboboxCreatorValue = Convert.ToInt32(ComboboxesFilter.firstCombobox.SelectedValue);
            ApplyFilters();
        }
        private void Cbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.ComboBox cbox)
            {
                comboboxStatusValue = Convert.ToInt32(cbox.SelectedValue);
                ApplyFilters();
            }
        }
        private void CheckTotalPages()
        {
            totalPages = (int)Math.Ceiling((double)allOrders.Count / 10);
        }
        private void ApplyFilters()
        {
            var context = DBEntities.GetContext();

            var clientsQuery = context.Orders
            .Where(c => !c.IsDeleted);

            // фильтр по создателю записи
            if (comboboxCreatorValue != 0)
            {
                clientsQuery = clientsQuery.Where(c => c.CreatorId == comboboxCreatorValue);
            }

            // фильтр по статусу заказа
            if (comboboxStatusValue != 0)
            {
                clientsQuery = clientsQuery.Where(c=>c.StatusId == comboboxStatusValue);
            }

            var filteredClients = clientsQuery
                .ToList() // Загружаем данные в память
                .Select(c => new OrdersViewModel
                {
                    OrderId = c.OrderId,
                    SubcriptionId = (int)c.SubscriptionId,
                    SubscriptionName = c.Subscription.SubscriptionName,
                    FullNameClient = c.ClientsNaturalPersons.Surname + " " + c.ClientsNaturalPersons.Name + " " + c.ClientsNaturalPersons.MiddleName,
                    StartDate = c.StartDate?.ToString("g"),
                    EndDate = c.EndDate?.ToString("g"),
                    OrderStatusId = c.OrderStatus.StatusId,
                    OrderStatus = c.OrderStatus.StatusValue,
                    CreatorId = c.CreatorId ?? 0,
                    FIOManager = c.Users?.UserData.Surname + " " + c.Users?.UserData.Name + " " + c.Users?.UserData.MiddleName
                })
                .ToList();

            allOrders.Clear();
            allOrders = filteredClients;

            CheckTotalPages();
            GeneratePaginationButtons();
            LoadCurrentPage();
        }

        private void miClient_Click(object sender, RoutedEventArgs e)
        {
            OrdersNaturalPersonsAddWin win = new OrdersNaturalPersonsAddWin();

            if (DataGridCustomForOrdersNaturalPersons.dg.SelectedItem is OrdersViewModel item)
            {
                if (item != null)
                {
                    var order = DBEntities.GetContext().Orders
                    .Where(c => c.OrderId == item.OrderId).FirstOrDefault();

                    if (item.OrderId != 0)
                        win.OrderId = order.OrderId;

                    win.Closed += Win_Closed;
                    win.ShowDialog();
                }
            }
        }
        private void miCreator_Click(object sender, RoutedEventArgs e)
        {
            StaffManagerMiniProfile win = new StaffManagerMiniProfile();

            if (DataGridCustomForOrdersNaturalPersons.dg.SelectedItem is OrdersViewModel item)
            {
                if (item != null && item.CreatorId != null)
                {
                    var staff = DBEntities.GetContext().Users
                        .FirstOrDefault(u => u.UserDataId == item.CreatorId);
                    if (staff != null)
                    {
                        win.StaffId = staff.UserData.UserDataId;
                        win.ShowDialog();
                    }
                }
            }
        }

        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridCustomForOrdersNaturalPersons.dg.SelectedItem is OrdersViewModel item)
            {
                try
                {
                    if (item.OrderId != 0)
                    {
                        MessageBoxButton btns = MessageBoxButton.YesNo;
                        MessageBoxResult box = MessageBox.Show("Вы уверенны?", "Внимание", btns);

                        if (box == MessageBoxResult.Yes)
                        {
                            var context = DBEntities.GetContext();
                            context.Orders.FirstOrDefault(f => f.OrderId == item.OrderId).IsDeleted = true;
                            context.SaveChanges();
                            RefreshItems();
                            GeneratePaginationButtons();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    } 
}
