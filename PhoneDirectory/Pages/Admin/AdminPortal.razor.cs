
namespace PhoneDirectory.Pages.Admin
{
    public partial class AdminPortal
    {
        private List<DepartmentModel> departments;
        private List<EmployeeModel> employees;
        private string employeeSearchText = "";
        protected override async Task OnInitializedAsync()
        {
            employees = dataFactory.Employees;
            departments = dataFactory.Departments;
            await base.OnInitializedAsync();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadFilter();
                await ApplyFilter();
                StateHasChanged();
            }
        }

        private async Task SaveFilter()
        {
            await sessionStorage.SetAsync(nameof(employeeSearchText), employeeSearchText);
        }

        private async Task LoadFilter()
        {
            var results = await sessionStorage.GetAsync<string>(nameof(employeeSearchText));
            employeeSearchText = results.Success ? results.Value : "";
        }

        private async Task ApplyFilter()
        {
            var output = dataFactory.Employees;
            if (string.IsNullOrWhiteSpace(employeeSearchText) == false)
            {
                var stringComparison = StringComparison.InvariantCultureIgnoreCase;
                output = output.Where(e => e.FirstName.StartsWith(employeeSearchText, stringComparison) || e.LastName.StartsWith(employeeSearchText, stringComparison) || e.Extension.Contains(employeeSearchText, stringComparison) || e.PhoneMain.Contains(employeeSearchText, stringComparison) || e.PhoneMobile.Contains(employeeSearchText, stringComparison)).ToList();
            }

            employees = output;
            await SaveFilter();
        }

        private void OpenDepartmentDetails(DepartmentModel department)
        {
            navManager.NavigateTo($"DepartmentDetails/{department.Id}");
        }

        private void OpenEmployeeDetails(EmployeeModel employee)
        {
            navManager.NavigateTo($"EmployeeDetails/{employee.Id}");
        }

        private void CreateNewEmployee()
        {
            navManager.NavigateTo("Create/Employee");
        }

        private void CreateNewDepartment()
        {
            navManager.NavigateTo("Create/Department");
        }

        private async Task OnSearchInput(string text)
        {
            employeeSearchText = text;
            await ApplyFilter();
        }

        private void AdminLogout()
        {
            OpenDirectory();
        }

        private void OpenDirectory()
        {
            navManager.NavigateTo("/");
        }
    }
}