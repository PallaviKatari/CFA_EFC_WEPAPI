using System.ComponentModel.DataAnnotations;

namespace CFA_EFC_WEPAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
