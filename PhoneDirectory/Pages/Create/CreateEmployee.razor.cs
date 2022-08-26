using PhoneDirectory.Models;

namespace PhoneDirectory.Pages.Create
{
    public partial class CreateEmployee
    {
        private NewEmployeeModel newEmployee = new NewEmployeeModel();
        private List<TitleModel> Titles;
        private List<DepartmentModel> Departments;
        private List<EmployeeModel> availableSupervisors = new List<EmployeeModel>();
        private string submitButtonText;
        //may not use!!!
        private string firstName;
        private string lastName;
        private string phoneMain;
        private string phoneMobile;
        private string extension;
        private string notes;
        private int titleId;
        private int departmentId;
        private int supervisorId;
        private string email;
        protected override async Task OnInitializedAsync()
        {
            Titles = dataFactory.Titles;
            Departments = dataFactory.Departments;
            submitButtonText = "Create Employee";
            await base.OnInitializedAsync();
        }

        private async Task SaveNewEmployee()
        {
            var employeeConvert = new EmployeeModel()
            {FirstName = newEmployee.FirstName, LastName = newEmployee.LastName, PhoneMain = newEmployee.PhoneMain, PhoneMobile = newEmployee.PhoneMobile, Extension = newEmployee.Extension, Notes = newEmployee.Notes, TitleId = newEmployee.TitleId, DepartmentId = newEmployee.DepartmentId, SupId = newEmployee.SupId, Email = newEmployee.Email};
            if (newEmployee.FirstName == null)
            {
                submitButtonText = "Nothing to submit!";
                await Task.Delay(2000);
                StateHasChanged();
                submitButtonText = "Create Employee";
            }
            else if (newEmployee.FirstName != null)
            {
                await dataFactory.AddEmployeeAsync(employeeConvert);
                submitButtonText = "Employee Created!";
                await Task.Delay(2000);
                OpenDirectory();
            }
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