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
    /// Логика взаимодействия для Staff.xaml
    /// </summary>
    public partial class Staff : Page
    {
        public Staff()
        {
            InitializeComponent();
        }

        //Загрузка данных в датагрид и паггинация
        public List<StaffViewModel> allStaff;
        private int currentPage = 1;
        private int itemsPerPage = 10;
        private int totalPages = 1;

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshItems();
        }

        public void SetPaggination()
        {
            CheckTotalPages();
            LoadCurrentPage();
            GeneratePaginationButtons();
        }

        public void RefreshItems()
        {
            var context = DBEntities.GetContext();
            allStaff = context.UserData
                .Select
                (o => new StaffViewModel 
                {
                    UserPhoto = o.UserPhoto,
                    FIOStaff = o.Surname + " " + o.Name + " " + o.MiddleName + " ",
                    Email = o.Email,
                    PhoneNumber = o.PhoneNumber ?? " ",
                    Role = o.Users.FirstOrDefault(u => u.UserDataId == o.UserDataId).Roles.NameRole ?? " ",
                    AccountStatus = o.Users.FirstOrDefault(u => u.UserDataId == o.UserDataId).AccountStatus.AccountStatusValue ?? " ",
                    IsDeleted = context.Users.FirstOrDefault(f=>f.UserDataId == o.UserDataId && f.IsDeleted == false).IsDeleted 
                })
                .ToList();

            currentPage = 1;
            SetPaggination();
        }
        private void CheckTotalPages()
        {
            totalPages = (int)Math.Ceiling((double)allStaff.Count / 10);
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
            var itemsToShow = allStaff
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

            dataGrid.dg.ItemsSource = itemsToShow;
        }

        private void ComboboxesFilter_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            var items = new List<object>();

            // Заглушка — объект с FullName и UserDataId = 0 или null
            items.Add(new { AccountStatusId = 0, AccountStatusValue = "Статус учетной записи" });

            items.AddRange(context.AccountStatus
                .Select(u => new
                {
                    AccountStatusId = u.AccountStatusId,
                    AccountStatusValue = u.AccountStatusValue
                }));

            ComboboxesFilter.firstCombobox.ItemsSource = items;
            ComboboxesFilter.firstCombobox.DisplayMemberPath = "AccountStatusValue";
            ComboboxesFilter.firstCombobox.SelectedValuePath = "AccountStatusId";
            ComboboxesFilter.firstCombobox.SelectedIndex = 0;
            ComboboxesFilter.firstCombobox.SelectionChanged += FirstCombobox_SelectionChanged;
            ComboboxesFilter.gridFilter.Height = 150;


            //Добавить 2-ой комбобокс
            ComboboxMaterialDesignWithBorder cbox = new ComboboxMaterialDesignWithBorder();

            var items2 = new List<object>();
            items2.Add(new { RoleId = 0, NameRole = "Должность" });

            items2.AddRange(context.Roles
                .Select(u => new
                {
                    RoleId = u.RoleId,
                    NameRole = u.NameRole,
                }));

            cbox.cbox.ItemsSource = items2;
            cbox.cbox.DisplayMemberPath = "NameRole";
            cbox.cbox.SelectedValuePath = "RoleId";
            cbox.cbox.SelectedIndex = 0;
            cbox.cbox.SelectionChanged += Cbox_SelectionChanged;
            cbox.Margin = new Thickness(15, 0, 15, 0);

            ComboboxesFilter.spCboxes.Children.Add(cbox);
        }
        public int cboxAccountStatusId { get; set; } = 0;
        public int cboxRoleId { get; set; } = 0;

        private void FirstCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboxAccountStatusId = Convert.ToInt32(ComboboxesFilter.firstCombobox.SelectedValue);
            ApplyFilters();
        }
        private void Cbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.ComboBox cbox)
            {
                cboxRoleId = Convert.ToInt32(cbox.SelectedValue);
                ApplyFilters();
            }
        }

        public void ApplyFilters()
        {
            var context = DBEntities.GetContext();

            var clientsQuery = context.Users
            .Where(c => !c.IsDeleted);

            // фильтр по статусу учетной записи
            if (cboxAccountStatusId != 0)
            {
                clientsQuery = clientsQuery.Where(c => c.AccountStatusId == cboxAccountStatusId);
            }

            // фильтр по должности
            if (cboxRoleId != 0)
            {
                clientsQuery = clientsQuery.Where(c => c.RoleId == cboxRoleId);
            }

            var filteredUsers = clientsQuery
                .Where(x => !x.IsDeleted)
                .ToList() // Загружаем данные в память
                .Select(c => new StaffViewModel
                {
                    UserId = c.UserId,
                    AccountStatusId = (int)c.AccountStatusId,
                    UserDataId = (int)c.UserDataId,
                    UserPasswordDataId = (int)c.UserPassportDataId,
                    RoleId = (int)c.RoleId,
                    Login = c.Login,
                    Password = c.Password,
                    CreatorId = c.CreatorId ?? 1,
                    CreatedAt = (DateTime)c.CreatedAt,
                    IsDeleted = c.IsDeleted,

                    FIOStaff = c.UserData?.Surname + " " + c.UserData?.Name + " " + c.UserData?.MiddleName + " ",
                    Email = c.UserData?.Email,
                    PhoneNumber = c.UserData?.PhoneNumber,
                    Role = c.Roles.NameRole,
                    AccountStatus = c.AccountStatus.AccountStatusValue
                })
                .ToList();

            if (allStaff != null)
                allStaff.Clear();

            allStaff = filteredUsers;

            CheckTotalPages();
            GeneratePaginationButtons();
            LoadCurrentPage();
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