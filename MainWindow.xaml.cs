using System;
using System.Collections.ObjectModel;
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

        private void UpdateTotals()
        {
            LblNetTotal.Text = "$0.00";
            LblTipAmount.Text = "$0.00";
            LblGstAmount.Text = "$0.00";
            LblTotalAmount.Text = "$0.00";
        }
    }

    public class MenuItem
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}