using CustomControlsForDiplomFramework;
using Dickplom1.DataFolder;
using Dickplom1.Pages.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace Dickplom1.Windows.Others
{
    /// <summary>
    /// Логика взаимодействия для OrdersNaturalPersonsAddWin.xaml
    /// </summary>
    public partial class OrdersNaturalPersonsAddWin : Window
    {
        public OrdersNaturalPersonsAddWin()
        {
            InitializeComponent();
        }
        public int OrderId {  get; set; } = 0;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (OrderId != 0)
            {
                try
                {
                    var context = DBEntities.GetContext();
                    var selectedOrder = context.Orders.FirstOrDefault(f=>f.OrderId == OrderId);

                    if (selectedOrder != null)
                        datePicker.dp.SelectedDate = selectedOrder.StartDate;
                    
                }
                catch (Exception)
                {
                }

            }
        }

        private void gridMovingWin_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void mainGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!sp.IsMouseOver)
            {
                gridFocus.Focus();
                Keyboard.ClearFocus();
            }
        }
           

        private void cboxSubscription_Loaded(object sender, RoutedEventArgs e)
        {
            SubscriptionsRefresh();
        }

        public void SubscriptionsRefresh()
        {
            var context = DBEntities.GetContext();

            var items = new List<object>();

            // Заглушка
            items.Add(new { SubscriptionId = 0, SubscriptionName = "Выберите подписку" });

            items.AddRange(context.Subscription
                .Where(s => s.SubscriptionTypeId == 1 && s.IsDeleted == false)
                .Select(u => new
                {
                    u.SubscriptionId,
                    SubscriptionName = u.SubscriptionName + " (" + u.SubscriptionPeriodMonth.SubscriptionPeriodMonthValue + " мес)"
                }));

            cboxSubscription.cbox.ItemsSource = items;
            cboxSubscription.cbox.DisplayMemberPath = "SubscriptionName";
            cboxSubscription.cbox.SelectedValuePath = "SubscriptionId";
            cboxSubscription.cbox.SelectedIndex = 0;
            cboxSubscription.cbox.SelectionChanged += Cbox_SelectionChanged;

            //Если это не добавление а обновление данных то загружаем данные
            if (OrderId != 0)
            {
                try
                {
                    var selectedOrder = context.Orders.FirstOrDefault(f => f.OrderId == OrderId);

                    if (selectedOrder != null)
                        cboxSubscription.cbox.SelectedValue = selectedOrder.SubscriptionId;
                }
                catch (Exception)
                {

                }

            }
        }

        private void cboxClient_Loaded(object sender, RoutedEventArgs e)
        {
            ClientsRefresh();
        }

        public void ClientsRefresh()
        {
            var context = DBEntities.GetContext();

            var items = new List<object>();

            // Заглушка
            items.Add(new { ClientId = 0, ClientName = "Выберите клиента" });

            items.AddRange(context.ClientsNaturalPersons
                .Where(w=>w.IsDeleted == false)
                .Select(u => new
                {
                    u.ClientNaturalPersonsId,
                    ClientName = u.Surname + " " + u.Name + " " + u.MiddleName + " "
                }));

            cboxClient.cbox.ItemsSource = items;
            cboxClient.cbox.DisplayMemberPath = "ClientName";
            cboxClient.cbox.SelectedValuePath = "ClientNaturalPersonsId";
            cboxClient.cbox.SelectedIndex = 0;
            cboxClient.cbox.SelectionChanged += Cbox_SelectionChanged1;

            //Если это не добавление а обновление данных то загружаем данные
            if (OrderId != 0)
            {
                try
                {
                    var selectedOrder = context.Orders.FirstOrDefault(f => f.OrderId == OrderId);

                    if (selectedOrder != null)
                        cboxClient.cbox.SelectedValue = selectedOrder.ClientId;
                }
                catch (Exception)
                {
                }

            }
        }

        private void cboxOrderStatus_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            var items = new List<object>();

            // Заглушка
            items.Add(new { StatusId = 0, StatusValue = "Выберите статус заказа" });

            items.AddRange(context.OrderStatus
                .Select(u => new
                {
                    u.StatusId,
                    u.StatusValue
                }));

            cboxOrderStatus.cbox.ItemsSource = items;
            cboxOrderStatus.cbox.DisplayMemberPath = "StatusValue";
            cboxOrderStatus.cbox.SelectedValuePath = "StatusId";
            cboxOrderStatus.cbox.SelectedIndex = 0;
            cboxOrderStatus.cbox.SelectionChanged += Cbox_SelectionChanged2;

            //Если это не добавление а обновление данных то загружаем данные
            if (OrderId != 0)
            {
                try
                {
                    var selectedOrder = context.Orders.FirstOrDefault(f => f.OrderId == OrderId);

                    if (selectedOrder != null)
                        cboxOrderStatus.cbox.SelectedValue = selectedOrder.OrderStatus.StatusId;
                }
                catch (Exception)
                {
                }

            }
        }
        private void datePicker_Loaded(object sender, RoutedEventArgs e)
        {
            datePicker.dp.SelectedDateChanged += Dp_SelectedDateChanged;
        }

        private void Cbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetDataToTextBlocks();
        }
        private void Dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SetDataToTextBlocks();
        }

        private void Cbox_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            SetDataToTextBlocks();
        }
        private void Cbox_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            SetDataToTextBlocks();
        }

        public string endDatE { get; set; }
        public string priceAll { get; set; }



        private void SetDataToTextBlocks()
        {
            var context = DBEntities.GetContext();
            var selectedItem = cboxSubscription.cbox.SelectedItem;

            if (selectedItem != null)
            {
                var subscriptionName = selectedItem
                    .GetType()
                    .GetProperty("SubscriptionName")
                    ?.GetValue(selectedItem)
                    ?.ToString();

                var subscriptionId = selectedItem
                    .GetType()
                    .GetProperty("SubscriptionId")
                    ?.GetValue(selectedItem)
                    ?.ToString();

                if (Convert.ToInt32(subscriptionId) == 0)
                {
                    tblockSubscription.Text = string.Empty;
                }
                else
                {
                    //Подписка
                    tblockSubscription.Text = string.Empty;
                    tblockSubscription.Text = subscriptionName.ToString();
                }
            }

            //Период
            try
            {
                if (datePicker.dp.SelectedDate == null)
                {
                    tblockPeriod.Text = string.Empty;
                }
                else
                {
                    DateTime.TryParse(datePicker.dp.SelectedDate.ToString(), out DateTime startDate);
                    if (cboxSubscription.cbox.SelectedIndex != 0 && cboxSubscription.cbox.SelectedValue != null)
                    {
                        var subscription = context.Subscription.FirstOrDefault(f => f.SubscriptionId == (int)cboxSubscription.cbox.SelectedValue);
                        int month = Convert.ToInt32(subscription.SubscriptionPeriodMonth.SubscriptionPeriodMonthValue);
                        DateTime endDate = startDate.AddMonths(month);

                        tblockPeriod.Text = string.Empty;
                        tblockPeriod.Text = $"{startDate.ToString("d")} - {endDate.ToString("d")}";

                        endDatE = endDate.ToString();
                    }
                }
                
            }
            catch (Exception)
            {
            }

            //Клиент
            try
            {
                if (cboxClient.cbox.SelectedValue == null)
                {
                    tblockClientFIO.Text = string.Empty;
                }
                else
                {
                    int clientId = Convert.ToInt32(cboxClient.cbox.SelectedValue);

                    var client = context.ClientsNaturalPersons
                        .Where(c=>c.ClientNaturalPersonsId == clientId)
                        .FirstOrDefault();

                    string fullName = $"{client.Surname} {client.Name} {client.MiddleName}";

                    tblockClientFIO.Text = fullName;
                }
            }
            catch (Exception)
            {
            }

            //Статус
            try
            {
                if (Convert.ToInt32(cboxOrderStatus.cbox.SelectedValue) == 0)
                {
                    tblockStatus.Text = string.Empty;
                }
                else
                {
                    int statusId = Convert.ToInt32(cboxOrderStatus.cbox.SelectedValue);
                    var statusName = context.OrderStatus
                        .Where(s => s.StatusId == statusId)
                        .FirstOrDefault();

                    tblockStatus.Text = statusName.StatusValue.ToString();
                }
            }
            catch (Exception)
            {
            }

            //Цена
            try
            {
                if (Convert.ToInt32(cboxSubscription.cbox.SelectedValue) == 0)
                    tblockItogo.Text = string.Empty;
                
                else
                {
                    var subscriptionName = selectedItem
                    .GetType()
                    .GetProperty("SubscriptionName")
                    ?.GetValue(selectedItem)
                    ?.ToString();

                    int subId = Convert.ToInt32(cboxSubscription.cbox.SelectedValue);
                    var subscription = context.Subscription.FirstOrDefault(f => f.SubscriptionId == (int)cboxSubscription.cbox.SelectedValue);
                    int month = Convert.ToInt32(subscription.SubscriptionPeriodMonth.SubscriptionPeriodMonthValue);
                    int price = Convert.ToInt32(subscription.PriceForMonth) * month;

                    tblockItogo.Text = price.ToString() + " руб";

                    priceAll = price.ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnSave_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.btnWithBorder.Click += BtnWithBorder_Click;
        }

        private void BtnWithBorder_Click(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            if (cboxSubscription.cbox.SelectedIndex == 0 
                || cboxClient.cbox.SelectedIndex == 0 
                || cboxOrderStatus.cbox.SelectedIndex == 0
                || datePicker.dp.SelectedDate == null)
            {
                MessageBox.Show("Необходимо заполнить все поля");
                return;
            }
            else
            {
                
                try
                {
                    if (endDatE == null) return;
                    if (priceAll == null) return;

                    //Рекдактирование заказа
                    if (OrderId != 0)
                    {
                        var selectedOrder = context.Orders.FirstOrDefault(f=>f.OrderId == OrderId);

                        if (selectedOrder != null)
                        {
                            var orderActiveOld = context.Orders.FirstOrDefault(f => f.ClientId == selectedOrder.ClientId && f.OrderId != selectedOrder.OrderId && f.StatusId > 1 & f.StatusId < 6 && f.IsDeleted == false);
                            if (orderActiveOld != null)
                            {
                                if ((int)cboxOrderStatus.cbox.SelectedValue >= 2 && (int)cboxOrderStatus.cbox.SelectedValue <= 6)
                                {
                                    MessageBox.Show("У выбранного клиента уже есть активный заказ");
                                    return;
                                }
                            }
                            if (DateTime.TryParse(datePicker.dp.Text, out DateTime dateParsedNew))
                            {
                                if (dateParsedNew.Date < DateTime.Now.AddMonths(-18) || dateParsedNew.Date > DateTime.Now.Date.AddMonths(18))
                                {
                                    MessageBox.Show("Некорректная указана дата");
                                    return;
                                }
                                if (dateParsedNew.Date < DateTime.Now.Date)
                                {
                                    MessageBoxButton btns = MessageBoxButton.YesNo;
                                    MessageBoxResult box = MessageBox.Show("Дата заказа указана за прошлое время\nЖелаете продолжить?", "Внимание", btns);
                                    if (box == MessageBoxResult.No)
                                        return;
                                }
                            }
                            selectedOrder.SubscriptionId = (int)cboxSubscription.cbox.SelectedValue;
                            selectedOrder.ClientId = (int)cboxClient.cbox.SelectedValue;
                            selectedOrder.StartDate = datePicker.dp.SelectedDate ?? DateTime.MinValue;
                            selectedOrder.EndDate = DateTime.Parse(endDatE);
                            selectedOrder.StatusId = (int)cboxOrderStatus.cbox.SelectedValue;
                            selectedOrder.Price = Convert.ToInt32(priceAll);
                        }
                        context.SaveChanges();
                        MessageBox.Show("Заказ успешно обновлен");
                        this.Close();
                        return;
                    }

                    if (DateTime.TryParse(datePicker.dp.Text, out DateTime dateParsed))
                    {
                        if (dateParsed.Date < DateTime.Now.AddMonths(-18) || dateParsed.Date > DateTime.Now.Date.AddMonths(18))
                        {
                            MessageBox.Show("Некорректно указана дата");
                            return;
                        }
                        if (dateParsed.Date < DateTime.Now.Date)
                        {
                            MessageBoxButton btns = MessageBoxButton.YesNo;
                            MessageBoxResult box = MessageBox.Show("Дата заказа указана за прошлое время\nЖелаете продолжить?", "Внимание", btns);
                            if (box == MessageBoxResult.No)
                                return;
                        }
                    }
                    //Создание заказа
                    Orders newOrder = new Orders
                    {
                        SubscriptionId = (int)cboxSubscription.cbox.SelectedValue,
                        ClientId = (int)cboxClient.cbox.SelectedValue,
                        StartDate = datePicker.dp.SelectedDate ?? DateTime.MinValue,
                        EndDate = DateTime.Parse(endDatE),
                        StatusId = (int)cboxOrderStatus.cbox.SelectedValue,
                        Price = Convert.ToInt32(priceAll),
                        CreatedAt = DateTime.Parse(DateTime.Now.ToString("g"))
                        //CreatorId = this Как сделаю авторизацию в приложении добавить сюда текущий manager id 
                    };

                    var orderActive = context.Orders.FirstOrDefault(f=>f.ClientId == newOrder.ClientId && f.StatusId > 1 & f.StatusId < 6 && f.IsDeleted == false);
                    if (orderActive != null)
                    {
                        if (newOrder.StatusId >= 2 && newOrder.StatusId <= 6)
                        {
                            MessageBox.Show("У выбранного клиента уже есть активный заказ");
                            return;
                        }
                    }

                    context.Orders.Add(newOrder);
                    context.SaveChanges();
                    MessageBox.Show("Заказ успешно добавлен");
                    this.Close();
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnAddSubscription_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionAddWin win = new SubscriptionAddWin();
            win.Closed += Win_Closed1;
            win.ShowDialog();
        }

        private void Win_Closed1(object sender, EventArgs e)
        {
            SubscriptionsRefresh();
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            ClientsNaturalPersonAddWin client = new ClientsNaturalPersonAddWin();
            client.ShowDialog();
        }

        private void btnAddPlusWhiteTheme_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClientsNaturalPersonAddWin win = new ClientsNaturalPersonAddWin();
            win.Closed += Win_Closed;
            win.ShowDialog();
        }

        private void Win_Closed(object sender, EventArgs e)
        {
            ClientsRefresh();
        }

        private void datePicker_TextInput(object sender, TextCompositionEventArgs e)
        {
            SetDataToTextBlocks();
        }
    }
}
