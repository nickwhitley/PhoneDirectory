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
                _cache.Set(cacheName, employees, TimeSpan.FromMinutes(5));
                return employees;

            }
            

            return cacheOutput;
        }

        public async Task UpdateEmployeeAsync(EmployeeModel employee)
        {
            ///TODO - Make sure to update local employee model before reaching this point.
            string storedProc = "dbo.spEmployee_Update";

            await _database.SaveDataAsync(
                storedProc,
                employee
                //new
                //{
                //    @id = employee.Id,
                //    @firstName = employee.FirstName,
                //    @lastName = employee.LastName,
                //    @phoneMain = employee.PhoneMain,
                //    @phoneMobile = employee.PhoneMobile,
                //    @extension = employee.Extension,
                //    @notes = employee.Notes,
                //    @titleId = employee.Title.Id,
                //    @departmentId = employee.Department.Id,
                //    @supId = employee.Supervisor.Id
                //}
                );
        }

        public async Task DeleteEmployeeAsync(EmployeeModel employee)
        {
            string storedProc = "dbo.spEmployee_DeleteById";

            await _database.SaveDataAsync(storedProc, new { @id = employee.Id });
        }
    }
}
