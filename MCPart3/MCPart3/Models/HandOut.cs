using System;
using System.ComponentModel.DataAnnotations;

namespace MCPart3.Models
{
    public class HandOut
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name can be between 2 and 100 characters.", MinimumLength = 2)]
        public string CustomerName { get; set; }

        [Required]
        public Accessory Accessory { get; set; }
    }
}
