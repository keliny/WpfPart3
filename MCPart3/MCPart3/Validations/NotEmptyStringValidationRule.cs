using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace MCPart3.Validations
{
    public class NotEmptyStringValidationRule : ValidationRule
    {
        public override System.Windows.Controls.ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (String.IsNullOrWhiteSpace(value as string))
            {
                MessageBox.Show($"Minutes can be between 0 and 60.");
                return new System.Windows.Controls.ValidationResult(false, "No empty field allowed!");
            }
            return System.Windows.Controls.ValidationResult.ValidResult;
        }
    }
}