using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneDirectoryLibrary.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Names can be no longer than 20 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Invalid name character.")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Names can be no longer than 20 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Invalid name character.")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneMain { get; set; }
        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneMobile { get; set; }
        [MinLength(3)]
        [MaxLength(5)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Extension can only contain numbers.")]
        public string Extension { get; set; } = "";
        [MaxLength(130)]
        public string Notes { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid title.")]
        public int TitleId { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid department.")]
        public int DepartmentId { get; set; }
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid supervisor.")]
        public int SupId { get; set; }
        public TitleModel Title { get; set; }
        public DepartmentModel Department { get; set; }
        public EmployeeModel Supervisor { get; set; }
        [Required]
        [MaxLength(35)]
        [EmailAddress]
        public string Email { get; set; }

    }
}
