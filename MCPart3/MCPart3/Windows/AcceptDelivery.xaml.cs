using MCPart3.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MCPart3.Windows
{
    /// <summary>
    /// Interaction logic for AcceptDelivery.xaml
    /// </summary>
    public partial class AcceptDelivery : Window
    {
        readonly MCDB _db = new MCDB();
        public Accessory Acc { get; set; }
        public AcceptDelivery()
        {
            InitializeComponent();
            CategoryInput.ItemsSource = _db.Categories.ToList();
        }

        private void CategorySelected(object sender, SelectionChangedEventArgs e)
        {
            var index = CategoryInput.SelectedIndex;
            if (index == -1)
            {
                AccessoryInput.IsEnabled = false;
                AmountInput.Text = "";
                AmountInput.IsEnabled = false;
            }
            else
            {
                AccessoryInput.IsEnabled = true;
                AccessoryInput.ItemsSource = _db.Accessories.Where(a =>
                    a.Status == "Active").ToList().Where(c => c.Category == (CategoryInput.SelectedItem as Category)).ToList();
            }
        }

        private void AccessorySelected(object sender, SelectionChangedEventArgs e)
        {
            var index = CategoryInput.SelectedIndex;
            if (index == -1)
            {
                AmountInput.Text = "";
                AmountInput.IsEnabled = false;
            }
            else
            {
                Acc = AccessoryInput.SelectedItem as Accessory;
                AmountInput.IsEnabled = true;
            }
        }

        private void ValidateAmount(object sender, RoutedEventArgs e)
        {
            var res = Validations.Validations.ValidateAmountAccepted(Acc, sender);
        }

        private void AcceptDeliveryBtn(object sender, RoutedEventArgs e)
        {
            Accessory updateAccessory = (from a in _db.Accessories
                                         where a.Id == Acc.Id
                                         select a).Single();

            updateAccessory.AmountStored += int.Parse(AmountInput.Text);

            _db.SaveChanges();
            MainWindow.datagrid.ItemsSource = _db.Accessories.Where(a => a.Status == "Active").ToList();
            Close();
        }
    }
}
