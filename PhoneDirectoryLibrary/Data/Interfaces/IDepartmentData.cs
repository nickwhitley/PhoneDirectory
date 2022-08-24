namespace PhoneDirectoryLibrary.Data.Interfaces
{
    public interface IDepartmentData
    {
        Task AddDepartmentAsync(DepartmentModel department);
        Task<List<DepartmentModel>> GetDepartmentsAsync();
        Task UpdateDepartmentAsync(DepartmentModel department);
    }
}