using MCPart3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static MCPart3.Validations.Validations;

namespace MCPart3.Windows
{
    /// <summary>
    /// Interaction logic for HandOver.xaml
    /// </summary>
    public partial class HandOver : Window
    {
        readonly MCDB _db = new MCDB();
        public Accessory Acc { get; set; }
        public Dictionary<string, bool> ValidationStatus { get; set; }
        public HandOver()
        {
            InitializeComponent();
            CategoryInput.ItemsSource = _db.Categories.ToList();
            ValidationStatus = new Dictionary<string, bool>()
            {
                {"Category", false },
                {"Accessory", false },
                {"Amount", false },
                {"Customer", false }
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
            var res = ValidateAmounts(Acc, sender, e);
            if (res)
            {
                ValidationStatus["Amount"] = true;
            }
            else
            {
                ValidationStatus["Amount"] = false;
            }
        }

        private void ValidateCustomer(object sender, RoutedEventArgs e)
        {
            var res = ValidateCustomers(sender);
            if (res)
            {
                ValidationStatus["Customer"] = true;

            }
            else
            {
                ValidationStatus["Customer"] = false;

            }
        }

        private void HandOverBtn(object sender, RoutedEventArgs e)
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

                updateAccessory.AmountStored -= int.Parse(AmountInput.Text);
                var newHandOut = new HandOut()
                {
                    Date = DateTime.Now,
                    Accessory = updateAccessory,
                    CustomerName = CustomerInput.Text
                };

                _db.HandOuts.Add(newHandOut);
                _db.SaveChanges();

                MainWindow.datagrid.ItemsSource = _db.Accessories.Where(a => a.Status == "Active").ToList(); // not ideal I know... When called from the mainWindow it does not update the values in the grid...
                Close();
            }

        }

    }
}
