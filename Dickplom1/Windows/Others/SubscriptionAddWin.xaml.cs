using Dickplom1.DataFolder;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
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
    /// Логика взаимодействия для SubscriptionAddWin.xaml
    /// </summary>
    public partial class SubscriptionAddWin : Window
    {
        public SubscriptionAddWin()
        {
            InitializeComponent();
        }
        public int SubscriptionId { get; set; } = 0;

        private void mainGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /*gridFocus.Focus();
            Keyboard.ClearFocus();*/
        }

        private void gridMovingWin_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void TextboxWithBorder_Loaded(object sender, RoutedEventArgs e)
        {
            tboxComment.tb.Height = 215;
            Padding = new Thickness(5,5,5,5);
            tboxComment.tb.TextWrapping = TextWrapping.Wrap;
            tboxComment.tb.VerticalAlignment = VerticalAlignment.Top;
        }

        private void tboxSubsriptionsPeriod_Loaded(object sender, RoutedEventArgs e)
        {
            SubscriptionsPeriodRefresh();
        }
        public void SubscriptionsPeriodRefresh()
        {
            var context = DBEntities.GetContext();

            var items = new List<object>();

            // Заглушка
            items.Add(new { SubscriptionPeriodId = 0, SubscriptionPeriodName = "Период подписки в месяцах" });

            items.AddRange(context.SubscriptionPeriodMonth
                .Select(u => new
                {
                    SubscriptionPeriodId = u.SubscriptionPeriodMonthId,
                    SubscriptionPeriodName = u.SubscriptionPeriodMonthValue + " " + "(мес)"

                }));

            cboxSubsriptionsPeriod.cbox.ItemsSource = items;
            cboxSubsriptionsPeriod.cbox.DisplayMemberPath = "SubscriptionPeriodName";
            cboxSubsriptionsPeriod.cbox.SelectedValuePath = "SubscriptionPeriodId";
            cboxSubsriptionsPeriod.cbox.SelectedIndex = 0;
        }
        private void ButtomWithBorder_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.btnWithBorder.Click += BtnWithBorder_Click; ;
        }
        private void BtnWithBorder_Click(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            if (tboxSubscriptionName.Text == "Название"
                || cboxSubsriptionsPeriod.cbox.Text == "Период подписки в месяцах"
                || tboxPriceForMonth.tb.Text == "Цена за месяц"
                || string.IsNullOrWhiteSpace(tboxSubscriptionName.Text)
                || string.IsNullOrWhiteSpace(cboxSubsriptionsPeriod.cbox.Text)
                || string.IsNullOrWhiteSpace(tboxPriceForMonth.tb.Text)
                || string.IsNullOrWhiteSpace(tboxComment.tb.Text))
            {
                MessageBox.Show("Необходимо заполнить все поля");
                return;
            }
            else
            {
                if (context.Subscription.FirstOrDefault(f=>f.SubscriptionName == tboxSubscriptionName.Text) != null)
                {
                    MessageBox.Show("Подписка с таким названием уже существует в системе");
                    return;
                }
                //Создание подписки
                Subscription sub = new Subscription()
                {
                    SubscriptionName = tboxSubscriptionName.Text,
                    SubscriptionPeriodId = (int)cboxSubsriptionsPeriod.cbox.SelectedValue,
                    SubscriptionTypeId = 2,
                    PriceForMonth = Convert.ToDecimal(tboxPriceForMonth.tb.Text),
                    PriceFull = Convert.ToDecimal(Convert.ToDouble(tboxPriceForMonth.tb.Text) * (int)cboxSubsriptionsPeriod.cbox.SelectedValue),
                    /*CreatorId = Сюда добавить id создателя когда реализуешь авторизацию в системе!!!!!!!!!!!!!!!*/
                    CreatedAt = DateTime.Now

                };
                if (tboxComment.tb.Text == "Комментарий")
                    sub.Comment = "-";
                else
                    sub.Comment = tboxComment.tb.Text;

                context.Subscription.Add(sub);
                context.SaveChanges();
                MessageBox.Show("Запись успешно добавлена");
                this.Close();
            }
        }
    }
}
