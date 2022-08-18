using PhoneDirectoryLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDirectoryLibrary.Data
{
    public class EmployeeData
    {
        private readonly ISQLDataAccess _database;

        public EmployeeData(ISQLDataAccess database)
        {
            _database = database;
        }

        public Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            string storedProc = "dbo.spEmployee_GetAll";
            return _database.LoadDataAsync<EmployeeModel, dynamic>(
                storedProc, new { }
                );
        }

        public async Task UpdateEmployeeAsync(EmployeeModel employee)
        {
            ///TODO - Make sure to update local employee model before reaching this point.
            string storedProc = "dbo.spEmployee_Update";

            await _database.SaveDataAsync(
                storedProc,
                new
                {
                    @id = employee.Id,
                    @firstName = employee.FirstName,
                    @lastName = employee.LastName,
                    @phoneMain = employee.PhoneMain,
                    @phoneMobile = employee.PhoneMobile,
                    @extension = employee.Extension,
                    @notes = employee.Notes,
                    @titleId = employee.Title.Id,
                    @departmentId = employee.Department.Id,
                    @supId = employee.Supervisor.Id
                }
                );
        }
    }
}
