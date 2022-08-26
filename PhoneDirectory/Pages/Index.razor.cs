
namespace PhoneDirectory.Pages
{
    public partial class Index
    {
        private List<EmployeeModel> employees = new List<EmployeeModel>();
        private List<DepartmentModel> departments = new List<DepartmentModel>();
        private List<TitleModel> titles = new List<TitleModel>();
        private string searchText = "";
        private string selectedDepartment = "All";
        private string selectedTitle = "All";
        List<string> sortingDropdownItems = new List<string>();
        private string currentSort = "";
        protected override async Task OnInitializedAsync()
        {
            await dataFactory.PopulateDataAsync();
            employees = dataFactory.Employees;
            departments = dataFactory.Departments;
            titles = dataFactory.Titles;
            sortingDropdownItems = PopulateSortDropdownItems();
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

        private async Task LoadFilter()
        {
            var results = await sessionStorage.GetAsync<string>(nameof(searchText));
            searchText = results.Success ? results.Value : "";
            results = await sessionStorage.GetAsync<string>(nameof(selectedDepartment));
            selectedDepartment = results.Success ? results.Value : "All";
            results = await sessionStorage.GetAsync<string>(nameof(selectedTitle));
            selectedTitle = results.Success ? results.Value : "All";
            results = await sessionStorage.GetAsync<string>(nameof(currentSort));
            currentSort = results.Success ? results.Value : "";
        }

        private async Task SaveFilter()
        {
            await sessionStorage.SetAsync(nameof(searchText), searchText);
            await sessionStorage.SetAsync(nameof(selectedDepartment), selectedDepartment);
            await sessionStorage.SetAsync(nameof(selectedTitle), selectedTitle);
            await sessionStorage.SetAsync(nameof(currentSort), currentSort);
        }

        private async Task ApplyFilter()
        {
            var output = dataFactory.Employees;
            if (selectedDepartment != "All")
            {
                output = output.Where(e => e.Department?.Name == selectedDepartment).OrderBy(e => e.Title == null).ThenBy(e => e.Title != null ? e.Title.TitleLevel : 0).ThenBy(e => e.LastName).ToList();
            }

            if (selectedTitle != "All")
            {
                output = output.Where(e => e.Title?.Name == selectedTitle).ToList();
            }

            if (string.IsNullOrWhiteSpace(searchText) == false)
            {
                var stringComparison = StringComparison.InvariantCultureIgnoreCase;
                output = output.Where(e => e.FirstName.StartsWith(searchText, stringComparison) || e.LastName.StartsWith(searchText, stringComparison) || e.Extension.Contains(searchText, stringComparison) || e.PhoneMain.Contains(searchText, stringComparison) || e.PhoneMobile.Contains(searchText, stringComparison)).ToList();
            }

            switch (currentSort)
            {
                case "First Name ASC":
                    output = output.OrderBy(e => e.FirstName).ToList();
                    break;
                case "First Name DESC":
                    output = output.OrderByDescending(e => e.FirstName).ToList();
                    break;
                case "Last Name ASC":
                    output = output.OrderBy(e => e.LastName).ToList();
                    break;
                case "Last Name DESC":
                    output = output.OrderByDescending(e => e.LastName).ToList();
                    break;
                case "Title ASC":
                    output = output.OrderBy(e => e.Title == null).ThenBy(e => e.Title != null ? e.Title.Name : null).ToList();
                    break;
                case "Title DESC":
                    output = output.OrderByDescending(e => e.Title == null).ThenByDescending(e => e.Title != null ? e.Title.Name : null).ToList();
                    break;
                case "Title Level ASC":
                    output = output.OrderBy(e => e.Title == null).ThenBy(e => e.Title != null ? e.Title.TitleLevel : 0).ToList();
                    break;
                case "Title Level DESC":
                    output = output.OrderByDescending(e => e.Title == null).ThenByDescending(e => e.Title != null ? e.Title.TitleLevel : 0).ToList();
                    break;
                case "Department ASC":
                    output = output.OrderBy(e => e.Department.Name).ToList();
                    break;
                case "Department DESC":
                    output = output.OrderByDescending(e => e.Department.Name).ToList();
                    break;
                default:
                    break;
            }

            employees = output;
            await SaveFilter();
        }

        private async Task OnSearchInput(string searchInput)
        {
            searchText = searchInput;
            await ApplyFilter();
        }

        private List<string> PopulateSortDropdownItems()
        {
            var output = new List<string>();
            output.Add("");
            output.Add("First Name ASC");
            output.Add("First Name DESC");
            output.Add("Last Name ASC");
            output.Add("Last Name DESC");
            output.Add("Title ASC");
            output.Add("Title DESC");
            output.Add("Title Level ASC");
            output.Add("Title Level DESC");
            output.Add("Department ASC");
            output.Add("Department DESC");
            return output;
        }

        private async Task OnDepartmentClick(string departmentName)
        {
            selectedDepartment = departmentName;
            await ApplyFilter();
        }

        private async Task OnTitleClick(string titleName)
        {
            selectedTitle = titleName;
            await ApplyFilter();
        }

        private async Task SortChange(ChangeEventArgs e)
        {
            currentSort = e.Value.ToString();
            await SaveFilter();
            await ApplyFilter();
            StateHasChanged();
        }

        private void AdminLogin()
        {
            navManager.NavigateTo("/Admin");
        }

        private void CreateNewEmployee()
        {
            navManager.NavigateTo("Create");
        }

        private void OpenEmployeeDetails(EmployeeModel employee)
        {
            navManager.NavigateTo($"/EmployeeDetails/{employee.Id}");
        }
    }
}