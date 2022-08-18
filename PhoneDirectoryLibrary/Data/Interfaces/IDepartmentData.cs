namespace PhoneDirectoryLibrary.Data.Interfaces
{
    public interface IDepartmentData
    {
        Task<List<DepartmentModel>> GetDepartmentsAsync();
        Task UpdateDepartmentAsync(DepartmentModel department);
    }
}