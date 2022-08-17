using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneDirectoryLibrary.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneMain { get; set; }
        public string PhoneMobile { get; set; }
        public int Extension { get; set; }
        public string Notes { get; set; }
        public int TitleId { get; set; }
        public int DepartmentId { get; set; }
        public int SupervisorId { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
