using System.ComponentModel.DataAnnotations;

namespace MCPart3.Models
{
    public class Category
    {
        [Key]
        [StringLength(100, ErrorMessage = "Category name can be between 2 and 100 characters.", MinimumLength = 2)]
        public string CategoryName { get; set; }

    }
}