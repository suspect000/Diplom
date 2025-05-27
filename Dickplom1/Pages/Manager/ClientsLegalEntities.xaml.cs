using CustomControlsForDiplomFramework;
using Dickplom1.DataFolder;
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



        private List<ClientViewModel> allClients;
        private int currentPage = 1;
        private int itemsPerPage = 10;
        private int totalPages = 1;

        private void DataGridCustomForClients_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            allClients = context.ClientsLegalEntities
                .Select(c => new ClientViewModel
                {
                    ClientId = c.ClientsLegalEntitiesId,
                    ClientPhoto = c.ClientsLegalEntitiesContactPerson.Photo,
                    FullName = c.ClientsLegalEntitiesContactPerson.Surname + " " + c.ClientsLegalEntitiesContactPerson.Name + " " + c.ClientsLegalEntitiesContactPerson.Middlename,
                    CompanyName = c.ClientsLegalEntitiesCompanyData.CompanyName,
                    Email = c.ClientsLegalEntitiesContactPerson.Email,
                    SubscriptionStatus = context.Orders
                    .Where(o => o.ClientId == c.ClientsLegalEntitiesId && o.ClientTypeId == 2)
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
    }
}
