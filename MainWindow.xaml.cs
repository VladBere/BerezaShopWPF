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

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LvBill.SelectedItem is MenuItem selectedItem)
                {
                    _items.Remove(selectedItem);
                    if (_items.Count == 0) _tipAmount = 0;
                    UpdateTotals();
                }
                else
                {
                    MessageBox.Show("Будь ласка, виберіть товар зі списку для видалення.", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при видаленні: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnApplyTip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_items.Count == 0)
                {
                    MessageBox.Show("Рахунок порожній. Неможливо додати чайові.", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (!decimal.TryParse(TxtTipValue.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tipInput) || tipInput < 0)
                {
                    MessageBox.Show("Введіть коректне значення для чайових.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                decimal netTotal = _items.Sum(i => i.Price);

                if (RbPercentage.IsChecked == true)
                {
                    _tipAmount = netTotal * (tipInput / 100m);
                }
                else if (RbAmount.IsChecked == true)
                {
                    _tipAmount = tipInput;
                }

                UpdateTotals();
                TxtTipValue.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при розрахунку чайових: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _items.Clear();
                _tipAmount = 0;
                UpdateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при очищенні: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
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