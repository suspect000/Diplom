using LiveChartsCore;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LiveChartsCore.SkiaSharpView;
using Dickplom1.Class;
using System.Windows.Media;
using Dickplom1.DataFolder;
using System.Linq;
using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Runtime.Remoting.Contexts;
using System.IO;
using System.Windows.Media.Imaging;

namespace Dickplom1.Pages.Manager
{
    /// <summary>
    /// Логика взаимодействия для Dashboards.xaml
    /// </summary>
    public partial class Dashboards : Page
    {
        public Dashboards()
        {
            InitializeComponent();

            List<Staff> staff = new List<Staff>
            {
                new Staff {FIO = "Сапожников В. И.", Post = "Менеджер", Salles = "15 сделок", KPI="100% KPI"},
                new Staff {FIO = "Сапожников В. И.", Post = "Мл. Менеджер", Salles = "10 сделок", KPI="80% KPI"},
                new Staff {FIO = "Сапожников В. И.", Post = "Менеджер", Salles = "5 сделок", KPI="60% KPI"},
                new Staff {FIO = "Сапожников В. И.", Post = "Менеджер", Salles = "3 сделок", KPI="40% KPI"},
                new Staff {FIO = "Сапожников В. И.", Post = "Менеджер", Salles = "1 сделок", KPI="20% KPI"},
                new Staff {FIO = "Сапожников В. И.", Post = "Менеджер", Salles = "1 сделок", KPI="20% KPI"},
                new Staff {FIO = "Сапожников В. И.", Post = "Менеджер", Salles = "1 сделок", KPI="20% KPI"},
            };
            dgStaff.dgStaff.ItemsSource = staff;

        }
        private Dickplom1.Class.Charts charts;
        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new PieSeries<double> { Values = new double[] { 2 } },
                new PieSeries<double> { Values = new double[] { 4 } },
                new PieSeries<double> { Values = new double[] { 1 } },
                new PieSeries<double> { Values = new double[] { 4 } },
                new PieSeries<double> { Values = new double[] { 3 } }
            };
        public class Person
        {
            public string Number { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
        }

        public class Staff
        {
            public string FIO { get; set; }
            public string Post { get; set; }
            public string Salles { get; set; }
            public string KPI { get; set; }
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (gridSalesAllChoseDate.Visibility == Visibility.Collapsed)
                Dickplom1.Class.Animations.MaximazedReports(ImgReportsArrowDown, gridSalesAllChoseDate);
            else
                Dickplom1.Class.Animations.MinimazedReports(ImgReportsArrowDown, gridSalesAllChoseDate);
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            rbtnChoseDateSalesAllMounth.IsChecked = true;
            tbSalesAllChosenDate.Text = "за месяц";
        }

        private void TextBlock_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            rbtnChoseDateSalesAllWeek.IsChecked = true;
            tbSalesAllChosenDate.Text = "за неделю";
        }

