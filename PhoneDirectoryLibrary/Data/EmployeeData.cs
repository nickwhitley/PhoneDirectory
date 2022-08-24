using Microsoft.Extensions.Caching.Memory;
using PhoneDirectoryLibrary.Data.Interfaces;
using PhoneDirectoryLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDirectoryLibrary.Data
{
    public class EmployeeData : IEmployeeData
    {
        private readonly ISQLDataAccess _database;
        private readonly IMemoryCache _cache;
        private const string cacheName = "EmployeeCache";

        public EmployeeData(ISQLDataAccess database, IMemoryCache cache)
        {
            _database = database;
            _cache = cache;
        }

        public async Task<List<EmployeeModel>> GetEmployeesAsync(IDataFactory dataFactory)
        {
            var cacheOutput = _cache.Get<List<EmployeeModel>>(cacheName);

            if(cacheOutput is null)
            {
                string storedProc = "dbo.spEmployee_GetAll";
                var departments = dataFactory.Departments;
                var titles = dataFactory.Titles;
                var employees = await _database.LoadDataAsync<EmployeeModel, dynamic>(
                    storedProc, new { }
                    );

                foreach (var employee in employees)
                {
                    if (employee.Notes is null)
                    {
                        employee.Notes = " ";
                    }
                    if (employee.TitleId != 0)
                    {
                        employee.Title = titles.Where(t => t.Id == employee.TitleId).FirstOrDefault();
                    }
                    if (employee.DepartmentId != 0)
                    {
                        employee.Department = departments.Where(d => d.Id == employee.DepartmentId).FirstOrDefault();
                    }
                    if (employee.SupId != 0)
                    {
                        employee.Supervisor = employees.Where(e => e.Id == employee.SupId).FirstOrDefault();
                    }

                }
                _cache.Set(cacheName, employees, TimeSpan.FromHours(4));
                return employees;

            }
            

            return cacheOutput;
        }

        public async Task UpdateEmployeeAsync(EmployeeModel employee)
        {
            ///TODO - Make sure to update local employee model before reaching this point.
            _cache.Remove(cacheName);

            string storedProc = "spEmployee_Update";

            await _database.SaveDataAsync(
                storedProc,
                new
                {
                    @Id = employee.Id,
                    employee.FirstName,
                    employee.LastName,
                    employee.PhoneMain,
                    employee.PhoneMobile,
                    employee.Extension,
                    employee.Notes,
                    employee.TitleId,
                    employee.DepartmentId,
                    employee.SupId,
                    employee.Email
                }
                );
        }

        public async Task AddEmployeeAsync(EmployeeModel employee)
        {
            _cache.Remove(cacheName);
            string storedProc = "spEmployee_Create";

            await _database.SaveDataAsync(
                storedProc,
                new
                {
                    employee.FirstName,
                    employee.LastName,
                    employee.PhoneMain,
                    employee.PhoneMobile,
                    employee.Extension,
                    employee.Notes,
                    employee.TitleId,
                    employee.DepartmentId,
                    employee.SupId,
                    employee.Email
                }
                );
        }

        public async Task DeleteEmployeeAsync(EmployeeModel employee)
        {
            string storedProc = "dbo.spEmployee_DeleteById";

            await _database.SaveDataAsync(storedProc, new { @id = employee.Id });
        }
    }
}
