using CustomControlsForDiplomFramework;
using Dickplom1.Class;
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

        public bool IsDeletedFilter { get; set; } = false;

        private void ButtomWithBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClientsLegalEntitiesAddWin win = new ClientsLegalEntitiesAddWin();
            win.Closed += Win_Closed;
            win.ShowDialog();
        }

        private void Win_Closed(object sender, EventArgs e)
        {
            RefreshItemsList();
            SetPaggination();
        }
        public void SetPaggination()
        {
            CheckTotalPages();
            LoadCurrentPage();
            GeneratePaginationButtons();
        }

        public void RefreshItemsList()
        {
            var context = DBEntities.GetContext();

            if (IsDeletedFilter)
            {
                allClientsLegal = context.ClientsLegalEntities
                .Where(c => c.IsDeleted == true)
                .Select(c => new ClientViewModel
                {
                    ClientId = c.ClientsLegalEntitiesId,
                    ClientPhoto = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Photo,
                    FullName = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Surname
                    + " " + c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Name
                    + " " + c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Middlename,
                    CompanyName = c.ClientsLegalEntitiesCompanyData.CompanyName,
                    CreatorId = c.CreatorId,
                    Email = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Email,
                })
                .ToList();
            }
            else
            {
                allClientsLegal = context.ClientsLegalEntities     
                    .Where(c => c.IsDeleted == false)      
                    .Select(c => new ClientViewModel      
                    {           
                        ClientId = c.ClientsLegalEntitiesId,
                        ClientPhoto = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Photo,
                        FullName = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Surname
                        + " " + c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Name
                        + " " + c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Middlename,
                        CompanyName = c.ClientsLegalEntitiesCompanyData.CompanyName,
                        CreatorId = c.CreatorId,
                        Email = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Email,
                    })
                    .ToList();
            }
            SetPaggination();
        }
        private void Page_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var thisWin = Application.Current.MainWindow as MainWindow;

            if (ComboboxesFilter.gridFilter.Visibility == Visibility.Visible && !ComboboxesFilter.gridFilter.IsMouseOver)
            {
                Dickplom1.Class.Animations.MinimazedReports(ComboboxesFilter.imageArrow, ComboboxesFilter.gridFilter);
            }
        }



        public List<ClientViewModel> allClientsLegal;
        private int currentPage = 1;
        private int itemsPerPage = 10;
        private int totalPages = 1;

        private void DataGridCustomForClients_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            allClientsLegal = context.ClientsLegalEntities
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
                })
                .ToList();
            /*totalPages = (int)Math.Ceiling((double)allClientsLegal.Count / 10);*/

            currentPage = 1;
            CheckTotalPages();

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
            var itemsToShow = allClientsLegal
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
            ComboboxesFilter.firstCombobox.SelectionChanged += FirstCombobox_SelectionChanged;
        }
        private void CheckTotalPages()
        {
            totalPages = (int)Math.Ceiling((double)allClientsLegal.Count / 10);
        }

        public int comboboxCreatorValue { get; set; } = 0;
        public int comboboxStatusValueId { get; set; } = 0;
        public string comboboxStatusValue { get; set; } = "";


        private void FirstCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboboxCreatorValue = Convert.ToInt32(ComboboxesFilter.firstCombobox.SelectedValue);
            ApplyFilters();
        }
        public void ApplyFilters()
        {
            var context = DBEntities.GetContext();

            // Фильтраиция по не удаленным записям
            if (IsDeletedFilter == false)
            {
                var clientsQuery = context.ClientsLegalEntities
                    .Where(c => (bool)!c.IsDeleted);

                // фильтр по создателю записи
                if (comboboxCreatorValue != 0)
                {
                    clientsQuery = clientsQuery.Where(c => c.CreatorId == comboboxCreatorValue);
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
                    })
                    .ToList();

                allClientsLegal.Clear();
                allClientsLegal = filteredClients;
            }
            else // Фильтраиция по удаленным записям
            {
                var clientsQuery = context.ClientsLegalEntities
                    .Where(c => (bool)c.IsDeleted);

                // фильтр по создателю записи
                if (comboboxCreatorValue != 0)
                {
                    clientsQuery = clientsQuery.Where(c => c.CreatorId == comboboxCreatorValue);
                }

                var filteredClients = clientsQuery
                    .Where(c => c.IsDeleted == true)
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
                    })
                    .ToList();

                allClientsLegal.Clear();
                allClientsLegal = filteredClients;
            }

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
                    .FirstOrDefault(u => u.UserDataId == item.CreatorId);
                if (staff != null)
                {
                    win.StaffId = staff.UserData.UserDataId;
                    win.ShowDialog();
                }
                else
                    MessageBox.Show("Создатель не найден");

            }
        }

        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();
            MessageBoxButton btns = MessageBoxButton.YesNo;
            MessageBoxResult box = MessageBox.Show("Вы уверенны?", "Внимание", btns);

            if (box == MessageBoxResult.Yes)
                if (DataGridCustomForClients.dgForClients.SelectedItem is ClientViewModel item)
                {
                    if (IsDeletedFilter)
                    {
                        var selectedClientLegal = context.ClientsLegalEntities.FirstOrDefault(c => c.ClientsLegalEntitiesId == item.ClientId);
                        var selectedBankData = context.ClientsLegalEntitiesBankData.FirstOrDefault(c => c.BankDataId == selectedClientLegal.BankDataId);
                        var selectedCompanydata = context.ClientsLegalEntitiesCompanyData.FirstOrDefault(c=>c.CompanyId == selectedClientLegal.CompanyId);
                        var selectedContactPerson = context.ClientsLegalEntitiesContactPerson.Where(c=>c.CompanyId == selectedCompanydata.CompanyId).ToList();
                        var selectedOrder = context.OrdersLegalEntities.Where(f=>f.ClientId == item.ClientId).ToList();

                        if (selectedContactPerson != null)
                            context.ClientsLegalEntitiesContactPerson.RemoveRange(selectedContactPerson);

                        if (selectedCompanydata != null)
                            context.ClientsLegalEntitiesCompanyData.Remove(selectedCompanydata);

                        if (selectedBankData != null)
                            context.ClientsLegalEntitiesBankData.Remove(selectedBankData);

                        if (selectedOrder != null)
                            context.OrdersLegalEntities.RemoveRange(selectedOrder);

                        context.ClientsLegalEntities.Remove(selectedClientLegal);
                    }
                    else
                    {
                        var selectedOrder = context.OrdersLegalEntities.FirstOrDefault(f => f.ClientId == item.ClientId && f.StatusId > 1 & f.StatusId < 6 && f.IsDeleted == false);
                        if (selectedOrder != null)
                        {
                            MessageBoxResult boxNew = MessageBox.Show($"У данного клиента есть активный заказ, который тоже сместится в корзину \nвы точно уверенны?", "Внимание", btns);
                            if (boxNew == MessageBoxResult.Yes)
                            {
                                context.ClientsLegalEntities.FirstOrDefault(c => c.ClientsLegalEntitiesId == item.ClientId).IsDeleted = true;
                                context.OrdersLegalEntities.FirstOrDefault(c => c.ClientId == item.ClientId && c.StatusId > 1 & c.StatusId < 6).IsDeleted = true;
                            }
                            else
                                return;
                        }
                        else
                            context.ClientsLegalEntities.FirstOrDefault(f => f.ClientsLegalEntitiesId == item.ClientId).IsDeleted = true;
                    }
                    context.SaveChanges();
                    RefreshItemsList();
                    LoadCurrentPage();
                }
        }

        private void DeletedRecords_Loaded(object sender, RoutedEventArgs e)
        {
            spDeletedRecords.stackPanel.MouseLeftButtonUp += StackPanel_MouseLeftButtonUp;
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsDeletedFilter = true; //активируем флажок

            Dickplom1.Class.Musor.ShowElement(spBack); // Включаем кнопку вернуть
            Dickplom1.Class.Musor.HideElement(spDeletedRecords); // Выключаем кнопку удаленных записей

            try
            {
                MenuItem miBtn = DataGridCustomForClients.ContextMenu?.Items
                    .OfType<MenuItem>()
                    .FirstOrDefault(mi => (string)mi.Header == "Восстановить");
                if (miBtn != null)
                    miBtn.Visibility = Visibility.Visible;

                var context = DBEntities.GetContext();

                allClientsLegal = context.ClientsLegalEntities
                    .Where(w => w.IsDeleted == true)
                    .Select(c => new ClientViewModel
                    {
                        ClientId = c.ClientsLegalEntitiesId,
                        ClientPhoto = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Photo,
                        FullName = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Surname
                        + " " + c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Name
                        + " " + c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Middlename,
                        CompanyName = c.ClientsLegalEntitiesCompanyData.CompanyName,
                        CreatorId = c.CreatorId,
                        Email = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Email
                    })
                    .ToList();

                totalPages = (int)Math.Ceiling((double)allClientsLegal.Count / 10);
                currentPage = 1;

                LoadCurrentPage();
                GeneratePaginationButtons();
            }
            catch (Exception)
            {
            }
        }

        private void dgBtnRecovery_Click(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            try
            {
                if (DataGridCustomForClients.dgForClients.SelectedItem is ClientViewModel item) 
                {
                    if (item != null)
                    {
                        var selectedOrder = context.OrdersLegalEntities.FirstOrDefault(f=>f.ClientId == item.ClientId && f.IsDeleted == true && f.StatusId > 1 & f.StatusId < 6);
                        if (selectedOrder != null)
                        {
                            selectedOrder.IsDeleted = false;
                            context.ClientsLegalEntities.FirstOrDefault(f => f.ClientsLegalEntitiesId == item.ClientId).IsDeleted = false;
                        }
                        else
                            context.ClientsLegalEntities.FirstOrDefault(f => f.ClientsLegalEntitiesId == item.ClientId).IsDeleted = false;
                    }
                    
                    context.SaveChanges();
                    RefreshItemsList();
                    LoadCurrentPage();
                }
            }
            catch (Exception)
            {
            }
        }

        private void spBack_MouseEnter(object sender, MouseEventArgs e)
        {
            Dickplom1.Class.Animations.OpacityAnimation(spBack, spBack.Opacity, 0.7, 0.3);
        }

        private void spBack_MouseLeave(object sender, MouseEventArgs e)
        {
            Dickplom1.Class.Animations.OpacityAnimation(spBack, spBack.Opacity, 1, 0.3);
        }

        private void spBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsDeletedFilter = false; //активируем флажок

            Dickplom1.Class.Musor.ShowElement(spDeletedRecords); // Включаем кнопку вернуть
            Dickplom1.Class.Musor.HideElement(spBack); // Выключаем кнопку удаленных записей

            try
            {
                MenuItem miBtn = DataGridCustomForClients.ContextMenu?.Items
                    .OfType<MenuItem>()
                    .FirstOrDefault(mi => (string)mi.Header == "Восстановить");
                if (miBtn != null)
                    miBtn.Visibility = Visibility.Collapsed;

                var context = DBEntities.GetContext();

                allClientsLegal = context.ClientsLegalEntities
                    .Where(w => w.IsDeleted == false)
                    .Select(c => new ClientViewModel
                    {
                        ClientId = c.ClientsLegalEntitiesId,
                        ClientPhoto = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Photo,
                        FullName = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Surname
                        + " " + c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Name
                        + " " + c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Middlename,
                        CompanyName = c.ClientsLegalEntitiesCompanyData.CompanyName,
                        CreatorId = c.CreatorId,
                        Email = c.ClientsLegalEntitiesCompanyData.ClientsLegalEntitiesContactPerson.FirstOrDefault(w => w.IsActive == true && w.CompanyId == c.CompanyId).Email,
                    })
                    .ToList();

                totalPages = (int)Math.Ceiling((double)allClientsLegal.Count / 10);
                currentPage = 1;

                LoadCurrentPage();
                GeneratePaginationButtons();
            }
            catch (Exception)
            {
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null && mainWindow.gridSearch != null)
            {
                mainWindow.gridSearch.Visibility = Visibility.Visible;
            }
            Dickplom1.Class.Musor.SearchSelect();
        }
    }
}