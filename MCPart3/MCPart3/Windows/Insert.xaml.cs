using MCPart3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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

        public Insert()
        {
            InitializeComponent();
            CategoryInput.ItemsSource = _db.Categories.ToList();
            CategoryInput.SelectedItem = _db.Categories.FirstOrDefault();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void ValidateName(object sender, RoutedEventArgs e)
        {
            var result = ValidateNames(sender, e);
        }

        private void ValidateRecommendedAmount(object sender, RoutedEventArgs e)
        {
            var result = ValidateRecommendedAmounts(sender, e);
        }
    }
}
