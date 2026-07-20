using System;
using System.Windows;
using System.Windows.Threading;

namespace BerezaShop
{
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Критична помилка в інтерфейсі:\n{e.Exception.Message}\n\nПрограму буде відновлено.", 
                            "Глобальна помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true; 
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                MessageBox.Show($"Критична системна помилка:\n{ex.Message}", 
                                "Глобальна помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}