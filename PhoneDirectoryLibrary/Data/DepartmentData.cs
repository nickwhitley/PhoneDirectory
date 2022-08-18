using PhoneDirectoryLibrary.Data.Interfaces;
using PhoneDirectoryLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDirectoryLibrary.Data
{
    public class DepartmentData : IDepartmentData
    {
        private readonly ISQLDataAccess _database;

        public DepartmentData(ISQLDataAccess database)
        {
            _database = database;
        }

        public Task<List<DepartmentModel>> GetDepartmentsAsync()
        {
            string storedProc = "dbo.spDepartment_GetAll";

            return _database.LoadDataAsync<DepartmentModel, dynamic>(
                storedProc, new { }
                );
        }

        public async Task UpdateDepartmentAsync(DepartmentModel department)
        {
            string storedProc = "dbo.spDepartment_Update";

            await _database.SaveDataAsync(
                storedProc,
                new
                {
                    @id = department.Id,
                    @name = department.Name
                });
        }
    }
}
