using MCPart3.Models;
using System.Linq;
using System.Windows;
using static MCPart3.Validations.Validations;

namespace MCPart3.Windows
{
    /// <summary>
    /// Interaction logic for Update.xaml
    ///
    /// Zároveň bude možné doplněk editovat, přičemž lze upravit vše kromě množství na skladě. ???
    /// 
    /// </summary>
    public partial class Update : Window
    {
        readonly MCDB _db = new MCDB();
        private int _id;
        private Accessory _acc;

        public Update(int id)
        {
            _id = id;
            InitializeComponent();
            _acc = (from a in _db.Accessories where a.Id == _id select a).Single();
            CategoryInput.ItemsSource = _db.Categories.ToList();
            CategoryInput.SelectedItem = _acc.Category;
            NameInput.Text = _acc.Name;
            MinimumAmountInput.Text = _acc.Min.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void ValidateName(object sender, RoutedEventArgs e)
        {
            var res = ValidateNames(sender, e);
        }

        private void ValidateStored(object sender, RoutedEventArgs e)
        {
            var res = ValidateRecommendedAmounts(sender, e);
        }
    }
}
