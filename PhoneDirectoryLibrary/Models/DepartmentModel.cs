

namespace PhoneDirectoryLibrary.Models
{
    public class DepartmentModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Department Name can not contain over 30 characters.")]
        [RegularExpression(@"^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$", ErrorMessage = "Invalid department name format.")]
        public string Name { get; set; }
    }
}
