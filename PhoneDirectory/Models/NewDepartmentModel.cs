

namespace PhoneDirectory.Models
{
    public class NewDepartmentModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
