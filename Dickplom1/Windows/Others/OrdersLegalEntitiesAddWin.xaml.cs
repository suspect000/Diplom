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
                .Select(u => new
                {
                    u.CompanyId,
                    u.CompanyName
                }));

            cboxCompany.cbox.ItemsSource = items;
            cboxCompany.cbox.DisplayMemberPath = "CompanyName";
            cboxCompany.cbox.SelectedValuePath = "CompanyId";
            cboxCompany.cbox.SelectedIndex = 0;
            cboxCompany.cbox.SelectionChanged += Cbox_SelectionChanged;
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
            //Выберите подписку
            SubscriptionsRefresh();
        }

        public void SubscriptionsRefresh()
        {
            var context = DBEntities.GetContext();

            var items = new List<object>();

            // Заглушка
            items.Add(new { SubscriptionId = 0, SubscriptionName = "Выберите подписку" });

            items.AddRange(context.Subscription
                .Where(s=> s.SubscriptionTypeId == 2)
                .Select(u => new
                {
                    u.SubscriptionId,
                    SubscriptionName = u.SubscriptionName + " " + u.SubscriptionPeriodMonth.SubscriptionPeriodMonthValue
                }));

            cboxSubscription.cbox.ItemsSource = items;
            cboxSubscription.cbox.DisplayMemberPath = "SubscriptionName";
            cboxSubscription.cbox.SelectedValuePath = "SubscriptionId";
            cboxSubscription.cbox.SelectedIndex = 0;
            cboxSubscription.cbox.SelectionChanged += Cbox_SelectionChanged2; ; ;
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
                    u.StatusId,
                    u.StatusValue
                }));

            cboxOrderStatus.cbox.ItemsSource = items;
            cboxOrderStatus.cbox.DisplayMemberPath = "OrderStatusName";
            cboxOrderStatus.cbox.SelectedValuePath = "OrderStatusId";
            cboxOrderStatus.cbox.SelectedIndex = 0;
            cboxOrderStatus.cbox.SelectionChanged += Cbox_SelectionChanged3; ; ; ;
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
                if ((int)cboxCompany.cbox.SelectedValue == 0)
                {
                    tblockCompanyName.Text = string.Empty;
                }
                else
                {
                    int company = Convert.ToInt32(cboxCompany.cbox.SelectedValue);

                    var client = context.ClientsLegalEntities
                        .Where(c => c.CompanyId == company)
                        .FirstOrDefault();

                    string companyName = client.ClientsLegalEntitiesCompanyData.CompanyName;

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
                        int month = Convert.ToInt32(cboxSubscription.cbox.Text.Substring(cboxSubscription.cbox.Text.Length - 1));
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
                    int month = Convert.ToInt32(subscriptionName.Substring(subscriptionName.Length - 1));
                    var subscription = context.Subscription
                        .Where(s => s.SubscriptionId == subId)
                        .FirstOrDefault();
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
                cboxContactPerson.Opacity = 1;
                cboxContactPerson.IsEnabled = true;
                ContactPersonRefresh();
            }
            else
            {
                cboxContactPerson.Opacity = 0.5;
                cboxContactPerson.IsEnabled = false;
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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }
    }
}
