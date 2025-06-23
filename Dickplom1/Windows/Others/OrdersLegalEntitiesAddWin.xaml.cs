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
using System.Windows.Shapes;

namespace Dickplom1.Windows.Others
{
    /// <summary>
    /// Логика взаимодействия для OrdersLegalEntitiesAddWin.xaml
    /// </summary>
    public partial class OrdersLegalEntitiesAddWin : Window
    {
        public OrdersLegalEntitiesAddWin()
        {
            InitializeComponent();
        }
        public int OrderId { get; set; } = 0;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (OrderId != 0)
            {
                try
                {
                    var context = DBEntities.GetContext();
                    var selectedOrderLegalEntities = context.OrdersLegalEntities.FirstOrDefault(f => f.OrderId == OrderId);

                    if (selectedOrderLegalEntities != null)
                        datePicker.dp.SelectedDate = selectedOrderLegalEntities.StartDate;

                }
                catch (Exception)
                {
                }
            }
        }

        private void mainGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /*gridFocus.Focus();
            Keyboard.ClearFocus();*/
        }

        private void gridMovingWin_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //e.Handled = true;
        }

        private void cboxCompany_Loaded(object sender, RoutedEventArgs e)
        {
            CompanyRefresh();
            cboxCompany.cbox.SelectionChanged += Cbox_SelectionChanged4;
        }

        public void CompanyRefresh()
        {
            var context = DBEntities.GetContext();

            var items = new List<object>();

            // Заглушка
            items.Add(new { CompanyId = 0, CompanyName = "Выберите компанию"});

            items.AddRange(context.ClientsLegalEntitiesCompanyData
                .Where(company =>
                context.ClientsLegalEntities
                .Any(client => !client.IsDeleted && client.CompanyId == company.CompanyId))
                .Select(company => new
                {
                    company.CompanyId,
                    company.CompanyName
                    
                })
                .ToList());

            cboxCompany.cbox.ItemsSource = items;
            cboxCompany.cbox.DisplayMemberPath = "CompanyName";
            cboxCompany.cbox.SelectedValuePath = "CompanyId";
            cboxCompany.cbox.SelectedIndex = 0;
            cboxCompany.cbox.SelectionChanged += Cbox_SelectionChanged;

            /*items.AddRange(context.ClientsLegalEntitiesCompanyData
                .Select(u => new
                {
                    u.CompanyId,
                    u.CompanyName
                }));

            cboxCompany.cbox.ItemsSource = items;
            cboxCompany.cbox.DisplayMemberPath = "CompanyName";
            cboxCompany.cbox.SelectedValuePath = "CompanyId";
            cboxCompany.cbox.SelectedIndex = 0;
            cboxCompany.cbox.SelectionChanged += Cbox_SelectionChanged;*/

            //Если это не добавление а обновление данных то загружаем данные
            if (OrderId != 0)
            {
                try
                {
                    var selectedCompany = context.OrdersLegalEntities.FirstOrDefault(f => f.OrderId == OrderId);

                    if (selectedCompany != null)
                        cboxCompany.cbox.SelectedValue = selectedCompany.ClientsLegalEntities.CompanyId;
                }
                catch (Exception)
                {

                }

            }
            if ((int)cboxCompany.cbox.SelectedValue != 0 && cboxCompany.cbox.SelectedValue != null)
            {
                spContactPerson.Opacity = 1;
                spContactPerson.IsEnabled = true;
                ContactPersonRefresh();
            }
            else
            {
                spContactPerson.Opacity = 0.5;
                spContactPerson.IsEnabled = false;
            }
        }

        private void Cbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        private void Cbox_SelectionChanged3(object sender, SelectionChangedEventArgs e)
        {
            SetDataToTextBlocks();
        }

        private void cboxSubscription_Loaded(object sender, RoutedEventArgs e)
        {
            SubscriptionsRefresh();
            cboxSubscription.cbox.SelectionChanged += Cbox_SelectionChanged5;
        }

        private void Cbox_SelectionChanged5(object sender, SelectionChangedEventArgs e)
        {
            SetDataToTextBlocks();
        }

        public void SubscriptionsRefresh()
        {
            var context = DBEntities.GetContext();

            var items = new List<object>();

            // Заглушка
            items.Add(new { SubscriptionId = 0, SubscriptionName = "Выберите подписку" });

            items.AddRange(context.Subscription
                .Where(s=> s.SubscriptionTypeId == 2 && s.IsDeleted == false)
                .Select(u => new
                {
                    u.SubscriptionId,
                    SubscriptionName = u.SubscriptionName + " (" + u.SubscriptionPeriodMonth.SubscriptionPeriodMonthValue + " мес)"
                }));

            cboxSubscription.cbox.ItemsSource = items;
            cboxSubscription.cbox.DisplayMemberPath = "SubscriptionName";
            cboxSubscription.cbox.SelectedValuePath = "SubscriptionId";
            cboxSubscription.cbox.SelectedIndex = 0;
            cboxSubscription.cbox.SelectionChanged += Cbox_SelectionChanged2;

            //Если это не добавление а обновление данных то загружаем данные
            if (OrderId != 0)
            {
                try
                {
                    var selectedCompany = context.OrdersLegalEntities.FirstOrDefault(f => f.OrderId == OrderId);

                    if (selectedCompany != null)
                        cboxSubscription.cbox.SelectedValue = selectedCompany.SubscriptionId;
                }
                catch (Exception)
                {

                }

            }
        }

        private void cboxOrderStatus_Loaded(object sender, RoutedEventArgs e)
        {
            OrderStatusRefresh();
        }

        public void OrderStatusRefresh()
        {
            var context = DBEntities.GetContext();

            var items = new List<object>();

            // Заглушка
            items.Add(new { OrderStatusId = 0, OrderStatusName = "Выберите статус заказа" });

            items.AddRange(context.OrderStatus
                .Select(u => new
                {
                    OrderStatusId = u.StatusId,
                    OrderStatusName = u.StatusValue
                }));

            cboxOrderStatus.cbox.ItemsSource = items;
            cboxOrderStatus.cbox.DisplayMemberPath = "OrderStatusName";
            cboxOrderStatus.cbox.SelectedValuePath = "OrderStatusId";
            cboxOrderStatus.cbox.SelectedIndex = 0;
            cboxOrderStatus.cbox.SelectionChanged += Cbox_SelectionChanged3;

            //Если это не добавление а обновление данных то загружаем данные
            if (OrderId != 0)
            {
                try
                {
                    var selectedCompany = context.OrdersLegalEntities.FirstOrDefault(f => f.OrderId == OrderId);

                    if (selectedCompany != null)
                        cboxOrderStatus.cbox.SelectedValue = selectedCompany.OrderStatus.StatusId;
                }
                catch (Exception)
                {

                }
            }
        }

        public string endDatE { get; set; }
        public string priceAll { get; set; }

        private void SetDataToTextBlocks()
        {
            var context = DBEntities.GetContext();
            var selectedItem = cboxSubscription.cbox.SelectedItem;

            //Компания
            try
            {
                if (cboxCompany.cbox.SelectedValue == null || (int)cboxCompany.cbox.SelectedValue == 0)
                {
                    tblockCompanyName.Text = string.Empty;
                }
                else
                {
                    int company = Convert.ToInt32(cboxCompany.cbox.SelectedValue);

                    var client = context.ClientsLegalEntities
                        .Where(c => c.CompanyId == company)
                        .FirstOrDefault();

                    string companyName = client?.ClientsLegalEntitiesCompanyData.CompanyName ?? string.Empty;

                    tblockCompanyName.Text = companyName;
                }
            }
            catch (Exception)
            {
            }

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
                    tblockSubscriptionName.Text = string.Empty;
                }
                else
                {
                    //Подписка
                    tblockSubscriptionName.Text = string.Empty;
                    tblockSubscriptionName.Text = subscriptionName.ToString();
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
                    if (cboxSubscription.cbox.Text != "Выберите подписку")
                    {
                        if (cboxSubscription.cbox.SelectedValue != null && (int)cboxSubscription.cbox.SelectedValue != 0)
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

            }
            catch (Exception)
            {
            }

            //Клиент
            try
            {
                if (cboxContactPerson.cbox.SelectedValue == null || (int)cboxContactPerson.cbox.SelectedValue == 0)
                {
                    tblockContactPerson.Text = string.Empty;
                }
                else
                {
                    int clientId = Convert.ToInt32(cboxContactPerson.cbox.SelectedValue);

                    var client = context.ClientsLegalEntitiesContactPerson
                        .Where(c => c.ContactPersonId == clientId)
                        .FirstOrDefault();

                    string fullName = $"{client.Surname} " +
                        $"{client.Name} " +
                        $"{client.Middlename}";

                    tblockContactPerson.Text = fullName;
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
                    var subscription = context.Subscription.FirstOrDefault(f=>f.SubscriptionId == (int)cboxSubscription.cbox.SelectedValue);
                    int month = Convert.ToInt32(subscription.SubscriptionPeriodMonth?.SubscriptionPeriodMonthValue);
                    int price = Convert.ToInt32(subscription.PriceForMonth) * month;

                    tblockItogo.Text = price.ToString() + " руб";

                    priceAll = price.ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        private void Cbox_SelectionChanged4(object sender, SelectionChangedEventArgs e)
        {
            if ((int)cboxCompany.cbox.SelectedValue != 0 && cboxCompany.cbox.SelectedValue != null)
            {
                spContactPerson.Opacity = 1;
                spContactPerson.IsEnabled = true;
                ContactPersonRefresh();
            }
            else
            {
                spContactPerson.Opacity = 0.5;
                spContactPerson.IsEnabled = false;
            }
        }
            
        public void ContactPersonRefresh()
        {
            var context = DBEntities.GetContext();

            var items = new List<object>();

            int selectedCompanyId = (int)cboxCompany.cbox.SelectedValue;

            try
            {
                // Заглушка
                items.Add(new { ContactPersonId = 0, ContactPersonName = "Выберите представителя" });

                items.AddRange(context.ClientsLegalEntitiesContactPerson
                    .Where(c => c.CompanyId != null && c.CompanyId == selectedCompanyId)
                    .Select(u => new
                    {
                        ContactPersonId = u.ContactPersonId,
                        ContactPersonName = u.Surname 
                        + " " + u.Name 
                        + " " + u.Middlename
                    }));

                cboxContactPerson.cbox.ItemsSource = items;
                cboxContactPerson.cbox.DisplayMemberPath = "ContactPersonName";
                cboxContactPerson.cbox.SelectedValuePath = "ContactPersonId";
                cboxContactPerson.cbox.SelectedIndex = 0;
                cboxContactPerson.cbox.SelectionChanged += Cbox_SelectionChanged1;

                //Если это не добавление а обновление данных то загружаем данные
                if (OrderId != 0)
                {
                    try
                    {
                        var selectedOrder = context.OrdersLegalEntities.FirstOrDefault(f => f.OrderId == OrderId);
                        var selectedCompanyData = context.ClientsLegalEntitiesCompanyData.FirstOrDefault(f=>f.CompanyId == selectedOrder.ClientsLegalEntities.CompanyId);
                        var selectedContactPerson = context.ClientsLegalEntitiesContactPerson.FirstOrDefault(f=>f.CompanyId == selectedCompanyData.CompanyId && f.IsActive == true);

                        if (cboxContactPerson != null)
                            cboxContactPerson.cbox.SelectedValue = selectedContactPerson.ContactPersonId;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }

        private void datePicker_TextInput(object sender, TextCompositionEventArgs e)
        {
            SetDataToTextBlocks();
        }

        private void datePicker_Loaded(object sender, RoutedEventArgs e)
        {
            datePicker.dp.SelectedDateChanged += Dp_SelectedDateChanged;
        }

        private void Dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SetDataToTextBlocks();
        }

        private void btnAddPlusWhiteTheme_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ContactPersonAdd win = new ContactPersonAdd();

            if (cboxCompany.cbox.SelectedValue != null && (int)cboxCompany.cbox.SelectedValue != 0)
                win.CompanyId = (int)cboxCompany.cbox.SelectedValue;
            win.Closed += Win_Closed;
            win.ShowDialog();
        }

        private void Win_Closed(object sender, EventArgs e)
        {
            ContactPersonRefresh();
        }

        private void btnAddPlusWhiteTheme_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            SubscriptionAddWin win = new SubscriptionAddWin();
            win.Closed += Win_Closed1;
            win.ShowDialog();
        }

        private void Win_Closed1(object sender, EventArgs e)
        {
            SubscriptionsRefresh();
        }

        private void ButtomWithBorder_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.btnWithBorder.Click += BtnWithBorder_Click;
        }

        private void BtnWithBorder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mainWin = Application.Current.MainWindow as MainWindow;
                var context = DBEntities.GetContext();

                if (cboxCompany.cbox.SelectedIndex == 0
                    || cboxContactPerson.cbox.SelectedIndex == 0
                    || cboxSubscription.cbox.SelectedIndex == 0
                    || datePicker.dp.SelectedDate == null
                    || cboxOrderStatus.cbox.SelectedIndex == 0)
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
                            var selectedOrder = context.OrdersLegalEntities.FirstOrDefault(f => f.OrderId == OrderId);

                            //Поиск активных заказов у этого клиента
                            var orderActiveOld = context.OrdersLegalEntities.FirstOrDefault(f => f.ClientId == selectedOrder.ClientId && f.OrderId != selectedOrder.OrderId && f.StatusId > 1 & f.StatusId < 6 && f.IsDeleted == false);
                            if (orderActiveOld != null)
                            {
                                if ((int)cboxOrderStatus.cbox.SelectedValue >= 2 && (int)cboxOrderStatus.cbox.SelectedValue <= 5)
                                {
                                    MessageBox.Show("У выбранного клиента уже есть активный заказ");
                                    return;
                                }
                            }
                            if (DateTime.TryParse(datePicker.dp.Text, out DateTime dateParsedNew))
                            {
                                if (dateParsedNew.Date < DateTime.Now.AddMonths(-18) || dateParsedNew.Date > DateTime.Now.Date.AddMonths(18))
                                {
                                    MessageBox.Show("Некорректно указана дата");
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

                            if (selectedOrder != null)
                            {
                                selectedOrder.SubscriptionId = (int)cboxSubscription.cbox.SelectedValue;
                                selectedOrder.ClientId = context.OrdersLegalEntities.FirstOrDefault(f => f.OrderId == OrderId).ClientId;
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
                        OrdersLegalEntities newOrder = new OrdersLegalEntities
                        {
                            SubscriptionId = (int)cboxSubscription.cbox.SelectedValue,
                            ClientId = context.ClientsLegalEntities.FirstOrDefault(f => f.CompanyId == (int)cboxCompany.cbox.SelectedValue).ClientsLegalEntitiesId,
                            StartDate = datePicker.dp.SelectedDate ?? DateTime.MinValue,
                            EndDate = DateTime.Parse(endDatE),
                            StatusId = (int)cboxOrderStatus.cbox.SelectedValue,
                            Price = Convert.ToInt32(priceAll),
                            CreatedAt = DateTime.Parse(DateTime.Now.ToString("g")),
                            CreatorId = mainWin?.ActiveUser.UserId ?? 0
                        };

                        var orderActive = context.OrdersLegalEntities.FirstOrDefault(f => f.ClientId == newOrder.ClientId && f.OrderId != newOrder.OrderId && f.StatusId > 1 & f.StatusId < 6 && f.IsDeleted == false);
                        if (orderActive != null)
                        {
                            if (newOrder.StatusId >= 2 && newOrder.StatusId <= 5)
                            {
                                MessageBox.Show("У выбранного клиента уже есть активный заказ");
                                return;
                            }
                        }
                        context.OrdersLegalEntities.Add(newOrder);
                        context.SaveChanges();
                        MessageBox.Show("Заказ успешно добавлен");
                        this.Close();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAddPlusWhiteTheme_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            ClientsLegalEntitiesAddWin win = new ClientsLegalEntitiesAddWin();
            win.Closed += Win_Closed2;
            win.ShowDialog();
        }

        private void Win_Closed2(object sender, EventArgs e)
        {
            CompanyRefresh();
        }
    }
}
