namespace PhoneDirectoryLibrary.Data.Interfaces
{
    public interface IEmployeeData
    {
        Task DeleteEmployeeAsync(EmployeeModel employee);
        Task<List<EmployeeModel>> GetEmployeesAsync(IDataFactory dataFactory);
        Task UpdateEmployeeAsync(EmployeeModel employee);
    }
}