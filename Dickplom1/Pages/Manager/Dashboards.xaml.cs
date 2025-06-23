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


        private void btnDynamicSalesChoseMounth_MouseEnter(object sender, MouseEventArgs e)
        {
                
                Dickplom1.Class.Animations.OpacityAnimation(btnDynamicSalesChoseMounth, btnDynamicSalesChoseMounth.Opacity, 0.7, 0.3);
        }

        private void btnDynamicSalesChoseMounth_MouseLeave(object sender, MouseEventArgs e)
        {
            Dickplom1.Class.Animations.OpacityAnimation(btnDynamicSalesChoseMounth, btnDynamicSalesChoseMounth.Opacity, 1, 0.3);
        }

        private void btnDynamicSalesChoseWeek_MouseEnter(object sender, MouseEventArgs e)
        {
            Dickplom1.Class.Animations.OpacityAnimation(btnDynamicSalesChoseWeek, btnDynamicSalesChoseMounth.Opacity, 0.7, 0.3);
        }

        private void btnDynamicSalesChoseWeek_MouseLeave(object sender, MouseEventArgs e)
        {
            Dickplom1.Class.Animations.OpacityAnimation(btnDynamicSalesChoseWeek, btnDynamicSalesChoseMounth.Opacity, 1, 0.3);
        }

        private void btnDynamicSalesChoseYear_MouseEnter(object sender, MouseEventArgs e)
        {
            Dickplom1.Class.Animations.OpacityAnimation(btnDynamicSalesChoseYear, btnDynamicSalesChoseMounth.Opacity, 0.7, 0.3);
        }

        private void btnDynamicSalesChoseYear_MouseLeave(object sender, MouseEventArgs e)
        {
            Dickplom1.Class.Animations.OpacityAnimation(btnDynamicSalesChoseYear, btnDynamicSalesChoseMounth.Opacity, 1, 0.3);

        }

        private void btnDynamicSalesChoseYear_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChartDynamicSales.XAxes = null;
            ChartDynamicSales.Series = null;
            var charts = this.DataContext as Dickplom1.Class.Charts;
            if (charts != null)
            {
                charts.UpdateXAxis("Год");
            }
        }

        private void btnDynamicSalesChoseMounth_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChartDynamicSales.XAxes = null;
            ChartDynamicSales.Series = null;
            var charts = this.DataContext as Dickplom1.Class.Charts;
            if (charts != null)
            {
                charts.UpdateXAxis("Месяц");
            }
        }

        private void btnDynamicSalesChoseWeek_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChartDynamicSales.XAxes = null;
            ChartDynamicSales.Series = null;
            var charts = this.DataContext as Dickplom1.Class.Charts;
            if (charts != null)
            {
                charts.UpdateXAxis("Неделя");
            }
        }

        private void imgStaffLid_Loaded(object sender, RoutedEventArgs e)
        {
            var image = sender as Image;
            
            var cornerRadius = 20.0;
            var clipRect = new RectangleGeometry
            {
                Rect = new Rect(0, 0, image.ActualWidth, image.ActualHeight),
                RadiusX = cornerRadius,
                RadiusY = cornerRadius
            };

            image.Clip = clipRect;
        }

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
                        context.Orders.Count(s => s.StatusId == 6 && s.CreatedAt.HasValue && s.CreatedAt.Value.Year == DateTime.Now.Year) +
                        context.OrdersLegalEntities.Count(s => s.StatusId == 6 && s.CreatedAt.HasValue && s.CreatedAt.Value.Year == DateTime.Now.Year);
                    tbSallesAll.Text = ordersAll.ToString();
                }
                if (tbSalesAllChosenDate.Text.Contains("за месяц"))
                {
                    int ordersAll =
                        context.Orders.Count(s => s.StatusId == 6 && s.CreatedAt.HasValue && s.CreatedAt.Value.Month == DateTime.Now.Month) +
                        context.OrdersLegalEntities.Count(s => s.StatusId == 6 && s.CreatedAt.HasValue && s.CreatedAt.Value.Month == DateTime.Now.Month);
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
                            s.StatusId == 6 &&
                            s.CreatedAt.HasValue &&
                            s.CreatedAt.Value >= weekStart &&
                            s.CreatedAt.Value < weekEnd) +
                        context.OrdersLegalEntities.Count(s =>
                            s.StatusId == 6 &&
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
                        int ordersYear = context.Orders.Count(o => o.StatusId == 6 && o.CreatedAt.HasValue && o.CreatedAt.Value.Year == now.Year)
                            + context.OrdersLegalEntities.Count(o => o.StatusId == 6 && o.CreatedAt.HasValue && o.CreatedAt.Value.Year == now.Year);

                        var selectedSub = context.Subscription.FirstOrDefault(f=>f.SubscriptionId == ordersYear);

                        if (selectedSub != null)
                            tbLead.Text = selectedSub.SubscriptionName;
                        else
                            tbLead.Text = "-";
                    }
                    if (tbdate1.tb.Text.Contains("за месяц"))
                    {
                        int ordersMonth = context.Orders.Count(o => o.StatusId == 6 && o.CreatedAt.HasValue && o.CreatedAt.Value.Month == now.Month && o.CreatedAt.Value.Year == now.Year)
                            + context.OrdersLegalEntities.Count(o => o.StatusId == 6 && o.CreatedAt.HasValue && o.CreatedAt.Value.Month == now.Month && o.CreatedAt.Value.Year == now.Year);

                        var selectedSub = context.Subscription.FirstOrDefault(f => f.SubscriptionId == ordersMonth);

                        if (selectedSub != null)
                            tbLead.Text = selectedSub.SubscriptionName;
                        else
                            tbLead.Text = "-";
                    }
                    if (tbdate1.tb.Text.Contains("за неделю"))
                    {
                        int ordersWeek = context.Orders.Count(o => o.StatusId == 6 && o.CreatedAt.HasValue && o.CreatedAt.Value >= weekStart && o.CreatedAt.Value < weekEnd)
                            + context.OrdersLegalEntities.Count(o => o.StatusId == 6 && o.CreatedAt.HasValue && o.CreatedAt.Value >= weekStart && o.CreatedAt.Value < weekEnd);

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
                            context.OrdersLegalEntities.Count(s => s.StatusId == 6 && s.CreatedAt.HasValue && s.CreatedAt.Value.Year == DateTime.Now.Year);

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
                            charts.SetCompletedOrdersPercent((double)ordersReady / totalOrders * 100);
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
    }
}
