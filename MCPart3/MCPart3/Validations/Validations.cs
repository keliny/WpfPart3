using MCPart3.Models;
using System.Windows;
using System.Windows.Controls;

namespace MCPart3.Validations
{
    public class Validations
    {
        public static bool ValidateNames(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Length > 100 || ((TextBox)sender).Text.Length < 2)
            {
                MessageBox.Show("Name must be between 2 and 100 characters!");
                return false;
            }
            return true;
        }

        public static bool ValidateRecommendedAmounts(object sender, RoutedEventArgs e)
        {
            var num = ((TextBox)sender).Text;
            if (string.IsNullOrWhiteSpace(num))
            {
                MessageBox.Show("Recommended amount can't be empty!");
                return false;
            }
            if (int.TryParse(num, out var numbResult))
            {
                if (numbResult >= 0 && numbResult <= 1000) return true;
                MessageBox.Show("Recommended amount must be between 0 and 1000!");
                return false;
            }
            MessageBox.Show("Recommended amount must be a number!");
            return false;
        }

        public static bool ValidatesStored(object sender, RoutedEventArgs e)
        {

            var num = ((TextBox)sender).Text;
            if (string.IsNullOrWhiteSpace(num))
            {
                MessageBox.Show("Stored amount can't be empty!");
                return false;
            }
            if (int.TryParse(num, out var numbResult))
            {
                if (numbResult >= 0 && numbResult <= 1000) return true;
                MessageBox.Show("Stored amount must be between 0 and 1000!");
                return false;
            }
            MessageBox.Show("Stored amount must be a number!");
            return false;
        }

        public static bool ValidateAmounts(Accessory acc, object amount, RoutedEventArgs e)
        {
            var num = ((TextBox)amount).Text;
            if (string.IsNullOrWhiteSpace(num))
            {
                MessageBox.Show("Amount can't be empty!");
                return false;
            }
            if (int.TryParse(num, out var numbResult))
            {
                if (numbResult >= 0 && numbResult <= acc.AmountStored) return true;
                MessageBox.Show($"Amount must be between 1 and {acc.AmountStored}!");
                return false;
            }
            MessageBox.Show("Amount must be a number!");
            return false;
        }

        public static bool ValidateAmountAccepted(Accessory acc, object amount)
        {
            var num = ((TextBox)amount).Text;
            if (string.IsNullOrWhiteSpace(num))
            {
                MessageBox.Show("Amount can't be empty!");
                return false;
            }
            if (int.TryParse(num, out var numbResult))
            {
                if (numbResult >= 0 && numbResult <= 1000 - acc.AmountStored) return true;
                MessageBox.Show($"Amount must be between 1 and {1000 - acc.AmountStored}!");
                return false;
            }
            MessageBox.Show("Amount must be a number!");
            return false;
        }

        public static bool ValidateCustomers(object name)
        {
            var text = ((TextBox)name).Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Name can't be empty!");
                return false;
            }
            if (text.Length < 2 || text.Length > 100)
            {
                MessageBox.Show("Name must be between 2 and 100 characters!");
                return false;
            }
            return true;
        }

    }
}
