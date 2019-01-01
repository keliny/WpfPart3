using MCPart3.Models;
using MCPart3.Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MCPart3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static MCDB _db = new MCDB();
        public static DataGrid datagrid;


        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        /// <summary>
        /// Initial load, set source for grid and call checker for items running low
        /// </summary>
        private void Load()
        {
            var test = _db.Categories.ToList(); // This is to get categories, without it they are empty

            MyDataGrid.ItemsSource = _db.Accessories.Where(a => a.Status == "Active").ToList();
            datagrid = MyDataGrid;
            UpdateGrid();

        }

        /// <summary>
        ///     Insert logic
        /// </summary>
        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            Insert windowInsert = new Insert();
            windowInsert.ShowDialog();
            datagrid.Items.Refresh();
            UpdateGrid();
        }

        /// <summary>
        /// Update logic
        /// </summary>
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MyDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select an Item first.");
            }
            else
            {
                int id = ((Accessory)MyDataGrid.SelectedItem).Id;
                Update windowUpdate = new Update(id);
                windowUpdate.ShowDialog();
                UpdateGrid();
            }


        }

        /// <summary>
        /// Delete logic - setting status to deleted, not really deleting it from db
        /// Reload the grid
        /// </summary>
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MyDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select an Item first.");
            }
            else
            {
                int id = ((Accessory)MyDataGrid.SelectedItem).Id;
                var deleteAccessory = _db.Accessories.Single(a => a.Id == id);
                deleteAccessory.Status = "Deleted";
                _db.SaveChanges();
                datagrid.ItemsSource = _db.Accessories.Where(a => a.Status == "Active").ToList();
                UpdateGrid();
            }

        }

        /// <summary>
        /// Reduce the amount stored, compares it against stored values
        /// </summary>
        private void HandOverBtn_Click(object sender, RoutedEventArgs e)
        {
            HandOver windowUpdate = new HandOver();
            windowUpdate.ShowDialog();
            UpdateGrid();
        }

        /// <summary>
        /// Order new supplies
        /// </summary>
        private void AcceptDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            AcceptDelivery acDeliver = new AcceptDelivery();
            acDeliver.ShowDialog();
            UpdateGrid();
        }

        /// <summary>
        /// Basically a checker for which items are running low
        /// </summary>
        public static void UpdateGrid()
        {

            // This is to "generate" the containers for grid, without this the itemsSource is not enough to go through the list...
            System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(ProcessRows));
        }

        /// <summary>
        /// Necessary for color styling the grid rows
        /// </summary>
        private static void ProcessRows()
        {

            foreach (Accessory accessory in datagrid.ItemsSource)
            {
                var row = datagrid.ItemContainerGenerator.ContainerFromItem(accessory) as DataGridRow;
                if (accessory.AmountStored < accessory.Min)
                {
                    if (row != null) row.Background = Brushes.Red;
                }
            }
        }


    }
}
