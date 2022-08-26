

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
        private bool employeeDeleted = false;
        private string deleteButtonText;
        private bool validId;

        //employe property changes
        private string ogFirstName;
        private string ogLastName;
        private string ogPhoneMain;
        private string ogExtension;
        private string ogPhoneMobile;
        private string ogNotes;
        private int ogTitleId;
        private int ogDepartmentId;
        private int ogSupId;
        private string ogEmail;
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

            //set original employee data
            ogFirstName = employee.FirstName;
            ogLastName = employee.LastName;
            ogPhoneMain = employee.PhoneMain;
            ogPhoneMobile = employee.PhoneMobile;
            ogExtension = employee.Extension;
            ogNotes = employee.Notes;
            ogTitleId = employee.TitleId;
            ogDepartmentId = employee.DepartmentId;
            ogSupId = employee.SupId;
            ogEmail = employee.Email;


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
            changesMade = InfoHasChanged();
            changesSaved = false;
            submitButtonText = "submit";
            PopulateAvailableSupervisors();
        }

        private bool InfoHasChanged()
        {
            if (ogFirstName != employee.FirstName ||
                ogLastName != employee.LastName ||
                ogPhoneMain != employee.PhoneMain ||
                ogExtension != employee.Extension ||
                ogPhoneMobile != employee.PhoneMobile ||
                ogNotes != employee.Notes ||
                ogDepartmentId != employee.DepartmentId ||
                ogTitleId != employee.TitleId ||
                ogSupId != employee.SupId ||
                ogEmail != employee.Email)
            {
                return true;
            } else
            {
                return false;
            }
        }


        private void SaveEmployeeForm()
        {
            changesSaved = true;
            changesMade = false;
            submitButtonText = "Changes Saved";
            dataFactory.UpdateEmployeeAsync(employee);
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
                deleteButtonText = "Employee Deleted!";
                employeeDeleted = true;
                await dataFactory.DeleteEmployeeAsync(employee);
                await Task.Delay(2500);
                await OpenDirectory();
            }
        }

        private void ResetEmployee()
        {
            employee = dataFactory.Employees.First(e => e.Id == employee.Id);
        }

        private async Task OpenDirectory()
        {
            if(!changesSaved){
                await dataFactory.ClearCache();
                await dataFactory.PopulateDataAsync();
                if (!employeeDeleted)
                {
                    ResetEmployee();
                }
            }
            
            navManager.NavigateTo("/");
        }
    }
}