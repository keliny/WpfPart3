using MCPart3.Models;
using System.Collections.Generic;
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
        public Dictionary<string, bool> ValidationStatus { get; set; }

        public AcceptDelivery()
        {
            InitializeComponent();
            CategoryInput.ItemsSource = _db.Categories.ToList();
            ValidationStatus = new Dictionary<string, bool>()
            {
                {"Category", false },
                {"Accessory", false },
                {"Amount", false }
            };
        }

        private void CategorySelected(object sender, SelectionChangedEventArgs e)
        {
            var index = CategoryInput.SelectedIndex;
            if (index == -1)
            {
                AccessoryInput.IsEnabled = false;
                AmountInput.Text = "";
                AmountInput.IsEnabled = false;
                ValidationStatus["Category"] = false;
                ValidationStatus["Accessory"] = false;
                ValidationStatus["Amount"] = false;
            }
            else
            {
                AccessoryInput.IsEnabled = true;
                AccessoryInput.ItemsSource = _db.Accessories.Where(a =>
                    a.Status == "Active").ToList().Where(c => c.Category == (CategoryInput.SelectedItem as Category)).ToList();
                ValidationStatus["Category"] = true;
                ValidationStatus["Accessory"] = false;
                ValidationStatus["Amount"] = false;
                AmountInput.Text = "";
                AmountInput.IsEnabled = false;
            }
        }

        private void AccessorySelected(object sender, SelectionChangedEventArgs e)
        {
            var index = CategoryInput.SelectedIndex;
            if (index == -1)
            {
                AmountInput.Text = "";
                AmountInput.IsEnabled = false;
                ValidationStatus["Accessory"] = false;
                ValidationStatus["Amount"] = false;
            }
            else
            {
                Acc = AccessoryInput.SelectedItem as Accessory;
                AmountInput.IsEnabled = true;
                ValidationStatus["Accessory"] = true;
                ValidationStatus["Amount"] = false;
            }
        }

        private void ValidateAmount(object sender, RoutedEventArgs e)
        {
            var res = Validations.Validations.ValidateAmountAccepted(Acc, sender);
            if (res)
            {
                ValidationStatus["Amount"] = true;
            }
            else
            {
                ValidationStatus["Amount"] = false;
            }
        }

        private void AcceptDeliveryBtn(object sender, RoutedEventArgs e)
        {
            if (ValidationStatus.ContainsValue(false))
            {
                var errors = "";
                foreach (var val in ValidationStatus.Where(v => v.Value == false))
                {
                    errors += val.Key + "\n";
                }
                MessageBox.Show($"There are some invalid values: \n{errors}");
            }
            else
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
}
