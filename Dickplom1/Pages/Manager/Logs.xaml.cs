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
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dickplom1.Pages.Manager
{
    /// <summary>
    /// Логика взаимодействия для Logs.xaml
    /// </summary>
    public partial class Logs : Page
    {
        public Logs()
        {
            InitializeComponent();
        }

        //Загрузка данных в датагрид и паггинация
        public List<LogsViewModel> allLogs;
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
            allLogs = context.Logs
                .Where(o => o.UserID != null)
                .ToList()
                .Select(o => new LogsViewModel
                {
                    LogId = o.LogID,
                    UserId = o.UserID ?? 0,
                    FIO = o.Users != null && o.Users.UserData != null
                        ? o.Users.UserData.Surname + " " + o.Users.UserData.Name +
                          (string.IsNullOrWhiteSpace(o.Users.UserData.MiddleName) ? "" : " " + o.Users.UserData.MiddleName)
                        : "Неизвестный пользователь",
                    Action = o.Action,
                    Date = o.DateTime?.ToString("d"),
                    Description = o.Description,
                })
                .ToList();

            currentPage = 1;
            SetPaggination();
        }
        private void CheckTotalPages()
        {
            totalPages = (int)Math.Ceiling((double)allLogs.Count / 10);
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
            var itemsToShow = allLogs
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
            items.Add(new {  ActionValue = "Выберите действие" });
            items.Add(new {  ActionValue = "INSERT" });
            items.Add(new {  ActionValue = "UPDATE" });
            items.Add(new {  ActionValue = "DELETE" });


            ComboboxesFilter.firstCombobox.ItemsSource = items;
            ComboboxesFilter.firstCombobox.DisplayMemberPath = "ActionValue";
            ComboboxesFilter.firstCombobox.SelectedValuePath = "ActionValue";
            ComboboxesFilter.firstCombobox.SelectedIndex = 0;
            ComboboxesFilter.firstCombobox.SelectionChanged += FirstCombobox_SelectionChanged;
            ComboboxesFilter.gridFilter.Height = 150;
        }
        public string cboxActionValue { get; set; } = string.Empty;

        private void FirstCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboxActionValue = ComboboxesFilter.firstCombobox.SelectedValue.ToString();
            ApplyFilters();
        }

        public void ApplyFilters()
        {
            var context = DBEntities.GetContext();

            var clientsQuery = context.Logs
            .Where(c => c.LogID != null);

            // фильтр по действию
            if (cboxActionValue != string.Empty)
            {
                if (cboxActionValue == "Выберите действие")
                {
                    clientsQuery = clientsQuery;
                }
                else
                {
                    clientsQuery = clientsQuery.Where(c => c.Action == cboxActionValue);
                }
            }
            

            var filteredUsers = clientsQuery
                .Where(o => o.UserID != null)
                .ToList()
                .Select(o => new LogsViewModel
                {
                    LogId = o.LogID,
                    UserId = o.UserID ?? 0,
                    FIO = o.Users != null && o.Users.UserData != null
                        ? o.Users.UserData.Surname + " " + o.Users.UserData.Name +
                          (string.IsNullOrWhiteSpace(o.Users.UserData.MiddleName) ? "" : " " + o.Users.UserData.MiddleName)
                        : "Неизвестный пользователь",
                    Action = o.Action,
                    Date = o.DateTime?.ToString("d"),
                    Description = o.Description,
                })
                .ToList();

            if (allLogs != null)
                allLogs.Clear();

            allLogs = filteredUsers;

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