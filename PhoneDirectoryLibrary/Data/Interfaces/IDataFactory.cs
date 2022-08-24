namespace PhoneDirectoryLibrary.Data.Interfaces
{
    public interface IDataFactory
    {
        List<DepartmentModel> Departments { get; set; }
        List<EmployeeModel> Employees { get; set; }
        List<TitleModel> Titles { get; set; }

        Task PopulateDataAsync();

        Task UpdateEmployeeAsync(EmployeeModel employee);

        List<EmployeeModel> GetAvailableSupervisors(int titleId, int departmentId);
        Task AddEmployeeAsync(EmployeeModel employee);
        Task AddDepartment(DepartmentModel department);
    }
}