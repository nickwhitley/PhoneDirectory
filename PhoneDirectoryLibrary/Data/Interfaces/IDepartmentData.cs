namespace PhoneDirectoryLibrary.Data.Interfaces
{
    public interface IDepartmentData
    {
        Task AddDepartmentAsync(DepartmentModel department);
        void ClearCache();
        Task<List<DepartmentModel>> GetDepartmentsAsync();
        Task UpdateDepartmentAsync(DepartmentModel department);
    }
}