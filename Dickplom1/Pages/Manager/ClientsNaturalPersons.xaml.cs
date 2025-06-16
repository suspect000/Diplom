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
using Dickplom1.DataFolder;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity;
using CustomControlsForDiplomFramework;
using static MaterialDesignThemes.Wpf.Theme;
using Dickplom1.Windows.Others;
using System.IO;

namespace Dickplom1.Pages.Manager
{
    /// <summary>
    /// Логика взаимодействия для ClientsNaturalPersons.xaml
    /// </summary>
    public partial class ClientsNaturalPersons : Page
    {
        public ClientsNaturalPersons()
        {
            InitializeComponent();
        }
        public bool IsDeletedFilter { get; set; } = false;
        private void ButtomWithBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Windows.Others.ClientsNaturalPersonAddWin win = new Windows.Others.ClientsNaturalPersonAddWin();
            win.Closed += Win_Closed1;
            win.ShowDialog();
        }

        private void Win_Closed1(object sender, EventArgs e)
        {
            LoadCurrentPage();
            GeneratePaginationButtons();
        }

        private void Page_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var thisWin = Application.Current.MainWindow as MainWindow;

            if (ComboboxesFilter.gridFilter.Visibility == Visibility.Visible && !ComboboxesFilter.gridFilter.IsMouseOver)
            {
                Dickplom1.Class.Animations.MinimazedReports(ComboboxesFilter.imageArrow, ComboboxesFilter.gridFilter);
            }
        }
        // Преобразование фотографии из byte[] -> ImageSource
        public static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;

            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        //Загрузка данных в датагрид и паггинация
        private List<ClientViewModel> allClients;
        private int currentPage = 1;
        private int itemsPerPage = 10;
        private int totalPages = 1;


        private void CheckTotalPages()
        {
            totalPages = (int)Math.Ceiling((double)allClients.Count / 10);
        }
        private void DataGridCustomForClients_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            RefreshItemsList();

            CheckTotalPages();
            currentPage = 1;

            LoadCurrentPage();
            GeneratePaginationButtons();
        }

        private void RefreshItemsList()
        {
            var context = DBEntities.GetContext();

            allClients = context.ClientsNaturalPersons
                .Where(c => c.IsDeleted == false)
                .Select(c => new ClientViewModel
                {
                    ClientId = c.ClientNaturalPersonsId,
                    ClientPhoto = c.ClientPhoto,
                    FullName = c.Surname + " " + c.Name + " " + c.MiddleName,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                })
                .ToList();
        }
        private void GeneratePaginationButtons()
        {
            sPanelPaggination.Children.Clear();

            void AddButton(int pageNumber, bool isCurrent = false)
            {
                var btn = new PagginationButtons();
                btn.rbtnPag.Content = pageNumber.ToString();
                btn.rbtnPag.Tag = pageNumber;
                btn.rbtnPag.IsChecked = isCurrent;
                btn.rbtnPag.Click += RbtnPag_Click;
                sPanelPaggination.Children.Add(btn);
            }

            void AddEllipsis()
            {
                var textBlock = new TextBlock
                {
                    Text = "...",
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(5, 0, 5, 0)
                };
                sPanelPaggination.Children.Add(textBlock);
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
            if (sender is System.Windows.Controls.RadioButton rbtn && int.TryParse(rbtn.Tag.ToString(), out int page))
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


        private void miClient_Click(object sender, RoutedEventArgs e)
        {
            ClientsNaturalPersonAddWin win = new ClientsNaturalPersonAddWin();

            if (DataGridCustomForClients.dgForClients.SelectedItem is ClientViewModel item)
            {
                var client = DBEntities.GetContext().ClientsNaturalPersons.Where(c => c.ClientNaturalPersonsId == item.ClientId).FirstOrDefault();

                win.ClientId = client.ClientNaturalPersonsId;
                win.Closed += Win_Closed;
                win.ShowDialog();
            }
        }

        private void Win_Closed(object sender, EventArgs e)
        {
            RefreshItemsList();
            LoadCurrentPage();
        }


        private void miCreator_Click(object sender, RoutedEventArgs e)
        {
            StaffManagerMiniProfile win = new StaffManagerMiniProfile();

            if (DataGridCustomForClients.dgForClients.SelectedItem is ClientViewModel item)
            {
                var staff = DBEntities.GetContext().Users
                    .Where(u=>u.UserDataId == item.CreatorId)
                    .FirstOrDefault();
                if (staff == null)
                {
                    MessageBox.Show("Создатель не найден");
                    return;
                }
                win.StaffId = staff.UserData.UserDataId;
                win.ShowDialog();
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
                        var selectedClient = context.ClientsNaturalPersons.FirstOrDefault(c => c.ClientNaturalPersonsId == item.ClientId);
                        var selectedOrder = context.Orders.FirstOrDefault(f => f.ClientId == item.ClientId);
                        if (selectedOrder != null)
                            context.Orders.Remove(selectedOrder);
                        context.ClientsNaturalPersons.Remove(selectedClient);
                        context.SaveChanges();
                    }
                    else
                    {
                        var selectedRecord = context.ClientsNaturalPersons.FirstOrDefault(c => c.ClientNaturalPersonsId == item.ClientId).IsDeleted = true;
                        context.SaveChanges();
                    }

                    RefreshItemsList();
                    LoadCurrentPage();
                }
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
        public int comboboxCreatorValue {  get; set; } = 0;
        public int comboboxStatusValue { get; set; } = 0;

        private void FirstCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboboxCreatorValue = Convert.ToInt32(ComboboxesFilter.firstCombobox.SelectedValue);
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var context = DBEntities.GetContext();

            if (IsDeletedFilter == false)
            {
                var clientsQuery = context.ClientsNaturalPersons
                           .Where(c => !c.IsDeleted);

                // фильтр по создателю записи
                if (comboboxCreatorValue != 0)
                {
                    clientsQuery = clientsQuery.Where(c => c.CreatorId == comboboxCreatorValue);
                }

                var filteredClients = clientsQuery
                    .Select(c => new ClientViewModel
                    {
                        ClientId = c.ClientNaturalPersonsId,
                        FullName = c.Surname + " " + c.Name + " " + c.MiddleName,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber,
                        ClientPhoto = c.ClientPhoto,
                    })
                    .ToList();

                allClients.Clear();
                allClients = filteredClients;
            }
            else // Фильтрация по удаленным записям
            {
                var clientsQuery = context.ClientsNaturalPersons
                           .Where(c => c.IsDeleted);

                // фильтр по создателю записи
                if (comboboxCreatorValue != 0)
                {
                    clientsQuery = clientsQuery.Where(c => c.CreatorId == comboboxCreatorValue);
                }
                var filteredClients = clientsQuery
                    .Select(c => new ClientViewModel
                    {
                        ClientId = c.ClientNaturalPersonsId,
                        FullName = c.Surname + " " + c.Name + " " + c.MiddleName,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber,
                        ClientPhoto = c.ClientPhoto,
                    })
                    .ToList();

                allClients.Clear();
                allClients = filteredClients;
            }


            CheckTotalPages();
            GeneratePaginationButtons();
            LoadCurrentPage();

        }

        private void DeletedRecords_Loaded(object sender, RoutedEventArgs e)
        {
            spDeletedRecords.stackPanel.MouseLeftButtonUp += StackPanel_MouseLeftButtonUp; ;
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

                allClients = context.ClientsNaturalPersons
                .Where(c => c.IsDeleted == true)
                .Select(c => new ClientViewModel
                {
                    ClientId = c.ClientNaturalPersonsId,
                    ClientPhoto = c.ClientPhoto,
                    FullName = c.Surname + " " + c.Name + " " + c.MiddleName,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                })
                .ToList();

                totalPages = (int)Math.Ceiling((double)allClients.Count / 10);
                currentPage = 1;

                LoadCurrentPage();
                GeneratePaginationButtons();
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

                RefreshItemsList();

                totalPages = (int)Math.Ceiling((double)allClients.Count / 10);
                currentPage = 1;

                LoadCurrentPage();
                GeneratePaginationButtons();
            }
            catch (Exception)
            {
            }
        }
    }
}
