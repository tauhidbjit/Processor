using Processor.Database;
using Processor.Repository;
using System.Linq;
using System.Windows;
using System.Threading;
using InventoryApp.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Threading.Tasks;

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for AddProductInfoWindow.xaml
    /// </summary>
    public partial class AddProductInfoWindow : Window, INotifyPropertyChanged
    {
        Thread th1;
        StatusViewModel statusVM;
        public AddProductInfoWindow()
        {
            //statusVM = new StatusViewModel();
            //statusVM.Status = "Ready";
            InitializeComponent();
            //DataContext = statusVM;
            DataContext = this;
            
            LoadDefaultButtonVisibility();
            LoadDataInGrid();
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

        private string _Hello = "Ready";
        public string Hello
        {
            get { return _Hello; }
            set
            {
                _Hello = value;
                OnPropertyChanged();
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            //th1 = new Thread(new ThreadStart(ProcessData));
            //th1.Start();
            ProcessData();
        }

        private void UpdateStatusText(string status)
        {
            statusTextBlock.Dispatcher.BeginInvoke((Action)(() => statusTextBlock.Text = status));
        }

        private void ProcessData()
        {
            //Application.Current.Dispatcher.Invoke(() =>
            //handler(this, new PropertyChangedEventArgs(propertyName)));

            Hello = "Data Processing is in progress..";
            UpdateStatusText(Hello);
            //Thread.Sleep(5000);
            //statusTextBlock.Text = _Hello;
            //DataContext = this;
            Battery battery = new Battery()
            {
                VendorName = vendorNameTextBox.Text,
                Grade = gradeTextBox.Text,
                SerialNo = serialNoTextBox.Text
            };

            // When the following code runs, a background thread is started and execution returned to 
            // main thread then the statusText is updated by "Data Processing is in progress.."
            
            Task.Run(() =>
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    unitOfWork.Products.Add(battery);
                    unitOfWork.Commit();
                    // To update main thread UI from background thread 
                    // We have to use Dispatcher.BeginInvoke()
                    // the following function communicates with UI and 
                    // updates the grid
                    Dispatcher.BeginInvoke(() => {
                        LoadDataInGrid();
                    });
                }

                // To update main thread UI from background thread 
                // We have to use Dispatcher.BeginInvoke()
                // the following function communicates with UI and 
                // updates status text
                UpdateStatusText("Product info added successfully!");
                //Dispatcher.BeginInvoke(() =>
                //{

                //    //_Hello = "Product info added successfully!";

                //    //_Hello = "Controls cleared!";
                //    //_Hello = "Loading data..";
                //    //LoadDataInGrid();
                //    //_Hello = "Data loaded successfully!";
                //});
            });

            
            //Dispatcher.BeginInvoke(() =>
            //{

            //    //_Hello = "Product info added successfully!";
            //    //ClearControls();
            //    //_Hello = "Controls cleared!";
            //    //_Hello = "Loading data..";
            //    //LoadDataInGrid();
            //    //_Hello = "Data loaded successfully!";
            //});
        }

        private void LoadDataInGrid()
        {
            //addButton.Visibility = Visibility.Visible;
            //updateButton.Visibility = Visibility.Hidden;
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                productInfoDataGrid.ItemsSource = unitOfWork.Products.GetAll().ToList();
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleAddUpdateButtonVisibility();
            int id = (productInfoDataGrid.SelectedItem as Battery).Id;
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Battery battery = new Battery();
                // Shoud use repository method getById like used in deleteButton_Click
                battery = (from m in unitOfWork.Products.GetAll()
                           where m.Id == id
                           select m).Single();

                vendorNameTextBox.Text = battery.VendorName;
                gradeTextBox.Text = battery.Grade;
                serialNoTextBox.Text = battery.SerialNo;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                int id = (productInfoDataGrid.SelectedItem as Battery).Id;
                Battery battery = unitOfWork.Products.Get(id);
                unitOfWork.Products.Remove(battery);
                unitOfWork.Commit();
            }
            LoadDataInGrid();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleAddUpdateButtonVisibility();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                int id = (productInfoDataGrid.SelectedItem as Battery).Id;

                Battery battery = new Battery();
                // Shoud use repository method getById like used in deleteButton_Click
                battery = (from m in unitOfWork.Products.GetAll()
                           where m.Id == id
                           select m).Single();

                battery.VendorName = vendorNameTextBox.Text;
                battery.Grade = gradeTextBox.Text;
                battery.SerialNo = serialNoTextBox.Text;
                unitOfWork.Commit();
            }
            ClearControls();
            LoadDataInGrid();
        }

        private void LoadDefaultButtonVisibility()
        {
            addButton.Visibility = Visibility.Visible;
            closeButton.Visibility = Visibility.Visible;
            updateButton.Visibility = Visibility.Hidden;
            cancelButton.Visibility = Visibility.Hidden;
        }
        private void ToggleAddUpdateButtonVisibility()
        {
            addButton.Visibility = (addButton.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible);
            closeButton.Visibility = (closeButton.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible);
            updateButton.Visibility = (updateButton.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible);
            cancelButton.Visibility = (cancelButton.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible);
        }

        private void ClearControls()
        {
            vendorNameTextBox.Text = string.Empty;
            gradeTextBox.Text = string.Empty;
            serialNoTextBox.Text = string.Empty;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            ToggleAddUpdateButtonVisibility();
        }
    }
}
