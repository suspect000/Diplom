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
    /// Логика взаимодействия для SubscriptionPeriodAdd.xaml
    /// </summary>
    public partial class SubscriptionPeriodAdd : Window
    {
        public SubscriptionPeriodAdd()
        {
            InitializeComponent();
        }

        private void btnSave_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.btnWithBorder.Click += BtnWithBorder_Click;
        }

        private void BtnWithBorder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tboxNewSubscriptionPeriod.Text != "Месяцы" && tboxNewSubscriptionPeriod.Text.All(char.IsDigit) && tboxNewSubscriptionPeriod != null)
                {
                    int mounths = Convert.ToInt32(tboxNewSubscriptionPeriod.Text);
                    if (mounths > 60)
                    {
                        MessageBox.Show($"Данный период слишком большой \nМаксимальный период подписки 60 месяцев");
                        return;
                    }

                    var context = DBEntities.GetContext();

                    SubscriptionPeriodMonth newPeriod = new SubscriptionPeriodMonth
                    {
                        SubscriptionPeriodMonthValue = tboxNewSubscriptionPeriod.Text,
                    };
                    if (newPeriod != null)
                    {
                        context.SubscriptionPeriodMonth.Add(newPeriod);
                        context.SaveChanges();
                        MessageBox.Show("Новое значение добавлено");
                        this.Close();
                    }
                }
            }
            catch (Exception)
            {
            }
            
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void tboxSubscriptionName_Loaded(object sender, RoutedEventArgs e)
        {
            tboxNewSubscriptionPeriod.tb.PreviewTextInput += Tb_PreviewTextInput;
        }

        private void Tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Проверяем только цифры
            e.Handled = !IsTextAllowed(textBox, e.Text);
        }
        private bool IsTextAllowed(TextBox textBox, string newText)
        {
            string resultingText = textBox.Text.Insert(textBox.SelectionStart, newText);

            return resultingText.Length <= 2 && resultingText.All(char.IsDigit);
        }
    }
}
