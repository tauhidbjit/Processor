using System;
using System.Threading;
using System.Windows;

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addProductInfoButton_Click(object sender, RoutedEventArgs e)
        {
            showProductInfoWindow();
            //spinnerUserControl.Visibility = Visibility.Visible;
            //spinnerUserControl.Visibility = Visibility.Hidden;
        }

        private void showProductInfoWindow()
        {
            AddProductInfoWindow addProductInfoWindow = new AddProductInfoWindow();
            addProductInfoWindow.ShowDialog();
        }
    }
}
