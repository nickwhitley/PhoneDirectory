
using PhoneDirectory.Models;

namespace PhoneDirectory.Pages
{
    public partial class EmployeeDetails
    {
        [Parameter]
        public int Id { get; set; }

        private EmployeeModel employee;
        private List<EmployeeModel> availableSupervisors = new List<EmployeeModel>();
        private string submitButtonText;
        private bool changesMade = false;
        private bool changesSaved = false;
        private bool confirmDelete = false;
        private string deleteButtonText;
        private bool validId;
        protected override async Task OnInitializedAsync()
        {
            var employeesList = dataFactory.Employees;
            if (!employeesList.Any(e => e.Id == Id))
            {
                validId = false;
            }
            else
            {
                employee = employeesList.First(e => e.Id == Id);
                availableSupervisors = PopulateAvailableSupervisors();
                validId = true;
            }

            submitButtonText = "Submit";
            deleteButtonText = "Delete Employee";
            await base.OnInitializedAsync();
        }

        private List<EmployeeModel> PopulateAvailableSupervisors()
        {
            availableSupervisors = dataFactory.GetAvailableSupervisors(employee.TitleId, employee.DepartmentId);
            return availableSupervisors;
        }

        private void OnInfoChange(FocusEventArgs args)
        {
            changesMade = true;
            submitButtonText = "submit";
            PopulateAvailableSupervisors();
        }

        private void SaveEmployeeForm()
        {
            changesSaved = true;
            changesMade = false;
            submitButtonText = "Changes Saved";
            dataFactory.UpdateEmployeeAsync(employee);
        }

        private void NewEmployeePage()
        {
            if (!changesSaved)
            {
                dataFactory.ClearCache();
            }

            navManager.NavigateTo($"/Create/Employee");
        }

        private async Task ResetForm()
        {
            if (!changesSaved)
            {
                dataFactory.ClearCache();
                await dataFactory.PopulateDataAsync();
            }

            navManager.NavigateTo($"/EmployeeDetails/{employee.Id}", forceLoad: true);
        }

        private async void DeleteEmployee()
        {
            if (deleteButtonText == "Delete Employee")
            {
                deleteButtonText = "Confirm Delete";
                StateHasChanged();
            }
            else if (deleteButtonText == "Confirm Delete")
            {
                await dataFactory.DeleteEmployeeAsync(employee);
                deleteButtonText = "Employee Deleted!";
                await Task.Delay(3500);
                OpenDirectory();
            }
        }

        private void OpenDirectory()
        {
            if (!changesSaved)
            {
                dataFactory.ClearCache();
            }

            navManager.NavigateTo("/");
        }
    }
}