using MCPart3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static MCPart3.Validations.Validations;

namespace MCPart3.Windows
{
    /// <summary>
    /// Interaction logic for Insert.xaml
    /// </summary>
    public partial class Insert : Window
    {
        readonly MCDB _db = new MCDB();
        public List<string> ValidList { get; set; }
        public Dictionary<string, bool> ValidationStatus { get; set; }

        public Insert()
        {
            ValidationStatus = new Dictionary<string, bool>()
            {
                {"Category", false },
                {"Amount", false },
                {"Name", false }
            };
            InitializeComponent();
            CategoryInput.ItemsSource = _db.Categories.ToList();


        }

        private void CategoryChanged(object sender, SelectionChangedEventArgs e)
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
            var result = ValidateNames(sender, e);
            if (result)
            {
                ValidationStatus["Name"] = true;
            }
            else
            {
                ValidationStatus["Name"] = false;
            }
        }

        private void ValidateRecommendedAmount(object sender, RoutedEventArgs e)
        {
            var result = ValidateRecommendedAmounts(sender, e);

            if (result)
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
                var newAccessory = new Accessory()
                {
                    AmountStored = 0,
                    Min = int.Parse(RecommendedAmountInput.Text),
                    Name = NameInput.Text,
                    Status = "Active",
                    Category = CategoryInput.SelectedItem as Category
                };

                _db.Accessories.Add(newAccessory);
                _db.SaveChanges();
                MainWindow.datagrid.ItemsSource = _db.Accessories.Where(a => a.Status == "Active").ToList();
                MainWindow.UpdateGrid();
                Close();
            }


        }


    }
}
