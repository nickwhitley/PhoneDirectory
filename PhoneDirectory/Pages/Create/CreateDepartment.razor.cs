using PhoneDirectory.Models;

namespace PhoneDirectory.Pages.Create
{
    public partial class CreateDepartment
    {
        private NewDepartmentModel newDepartment = new NewDepartmentModel();
        private string departmentName;
        private string submitButtonText;
        protected override async Task OnInitializedAsync()
        {
            submitButtonText = "Create Department";
            await base.OnInitializedAsync();
        }

        private async Task SaveNewDepartment()
        {
            var departmentConvert = new DepartmentModel()
            {Name = newDepartment.Name};
            submitButtonText = "Department Created!";
            await Task.Delay(3500);
            //await dataFactory.AddDepartment(departmentConvert);
            OpenDirectory();
        }

        private void OpenDirectory()
        {
            navManager.NavigateTo("/");
        }
    }
}