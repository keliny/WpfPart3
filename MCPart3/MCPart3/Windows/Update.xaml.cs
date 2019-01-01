using MCPart3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static MCPart3.Validations.Validations;

namespace MCPart3.Windows
{
    /// <summary>
    /// Interaction logic for Update.xaml 
    /// </summary>
    public partial class Update : Window
    {
        readonly MCDB _db = new MCDB();
        private int _id;
        private Accessory _acc;
        public Dictionary<string, bool> ValidationStatus { get; set; }

        public Update(int id)
        {
            _id = id;
            InitializeComponent();
            _acc = (from a in _db.Accessories where a.Id == _id select a).Single();
            CategoryInput.ItemsSource = _db.Categories.ToList();
            CategoryInput.SelectedItem = _acc.Category;
            NameInput.Text = _acc.Name;
            MinimumAmountInput.Text = _acc.Min.ToString();
            ValidationStatus = new Dictionary<string, bool>()
            {
                {"Category", true },
                {"Amount", true },
                {"Name", true }
            };
        }

        private void CategoryChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var index = CategoryInput.SelectedIndex;
            if (index == -1)
            {
                ValidationStatus["Category"] = false;

            }
            else
            {
                ValidationStatus["Category"] = true;
            }
        }

        private void ValidateName(object sender, RoutedEventArgs e)
        {
            var res = ValidateNames(sender, e);
            if (res)
            {
                ValidationStatus["Name"] = true;
            }
            else
            {
                ValidationStatus["Name"] = false;
            }
        }

        private void ValidateStored(object sender, RoutedEventArgs e)
        {
            var res = ValidateRecommendedAmounts(sender, e);
            if (res)
            {
                ValidationStatus["Amount"] = true;
            }
            else
            {
                ValidationStatus["Amount"] = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
                                             where a.Id == _id
                                             select a).Single();

                updateAccessory.Name = NameInput.Text;
                updateAccessory.Min = int.Parse(MinimumAmountInput.Text);
                updateAccessory.Category = CategoryInput.SelectedItem as Category;

                _db.SaveChanges();
                MainWindow.datagrid.ItemsSource = _db.Accessories.Where(a => a.Status == "Active").ToList();
                Close();
            }

        }
    }
}
