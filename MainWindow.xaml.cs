using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace BerezaShop
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<MenuItem> _items;
        private decimal _tipAmount = 0m;

        public MainWindow()
        {
            InitializeComponent();
            _items = new ObservableCollection<MenuItem>();
            LvBill.ItemsSource = _items;
            UpdateTotals();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_items.Count >= 5)
                {
                    MessageBox.Show("Досягнуто ліміт товарів (максимум 5).", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string desc = TxtDescription.Text.Trim();
                if (desc.Length < 3 || desc.Length > 20)
                {
                    MessageBox.Show("Назва має містити від 3 до 20 символів.", "Помилка вводу", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(TxtPrice.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price) || price <= 0)
                {
                    MessageBox.Show("Введіть коректну додатну ціну.", "Помилка вводу", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _items.Add(new MenuItem { Description = desc, Price = price });
                
                TxtDescription.Clear();
                TxtPrice.Clear();
                
                UpdateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при додаванні: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateTotals()
        {
            decimal netTotal = _items.Sum(i => i.Price);
            decimal gstAmount = netTotal * 0.05m;
            decimal totalAmount = netTotal + _tipAmount + gstAmount;

            LblNetTotal.Text = $"${netTotal:F2}";
            LblTipAmount.Text = $"${_tipAmount:F2}";
            LblGstAmount.Text = $"${gstAmount:F2}";
            LblTotalAmount.Text = $"${totalAmount:F2}";
        }
    }

    public class MenuItem
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}