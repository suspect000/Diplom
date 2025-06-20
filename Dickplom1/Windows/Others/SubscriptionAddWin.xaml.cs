using Dickplom1.DataFolder;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
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

            var context = DBEntities.GetContext();

            //Если это не добавление а обновление данных то загружаем данные
            if (SubscriptionId != 0)
            {
                try
                {
                    var selectedComment = context.Subscription.FirstOrDefault(f => f.SubscriptionId == SubscriptionId);

                    if (selectedComment != null)
                        tboxComment.Text = selectedComment.Comment;
                }
                catch (Exception)
                {

                }
            }
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

            //Если это не добавление а обновление данных то загружаем данные
            if (SubscriptionId != 0)
            {
                try
                {
                    var selectedPeriod = context.Subscription.FirstOrDefault(f => f.SubscriptionId == SubscriptionId);

                    if (selectedPeriod != null)
                        cboxSubsriptionsPeriod.cbox.SelectedValue = selectedPeriod.SubscriptionTypeId;
                }
                catch (Exception)
                {

                }

            }
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
            //Редактирование
            else if (SubscriptionId != 0)
            {
                var selectedSubscription = context.Subscription.FirstOrDefault(f=>f.SubscriptionId == SubscriptionId);

                    selectedSubscription.SubscriptionName = tboxSubscriptionName.Text;
                    selectedSubscription.SubscriptionPeriodId = (int)cboxSubsriptionsPeriod.cbox.SelectedValue;
                    selectedSubscription.SubscriptionTypeId = (int)cboxSubsriptionsType.cbox.SelectedValue;
                    selectedSubscription.PriceForMonth = Convert.ToDecimal(tboxPriceForMonth.tb.Text);
                    selectedSubscription.PriceFull = Convert.ToDecimal(Convert.ToDouble(tboxPriceForMonth.tb.Text) * (int)cboxSubsriptionsPeriod.cbox.SelectedValue);
                    /*selectedSubscription.CreatorId = Сюда добавить id создателя когда реализуешь авторизацию в системе!!!!!!!!!!!!!!!*/
                    selectedSubscription.CreatedAt = DateTime.Now;

                if (tboxComment.tb.Text == "Комментарий")
                    selectedSubscription.Comment = "-";
                else
                    selectedSubscription.Comment = tboxComment.tb.Text;

                context.SaveChanges();
                MessageBox.Show("Запись успешно обновлена");
                this.Close();
            }
            //Добавление
            else if (SubscriptionId == 0)
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
                    SubscriptionTypeId = (int)cboxSubsriptionsType.cbox.SelectedValue,
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

        private void tboxSubscriptionName_Loaded(object sender, RoutedEventArgs e)
        {
            var context = DBEntities.GetContext();

            //Если это не добавление а обновление данных то загружаем данные
            if (SubscriptionId != 0)
            {
                try
                {
                    var selectedName = context.Subscription.FirstOrDefault(f => f.SubscriptionId == SubscriptionId);

                    if (selectedName != null)
                        tboxSubscriptionName.Text = selectedName.SubscriptionName;
                }
                catch (Exception)
                {

                }

            }
        }

        private void tboxPriceForMonth_Loaded(object sender, RoutedEventArgs e)
        {
            tboxPriceForMonth.tb.MaxLength = 5;

            var context = DBEntities.GetContext();

            //Если это не добавление а обновление данных то загружаем данные
            if (SubscriptionId != 0)
            {
                try
                {
                    var selectedPrice = context.Subscription.FirstOrDefault(f => f.SubscriptionId == SubscriptionId);

                    if (selectedPrice != null)
                        tboxPriceForMonth.Text = selectedPrice.PriceForMonth.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void cboxSubsriptionsType_Loaded(object sender, RoutedEventArgs e)
        {
            SubscriptionsTypeRefresh();
        }

        public void SubscriptionsTypeRefresh()
        {
            var context = DBEntities.GetContext();

            var items = new List<object>();

            // Заглушка
            items.Add(new { SubscriptionTypeId = 0, SubscriptionTypeName = "Тип подписки" });

            items.AddRange(context.SubscriptionType
                .Select(u => new
                {
                    SubscriptionTypeId = u.SubscriptionTypeId,
                    SubscriptionTypeName = u.SubscriptionTypeValue

                }));

            cboxSubsriptionsType.cbox.ItemsSource = items;
            cboxSubsriptionsType.cbox.DisplayMemberPath = "SubscriptionTypeName";
            cboxSubsriptionsType.cbox.SelectedValuePath = "SubscriptionTypeId";
            cboxSubsriptionsType.cbox.SelectedIndex = 0;

            //Если это не добавление а обновление данных то загружаем данные
            if (SubscriptionId != 0)
            {
                try
                {
                    var selectedType = context.Subscription.FirstOrDefault(f => f.SubscriptionId == SubscriptionId);

                    if (selectedType != null)
                        cboxSubsriptionsType.cbox.SelectedValue = selectedType.SubscriptionTypeId;
                }
                catch (Exception)
                {

                }

            }
        }

        private void btnAddPlusWhiteTheme_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SubscriptionPeriodAdd win = new SubscriptionPeriodAdd();
            win.Closed += Win_Closed;
            win.ShowDialog();
        }

        private void Win_Closed(object sender, EventArgs e)
        {
            SubscriptionsPeriodRefresh();
        }

        private void tboxPriceForMonth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
    }
}
