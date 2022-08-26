
namespace PhoneDirectory.Pages
{
    public partial class DepartmentDetails
    {
        [Parameter]
        public int Id { get; set; }

        private List<EmployeeModel> departmentEmployees;
        private DepartmentModel department;
        private string tempDepartmentName;
        private bool validId;
        protected override async Task OnInitializedAsync()
        {
            if (!dataFactory.Departments.Any(d => d.Id == Id))
            {
                validId = false;
            }
            else
            {
                department = (DepartmentModel)dataFactory.Departments.Where(d => d.Id == Id).First();
                departmentEmployees = dataFactory.Employees.Where(e => e.DepartmentId == Id).ToList();
                validId = true;
            }

            await base.OnInitializedAsync();
        }

        private void SaveDepartmentForm()
        {
            department.Name = tempDepartmentName;
        //dataFactory;
        }

        private void OpenCreateDepartment()
        {
            navManager.NavigateTo("/Create/Department");
        }

        private void OpenDirectory()
        {
            navManager.NavigateTo("/");
        }
    }
}