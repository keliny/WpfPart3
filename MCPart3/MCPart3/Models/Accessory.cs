using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace MCPart3.Models
{
    public partial class Accessory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name can be between 2 and 100 characters.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000)]
        public int Min { get; set; }

        [Required]
        [Range(0, 1000)]
        public int AmountStored { get; set; }

        [Required]
        public string Status { get; set; }

        [Required] public Category Category { get; set; }
    }

    partial class Accessory : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (String.IsNullOrWhiteSpace(Name))
            {
                results.Add(new ValidationResult("First name cannot be null or empty"));
            }

            if (String.IsNullOrWhiteSpace(Min.ToString()))
            {
                results.Add(new ValidationResult("Min can't be empty."));
            }

            return results;
        }
    }
}
