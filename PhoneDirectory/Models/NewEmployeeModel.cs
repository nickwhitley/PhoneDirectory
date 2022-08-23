
namespace PhoneDirectory.Models
{
    public class NewEmployeeModel
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        [Phone]
        public string PhoneMain { get; set; }
        [MaxLength(20)]
        [Phone]
        public string PhoneMobile { get; set; }
        [MinLength(3)]
        [MaxLength(5)]
        public string Extension { get; set; }
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
