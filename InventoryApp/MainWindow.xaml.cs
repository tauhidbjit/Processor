using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window//, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = this;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler? PropertyChanged;
        //public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion

        //private string _Hello = "Bismillah";
        //public string Hello
        //{
        //    get { return _Hello; }
        //    set
        //    {
        //        _Hello = value;
        //        OnPropertyChanged();
        //    }
        //}

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