        private void TextBlock_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            rbtnChoseDateSalesAllYear.IsChecked = true;
            tbSalesAllChosenDate.Text = "за год";

        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            tbYear.Opacity = 0.8;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            tbYear.Opacity = 1;
        }

        private void TextBlock_MouseEnter_1(object sender, MouseEventArgs e)
        {
            tbMounth.Opacity = 0.8;
        }

        private void TextBlock_MouseLeave_1(object sender, MouseEventArgs e)
        {
            tbMounth.Opacity = 1;
        }

        private void tbWeek_MouseEnter(object sender, MouseEventArgs e)
        {
            tbWeek.Opacity = 0.8;
        }

        private void tbWeek_MouseLeave(object sender, MouseEventArgs e)
        {
            tbWeek.Opacity = 1;
        }

        private void gridStatusSalesSorting_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            /*if (gridst.Visibility == Visibility.Collapsed)
                Animations.MaximazedReports(ImgReportsArrowDown, gridSalesAllChoseDate);
            else
                Animations.MinimazedReports(ImgReportsArrowDown, gridSalesAllChoseDate);*/
        }

        private void DataGridCustom_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = UIElement.MouseWheelEvent,
                    Source = sender
                };
                MainContentScroll.RaiseEvent(eventArg);
            }
        }

        private void dgStaff_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = UIElement.MouseWheelEvent,
                    Source = sender
                };
                StaffContentScroll.RaiseEvent(eventArg);
            }
        }

        DBEntities context = DBEntities.GetContext();

        public List<OrdersViewModel> allOrders = DBEntities.GetContext().Orders
    .Where(c => c.IsDeleted == false)
    .ToList()
    .Select(o => new OrdersViewModel
    {
        OrderId = o.OrderId,
        SubscriptionName = o.Subscription.SubscriptionName,
        FullNameClient = o.ClientsNaturalPersons != null
            ? o.ClientsNaturalPersons.Surname + " " +
              o.ClientsNaturalPersons.Name + " " +
              o.ClientsNaturalPersons.MiddleName
            : string.Empty,
        StartDate = o.StartDate?.ToString("d") ?? string.Empty,
        EndDate = o.EndDate?.ToString("d") ?? string.Empty,
        ClientId = o.ClientId ?? 0,
        OrderStatus = o.OrderStatus?.StatusValue ?? string.Empty,
        OrderStatusId = o.StatusId ?? 0,
        FIOManager = o.Users?.UserData != null
            ? o.Users.UserData.Surname + " " +
              o.Users.UserData.Name + " " +
              o.Users.UserData.MiddleName
            : string.Empty,
        CreatorId = o.CreatorId ?? 0,
        IsDeleted = o.IsDeleted
    })
    .ToList();


        private void tbStaffLidName_Loaded(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBlock;

            if (tb != null)
            {
                double maxWidth = 136;

                if (tb.ActualWidth > maxWidth)
                {
                    string[] parts = tb.Text.Split(' ');

                    if (parts.Length >= 2)
                    {
                        string firstInitial = parts[0].Substring(0, 1) + ".";
                        string lastName = parts[1];

                        tb.Text = $"{firstInitial} {lastName}";
                    }
                }
            }
                
        }

        private void tbStaffLidPost_Loaded(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBlock;

            if (tb != null)
            {
                double maxWidth = 136;

                if (tb.ActualWidth > maxWidth)
                {
                    string[] parts = tb.Text.Split(' ');

                    if (parts.Length >= 2)
                    {
                        string firstInitial = parts[0].Substring(0, 1) + ".";
                        string lastName = parts[1];

                        tb.Text = $"{firstInitial} {lastName}";
                    }
                    else
                    {
                        tb.Text = tb.Text.Remove(1, tb.Text.Length - 1) + ".";
                    }
                }
            }
            LeaderKPI();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null && mainWindow.gridSearch != null)
            {
                mainWindow.gridSearch.Visibility = Visibility.Collapsed;
            }
            charts = DataContext as Dickplom1.Class.Charts;

            MakeStatistic();

        }
        private Dickplom1.Class.Charts _charts = new Dickplom1.Class.Charts();
        public void MakeStatistic()
        {
            var context = DBEntities.GetContext();

            //Продано всего
            try
            {
                if (tbSalesAllChosenDate.Text.Contains("за год"))
                {
                    int ordersAll =
                        context.Orders.Count(s => s.StatusId > 1 && s.CreatedAt.HasValue && s.CreatedAt.Value.Year == DateTime.Now.Year) +
                        context.OrdersLegalEntities.Count(s => s.StatusId > 1 && s.CreatedAt.HasValue && s.CreatedAt.Value.Year == DateTime.Now.Year);
                    tbSallesAll.Text = ordersAll.ToString();
                }
                if (tbSalesAllChosenDate.Text.Contains("за месяц"))
                {
                    int ordersAll =
                        context.Orders.Count(s => s.StatusId > 1 && s.CreatedAt.HasValue && s.CreatedAt.Value.Month == DateTime.Now.Month) +
                        context.OrdersLegalEntities.Count(s => s.StatusId > 1 && s.CreatedAt.HasValue && s.CreatedAt.Value.Month == DateTime.Now.Month);
                    tbSallesAll.Text = ordersAll.ToString();
                }
                if (tbSalesAllChosenDate.Text.Contains("за неделю"))
                {
                    var today = DateTime.Today;
                    int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
                    DateTime weekStart = today.AddDays(-1 * diff).Date;
                    DateTime weekEnd = weekStart.AddDays(7).Date;

                    int ordersAll =
                        context.Orders.Count(s =>
                            s.StatusId > 1 &&
                            s.CreatedAt.HasValue &&
                            s.CreatedAt.Value >= weekStart &&
                            s.CreatedAt.Value < weekEnd) +
                        context.OrdersLegalEntities.Count(s =>
                            s.StatusId > 1 &&
                            s.CreatedAt.HasValue &&
                            s.CreatedAt.Value >= weekStart &&
                            s.CreatedAt.Value < weekEnd);

                    tbSallesAll.Text = ordersAll.ToString();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            //_________
            try
            {
                DateTime now = DateTime.Now;
                DateTime weekStart = now.AddDays(-(int)now.DayOfWeek + 1); // начало недели (понедельник)
                DateTime weekEnd = weekStart.AddDays(7);

                if (tbdate1 != null)
                {
                    if (tbdate1.tb.Text.Contains("за год"))
                    {
                        int ordersYear = context.Orders.Count(o => o.StatusId > 1 && o.CreatedAt.HasValue && o.CreatedAt.Value.Year == now.Year)
                            + context.OrdersLegalEntities.Count(o => o.StatusId > 1 && o.CreatedAt.HasValue && o.CreatedAt.Value.Year == now.Year);

                        var selectedSub = context.Subscription.FirstOrDefault(f=>f.SubscriptionId == ordersYear);

                        if (selectedSub != null)
                            tbLead.Text = selectedSub.SubscriptionName;
                        else
                            tbLead.Text = "-";
                    }
                    if (tbdate1.tb.Text.Contains("за месяц"))
                    {
                        int ordersMonth = context.Orders.Count(o => o.StatusId > 1 && o.CreatedAt.HasValue && o.CreatedAt.Value.Month == now.Month && o.CreatedAt.Value.Year == now.Year)
                            + context.OrdersLegalEntities.Count(o => o.StatusId > 1 && o.CreatedAt.HasValue && o.CreatedAt.Value.Month == now.Month && o.CreatedAt.Value.Year == now.Year);

                        var selectedSub = context.Subscription.FirstOrDefault(f => f.SubscriptionId == ordersMonth);

                        if (selectedSub != null)
                            tbLead.Text = selectedSub.SubscriptionName;
                        else
                            tbLead.Text = "-";
                    }
                    if (tbdate1.tb.Text.Contains("за неделю"))
                    {
                        int ordersWeek = context.Orders.Count(o => o.StatusId > 1 && o.CreatedAt.HasValue && o.CreatedAt.Value >= weekStart && o.CreatedAt.Value < weekEnd)
                            + context.OrdersLegalEntities.Count(o => o.StatusId > 1 && o.CreatedAt.HasValue && o.CreatedAt.Value >= weekStart && o.CreatedAt.Value < weekEnd);

                        var selectedSub = context.Subscription.FirstOrDefault(f => f.SubscriptionId == ordersWeek);

                        if (selectedSub != null)
                            tbLead.Text = selectedSub.SubscriptionName;
                        else
                            tbLead.Text = "-";
                    }
                }
            }
            catch (Exception)
            {
            }
            try
            {
                if (tbdate2 != null)
                {
                    if (tbdate2.tb.Text.Contains("за год"))
                    {
                        int ordersReady =
                            context.Orders.Count(s => s.StatusId == 6 && s.CreatedAt.HasValue && s.CreatedAt.Value.Year == DateTime.Now.Year) +
                            context.OrdersLegalEntities.Count(s => s.StatusId == 6 && s.CreatedAt.HasValue && s.CreatedAt.Value.Year == DateTime.Now.Year);
                        int ordersCanceled =
                            context.Orders.Count(s => s.StatusId == 7 && s.CreatedAt.HasValue && s.CreatedAt.Value.Year == DateTime.Now.Year) +
                            context.OrdersLegalEntities.Count(s => s.StatusId == 7 && s.CreatedAt.HasValue && s.CreatedAt.Value.Year == DateTime.Now.Year);

                        tbSallesReady.Text = ordersReady.ToString() + " ";
                        tbSallesCanceled.Text = ordersCanceled.ToString() + " ";


                        int totalOrders = ordersReady + ordersCanceled;

                        var pieSeries = _charts.Series.ElementAt(1) as PieSeries<double>;

                        if (pieSeries != null && totalOrders > 0)
                        {
                            charts.SetCompletedOrdersPercent((double)ordersReady / totalOrders * 100);
                        }
                        else
                        {
                            charts.SetCompletedOrdersPercent(0);
                        }
                    }
                    if (tbdate2.tb.Text.Contains("за месяц"))
                    {
                        int ordersReady =
                            context.Orders.Count(s => s.StatusId == 6 && s.CreatedAt.HasValue && s.CreatedAt.Value.Month == DateTime.Now.Month) +
                            context.OrdersLegalEntities.Count(s => s.StatusId == 6 && s.CreatedAt.HasValue && s.CreatedAt.Value.Month == DateTime.Now.Month);
                        int ordersCanceled =
                            context.Orders.Count(s => s.StatusId == 7 && s.CreatedAt.HasValue && s.CreatedAt.Value.Month == DateTime.Now.Month) +
                            context.OrdersLegalEntities.Count(s => s.StatusId == 6 && s.CreatedAt.HasValue && s.CreatedAt.Value.Month == DateTime.Now.Month);

                        tbSallesReady.Text = ordersReady.ToString() + " ";
                        tbSallesCanceled.Text = ordersCanceled.ToString() + " ";


                        int totalOrders = ordersReady + ordersCanceled;

                        var pieSeries = _charts.Series.ElementAt(1) as PieSeries<double>;

                        if (pieSeries != null && totalOrders > 0)
                        {
                            charts.SetCompletedOrdersPercent((double)ordersReady / totalOrders * 100);
                        }
                        else
                        {
                            charts.SetCompletedOrdersPercent(0);
                        }
                    }
                    if (tbdate2.tb.Text.Contains("за неделю"))
                    {
                        var today = DateTime.Today;
                        int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
                        DateTime weekStart = today.AddDays(-1 * diff).Date;
                        DateTime weekEnd = weekStart.AddDays(7).Date;

                        int ordersReady =
                            context.Orders.Count(s =>
                                s.StatusId == 6 &&
                                s.CreatedAt.HasValue &&
                                s.CreatedAt.Value >= weekStart &&
                                s.CreatedAt.Value < weekEnd) +
                            context.OrdersLegalEntities.Count(s =>
                                s.StatusId == 6 &&
                                s.CreatedAt.HasValue &&
                                s.CreatedAt.Value >= weekStart &&
                                s.CreatedAt.Value < weekEnd);

                        int ordersCanceled =
                            context.Orders.Count(s =>
                                s.StatusId == 7 &&
                                s.CreatedAt.HasValue &&
                                s.CreatedAt.Value >= weekStart &&
                                s.CreatedAt.Value < weekEnd) +
                            context.OrdersLegalEntities.Count(s =>
                                s.StatusId == 7 &&
                                s.CreatedAt.HasValue &&
                                s.CreatedAt.Value >= weekStart &&
                                s.CreatedAt.Value < weekEnd);

                        tbSallesReady.Text = ordersReady.ToString() + " ";
                        tbSallesCanceled.Text = ordersCanceled.ToString() + " ";


                        int totalOrders = ordersReady + ordersCanceled;

                        var pieSeries = _charts.Series.ElementAt(1) as PieSeries<double>;

                        if (pieSeries != null && totalOrders > 0)
                        {
                            double percent = Math.Round((double)ordersReady / totalOrders * 100, 2);
                            charts.SetCompletedOrdersPercent(percent);
                        }
                        else
                        {
                            charts.SetCompletedOrdersPercent(0);
                        }
                    }
                }

            }
            catch (Exception)
            {
            }

            try
            {
                var allOrders = DBEntities.GetContext().Orders
                    .Where(c => c.IsDeleted == false && c.Subscription.SubscriptionTypeId == 1)
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
                        ClientId = o.ClientId ?? 0,
                        OrderStatus = o.OrderStatus.StatusValue,
                        OrderStatusId = o.StatusId ?? 0,
                        FIOManager = o.Users?.UserData.Surname + " " + o.Users?.UserData.Name + " " + o.Users?.UserData.MiddleName,
                        CreatorId = o.CreatorId ?? 0,
                        IsDeleted = o.IsDeleted
                    })
                    .ToList();

                var charts = this.DataContext as Dickplom1.Class.Charts;
                if (charts != null)
                {
                    var ordersData = charts.PrepareOrdersData(allOrders, "Месяц"); // или "Неделя", "Год"
                    charts.UpdateDynamicSalesData(ordersData, "Месяц");
                }
            }
            catch (Exception)
            {

            }

        }

        private void tbSalesAllChosenDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            MakeStatistic();
        }

        private void CustomComboboxForChoseDateGray_Loaded(object sender, RoutedEventArgs e)
        {
            //tbSallesLid.Text
        }

        private void tbSallesLid_TextChanged(object sender, TextChangedEventArgs e)
        {
            MakeStatistic();
        }

        private void CustomComboboxForChoseDateGray_Loaded_1(object sender, RoutedEventArgs e)
        {
            tbdate1.tb.TextChanged += Tb_TextChanged;
        }

        private void Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            MakeStatistic();
        }

        private void CustomComboboxForChoseDateGray_Loaded_2(object sender, RoutedEventArgs e)
        {
            tbdate2.tb.TextChanged += Tb_TextChanged1;
        }

        private void Tb_TextChanged1(object sender, TextChangedEventArgs e)
        {
            MakeStatistic();
        }

        private void cbOrders_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            var items = new List<object>();

            // Заглушка
            items.Add(new { OrdersId = 1, OrdersName = "Заказы для физических лиц" });
            items.Add(new { OrdersId = 2, OrdersName = "Заказы для юридических лиц" });

            cbOrders.cbox.ItemsSource = items;
            cbOrders.cbox.DisplayMemberPath = "OrdersName";
            cbOrders.cbox.SelectedValuePath = "OrdersId";
            cbOrders.cbox.SelectedIndex = 0;
            cbOrders.cbox.SelectionChanged += Cbox_SelectionChanged;
        }

        private void Cbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridOrdersRefresh();
        }
        public void DataGridOrdersRefresh()
        {
            var context = DBEntities.GetContext();

            try
            {
                if (cbOrders != null)
                {
                    if ((int)cbOrders.cbox.SelectedValue == 1)
                    {
                        var orders = context
                            .Orders
                            .Where(c => c.IsDeleted == false && c.Subscription.SubscriptionTypeId == 1)
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
                                ClientId = o.ClientId ?? 0,
                                OrderStatus = o.OrderStatus.StatusValue,
                                OrderStatusId = o.StatusId ?? 0,
                                FIOManager = o.Users?.UserData.Surname + " " + o.Users?.UserData.Name + " " + o.Users?.UserData.MiddleName,
                                CreatorId = o.CreatorId ?? 0,
                                IsDeleted = o.IsDeleted
                            })
                            .ToList();

                        for (int i = 0; i < orders.Count; i++)
                        {
                            orders[i].Number = i + 1; // начинаем с 1
                        }

                        DataGridCustom.dg.ItemsSource = orders;
                    }
                    else if ((int)cbOrders.cbox.SelectedValue == 2)
                    {
                        var ordersLegal = context.OrdersLegalEntities
                            .Where(c => c.IsDeleted == true)
                            .ToList()
                            .Select(o => new OrdersViewModel
                            {
                                OrderId = o.OrderId,
                                SubscriptionName = o.Subscription.SubscriptionName,
                                CompanyName = o.ClientsLegalEntities.ClientsLegalEntitiesCompanyData.CompanyName,
                                StartDate = o.StartDate?.ToString("d"),
                                EndDate = o.EndDate?.ToString("d"),
                                OrderStatus = o.OrderStatus.StatusValue,
                                OrderStatusId = o.StatusId ?? 0,
                                ClientId = o.ClientId ?? 0,
                                CreatorId = o.CreatorId ?? 0,
                                FIOManager = o.Users?.UserData.Surname + " " + o.Users?.UserData.Name + " " + o.Users?.UserData.MiddleName,
                                FullNameClient = (
                                context.ClientsLegalEntitiesContactPerson
                                .Where(f => f.IsActive == true && f.CompanyId == o.ClientsLegalEntities.ClientsLegalEntitiesCompanyData.CompanyId)
                                .Select(f => (f.Surname ?? " ") + " " + (f.Name ?? " ") + " " + (f.Middlename ?? " "))
                                .FirstOrDefault() ?? "—"
                                ).Trim()
                            })
                            .ToList();

                        for (int i = 0; i < ordersLegal.Count; i++)
                        {
                            ordersLegal[i].Number = i + 1;
                        }

                        DataGridCustom.dg.ItemsSource = ordersLegal;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void DataGridCustom_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridOrdersRefresh();
        }

        private void ChartDynamicSales_Loaded(object sender, RoutedEventArgs e)
        {
            if (tbDynamicSalesValue != null)
            {
                var context = DBEntities.GetContext();
                var allOrders = context.Orders.Where(f=>!f.IsDeleted).Count();
                if (allOrders != null && allOrders != 0)
                {
                    tbDynamicSalesValue.Text = allOrders.ToString();
                }
            }
        }

        private void dgStaff_Loaded(object sender, RoutedEventArgs e)
        {
            LoadStaffWithKPI();
        }
        public List<StaffViewModel> AllStaff {  get; set; } = null;
        public void LoadStaffWithKPI()
        {
            try
            {
                var context = DBEntities.GetContext();

                // Загружаем заказы (физические + юридические)
                var ordersPhysical = context.Orders
                    .Where(o => o.IsDeleted == false && o.CreatorId != null)
                    .ToList();

                var ordersLegal = context.OrdersLegalEntities
                    .Where(o => o.IsDeleted == false && o.CreatorId != null)
                    .ToList();

                // Группируем по CreatorId, считаем количество заказов на каждого
                var allOrdersGrouped = ordersPhysical
                    .Concat<object>(ordersLegal)
                    .GroupBy(o => ((dynamic)o).CreatorId)
                    .ToDictionary(g => (int)g.Key, g => g.Count());

                // Считаем максимум для нормализации KPI
                int maxOrders = allOrdersGrouped.Values.Any() ? allOrdersGrouped.Values.Max() : 1;

                // Загружаем сотрудников с KPI
                var allStaff = context.Users
                    .Where(o => o.IsDeleted == false && o.UserData != null)
                    .ToList()
                    .Select(o => new StaffViewModel
                    {
                        UserId = o.UserId,
                        UserDataId = o.UserData.UserDataId,
                        UserPhoto = o.UserData.UserPhoto,
                        Surname = o.UserData?.Surname ?? "",
                        Name =  o.UserData?.Name ?? "",
                        MiddleName = o.UserData?.MiddleName ?? "",
                        FIOStaff = o.UserData.Surname + " " + o.UserData.Name + " " + o.UserData.MiddleName,
                        Email = o.UserData.Email,
                        Login = o.Login,
                        PhoneNumber = o.UserData.PhoneNumber ?? " ",
                        Role = o.Roles.NameRole ?? " ",
                        AccountStatusId = o.AccountStatusId ?? 0,
                        AccountStatus = o.AccountStatus.AccountStatusValue ?? " ",
                        IsDeleted = o.IsDeleted,
                        CreatorId = o.CreatorId ?? 2,
                        CreatedAt = o.CreatedAt ?? DateTime.MinValue,
                        KPI = allOrdersGrouped.ContainsKey(o.UserId)
                            ? Math.Round((double)allOrdersGrouped[o.UserId] / maxOrders * 100, 2)
                            : 0
                    })
                    .ToList();
                AllStaff = allStaff;

                // Привязка к DataGrid
                dgStaff.dgStaff.ItemsSource = allStaff;
                LeaderKPI();
            }
            catch (Exception)
            {

                throw;
            }
            

        }
        public void LeaderKPI()
        {
            try
            {
                if (AllStaff != null)
                {
                    var leader = AllStaff
                        .Where(s => !s.IsDeleted)
                        .OrderByDescending(s => s.KPI)
                        .FirstOrDefault();
                    if (leader != null)
                    {
                       

                        string leaderRole = leader.Role;
                        double leaderKPI = leader.KPI;

                        if (leader.Surname != null && leader.Name != null)
                            tbStaffLidName.Text = leader.Surname + " " + leader.Name.Remove(1,leader.Name.Length - 1) + ".";

                        tbStaffLidPost.Text = leaderRole;
                        tbStaffLidKPI.Text = leaderKPI.ToString("F1");
                    }
                    // Проверка и загрузка фото
                    if (leader.UserPhoto != null && leader.UserPhoto.Length > 0)
                    {
                        using (var stream = new MemoryStream(leader.UserPhoto))
                        {
                            var image = new BitmapImage();
                            image.BeginInit();
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.StreamSource = stream;
                            image.EndInit();
                            image.Freeze(); // Чтобы можно было использовать в UI-потоке

                            imgStaffLid.Source = image;
                            imgStaffLidBackground.Source = image;

                            imgStaffLidBackground.Clip = new RectangleGeometry
                            {
                                Rect = new Rect(0, 0, imgStaffLidBackground.Width, imgStaffLidBackground.Height),
                                RadiusX = 20,
                                RadiusY = 20
                            };
                        }
                    }

                }

            }
            catch (Exception)
            {

            }

        }

        private void rectStaffLidBlur_Loaded(object sender, RoutedEventArgs e)
        {
            LeaderKPI();
        }
    }
}
