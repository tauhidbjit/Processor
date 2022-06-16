using Processor.Database;
using Processor.Dto.RequestModels;
using Processor.Repository;
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

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for AddProductInfoWindow.xaml
    /// </summary>
    public partial class AddProductInfoWindow : Window
    {
        public AddProductInfoWindow()
        {
            InitializeComponent();
            LoadDefaultButtonVisibility();
            LoadDataInGrid();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Battery battery = new Battery()
            {
                VendorName = vendorNameTextBox.Text,
                Grade = gradeTextBox.Text,
                SerialNo = serialNoTextBox.Text
            };

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.Products.Add(battery);
                unitOfWork.Commit();
            }
            ClearControls();
            LoadDataInGrid();
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
