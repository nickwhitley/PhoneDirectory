
namespace PhoneDirectory.Models
{
    public class NewEmployeeModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Name cannot be longer then 20 characters.")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Name cannot be longer then 20 characters.")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Phone number must in a valid format.")]
        public string PhoneMain { get; set; }
        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Phone number must in a valid format.")]
        public string PhoneMobile { get; set; }
        [MinLength(3)]
        [MaxLength(5)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Extension can only contain numbers.")]
        public string Extension { get; set; }
        [MaxLength(300)]
        public string Notes { get; set; }
        public int TitleId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public int SupId { get; set; }
        [Required]
        [MaxLength(35)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
