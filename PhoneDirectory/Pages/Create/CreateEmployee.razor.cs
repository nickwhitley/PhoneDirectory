

namespace PhoneDirectory.Pages.Create
{
    public partial class CreateEmployee
    {
        private EmployeeModel newEmployee = new EmployeeModel();
        private List<TitleModel> Titles;
        private List<DepartmentModel> Departments;
        private List<EmployeeModel> availableSupervisors = new List<EmployeeModel>();
        private string submitButtonText;

        protected override async Task OnInitializedAsync()
        {
            Titles = dataFactory.Titles;
            Departments = dataFactory.Departments;
            submitButtonText = "Create Employee";
            await base.OnInitializedAsync();
        }

        private async Task SaveNewEmployee()
        {
            submitButtonText = "Employee Created!";
            if(newEmployee.DepartmentId == 0)
            {
                newEmployee.DepartmentId = 1;
            }
            if(newEmployee.TitleId == 0)
            {
                newEmployee.TitleId = 9;
            }
            await dataFactory.AddEmployeeAsync(newEmployee);

            await Task.Delay(2000);
            OpenDirectory();
            
        }

        private void OnInfoChange(FocusEventArgs args)
        {
            if (newEmployee.TitleId != 0 && newEmployee.DepartmentId != 0)
            {
                availableSupervisors = dataFactory.GetAvailableSupervisors(newEmployee.TitleId, newEmployee.DepartmentId);
            }
        }

        private void OpenDirectory()
        {
            navManager.NavigateTo("/");
        }
    }
}