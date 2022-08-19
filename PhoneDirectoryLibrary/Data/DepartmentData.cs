using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache;
        private const string cacheName = "DepartmentCache";

        public DepartmentData(ISQLDataAccess database, IMemoryCache cache)
        {
            _database = database;
            _cache = cache;
        }

        public async Task<List<DepartmentModel>> GetDepartmentsAsync()
        {
            var cacheOutput = _cache.Get<List<DepartmentModel>>(cacheName);

            if(cacheOutput is null)
            {
                string storedProc = "dbo.spDepartment_GetAll";

                var departments = await _database.LoadDataAsync<DepartmentModel, dynamic>(
                    storedProc, new { }
                    );

                _cache.Set(cacheName, departments, TimeSpan.FromHours(4));
                return departments;
            }

            return cacheOutput;
            
        }

        public async Task UpdateDepartmentAsync(DepartmentModel department)
        {
            _cache.Remove(cacheName);
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
