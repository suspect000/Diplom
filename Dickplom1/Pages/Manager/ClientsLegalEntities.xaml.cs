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
        }

        private void ButtomWithBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClientsLegalEntitiesAddWin win = new ClientsLegalEntitiesAddWin();
            win.Closed += Win_Closed;
            win.ShowDialog();
        }

        private void Win_Closed(object sender, EventArgs e)
        {
            RefreshItemsList();
            LoadCurrentPage();
        }

        private void RefreshItemsList()
        {
            var context = DBEntities.GetContext();

            allClients = context.ClientsLegalEntities
                .Where(c=>c.IsDeleted == false)
                .Select(c => new ClientViewModel
                {
                    ClientId = c.ClientsLegalEntitiesId,
                    ClientPhoto = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w=>w.IsActive == true && w.CompanyId == c.CompanyId).Photo,
                    FullName = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Surname 
                    + " " + c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Name 
                    + " " + c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Middlename,
                    CompanyName = c.ClientsLegalEntitiesCompanyData.CompanyName,
                    CreatorId = c.CreatorId,
                    Email = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Email,
                    SubscriptionStatus = context.Orders
                    .Where(o => o.ClientId == c.ClientsLegalEntitiesId)
                    .Select(o => o.OrderStatus.StatusValue)
                    .FirstOrDefault() ?? "Не оформлена"
                })
                .ToList();
        }
        private void Page_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var thisWin = Application.Current.MainWindow as MainWindow;

            if (ComboboxesFilter.gridFilter.Visibility == Visibility.Visible && !ComboboxesFilter.gridFilter.IsMouseOver)
            {
                Dickplom1.Class.Animations.MinimazedReports(ComboboxesFilter.imageArrow, ComboboxesFilter.gridFilter);
            }
        }



        private List<ClientViewModel> allClients;
        private int currentPage = 1;
        private int itemsPerPage = 10;
        private int totalPages = 1;

        private void DataGridCustomForClients_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            allClients = context.ClientsLegalEntities
                .Where(c => c.IsDeleted == false)
                .Select(c => new ClientViewModel
                {
                    ClientId = c.ClientsLegalEntitiesId,
                    ClientPhoto = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true).Photo,
                    FullName = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true).Surname
                    + " " + c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true).Name
                    + " " + c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true).Middlename,
                    CompanyName = c.ClientsLegalEntitiesCompanyData.CompanyName,
                    CreatorId = c.CreatorId,
                    Email = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true).Email,
                    SubscriptionStatus = context.Orders
                    .Where(o => o.ClientId == c.ClientsLegalEntitiesId)
                    .Select(o => o.OrderStatus.StatusValue)
                    .FirstOrDefault() ?? "Не оформлена"
                })
                .ToList();

            totalPages = (int)Math.Ceiling((double)allClients.Count / 10);
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
            var itemsToShow = allClients
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

            DataGridCustomForClients.dgForClients.ItemsSource = itemsToShow;
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
            cbox.cbox.Margin = new Thickness(15, 0, 15, 0);
            cbox.cbox.SelectionChanged += Cbox_SelectionChanged; ;

            ComboboxesFilter.spCboxes.Children.Add(cbox);
        }
        private void CheckTotalPages()
        {
            totalPages = (int)Math.Ceiling((double)allClients.Count / 10);
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
        private void ApplyFilters()
        {
            var context = DBEntities.GetContext();

            var clientsQuery = context.ClientsLegalEntities
            .Where(c => (bool)!c.IsDeleted);

            // фильтр по создателю записи
            if (comboboxCreatorValue != 0)
            {
                clientsQuery = clientsQuery.Where(c => c.CreatorId == comboboxCreatorValue);
            }

            // фильтр по статусу заказа
            if (comboboxStatusValue != 0)
            {
                clientsQuery = clientsQuery.Where(c => context.OrdersLegalEntities
                .Any(o => o.ClientId == c.ClientsLegalEntitiesId
                && !o.IsDeleted
                && o.StatusId == comboboxStatusValue
                ));
            }
            var filteredClients = clientsQuery
                .Where(c => c.IsDeleted == false)
                .Select(c => new ClientViewModel
                {
                    ClientId = c.ClientsLegalEntitiesId,
                    ClientPhoto = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true).Photo,
                    FullName = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true).Surname
                    + " " + c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true).Name
                    + " " + c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true).Middlename,
                    CompanyName = c.ClientsLegalEntitiesCompanyData.CompanyName,
                    CreatorId = c.CreatorId,
                    Email = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true).Email,
                    SubscriptionStatus = context.Orders
                    .Where(o => o.ClientId == c.ClientsLegalEntitiesId)
                    .Select(o => o.OrderStatus.StatusValue)
                    .FirstOrDefault() ?? "Не оформлена"
                })
                .ToList();

            allClients.Clear();
            allClients = filteredClients;

            CheckTotalPages();
            GeneratePaginationButtons();
            LoadCurrentPage();
        }

        private void miClient_Click(object sender, RoutedEventArgs e)
        {
            ClientsLegalEntitiesAddWin win = new ClientsLegalEntitiesAddWin();

            if (DataGridCustomForClients.dgForClients.SelectedItem is ClientViewModel item)
            {
                var client = DBEntities.GetContext().ClientsLegalEntities
                    .Where(c => c.ClientsLegalEntitiesId == item.ClientId).FirstOrDefault();

                win.ClientId = client.ClientsLegalEntitiesId; // передали ClientsLegalEntitiesId!!!!!!!
                win.Closed += Win_Closed;
                win.ShowDialog();
            }
        }

        private void miCreator_Click(object sender, RoutedEventArgs e)
        {
            StaffManagerMiniProfile win = new StaffManagerMiniProfile();

            if (DataGridCustomForClients.dgForClients.SelectedItem is ClientViewModel item)
            {
                var staff = DBEntities.GetContext().Users
                    .Where(u => u.UserDataId == item.CreatorId)
                    .FirstOrDefault();
                win.StaffId = staff.UserData.UserDataId;
                win.ShowDialog();
            }
        }

        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxButton btns = MessageBoxButton.YesNo;
            MessageBoxResult box = MessageBox.Show("Вы уверенны?", "Внимание", btns);

            if (box == MessageBoxResult.Yes)
                if (DataGridCustomForClients.dgForClients.SelectedItem is ClientViewModel item)
                {
                    DBEntities.GetContext().ClientsLegalEntities.FirstOrDefault(c => c.ClientsLegalEntitiesId == item.ClientId).IsDeleted = true;
                    DBEntities.GetContext().SaveChanges();
                    RefreshItemsList();
                    LoadCurrentPage();
                }
        }
    }
}