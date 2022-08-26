using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneDirectoryLibrary.Data.Interfaces;

namespace PhoneDirectoryLibrary.Data
{
    public class DataFactory : IDataFactory
    {
        public List<EmployeeModel> Employees { get; set; }
        public List<DepartmentModel> Departments { get; set; }
        public List<TitleModel> Titles { get; set; }

        private readonly IEmployeeData _employeeData;
        private readonly IDepartmentData _departmentData;
        private readonly ITitleData _titleData;


        public DataFactory(IEmployeeData employeeData,
            IDepartmentData departmentData, ITitleData titleData)
        {
            _employeeData = employeeData;
            _departmentData = departmentData;
            _titleData = titleData;
        }

        public async Task PopulateDataAsync()
        {
            Titles = await _titleData.GetTitlesAsync();
            Departments = await _departmentData.GetDepartmentsAsync();
            Employees = await _employeeData.GetEmployeesAsync(this);
        }

        public async Task UpdateEmployeeAsync(EmployeeModel employee)
        {
            await _employeeData.UpdateEmployeeAsync(employee);
        }

        public async Task DeleteEmployeeAsync(EmployeeModel employee)
        {
            await _employeeData.DeleteEmployeeAsync(employee);
        }

        public async Task AddEmployeeAsync(EmployeeModel employee)
        {
            await _employeeData.AddEmployeeAsync(employee);
        }

        public async Task AddDepartment(DepartmentModel department)
        {
            await _departmentData.AddDepartmentAsync(department);
        }

        public List<EmployeeModel> GetAvailableSupervisors(int titleId, int departmentId)
        {
            var output = Employees;
            var titleLevel = Titles.Where(t => t.Id == titleId).FirstOrDefault().TitleLevel;

            if (titleLevel > 4)
            {

                output = (List<EmployeeModel>)output.Where(e => e.DepartmentId == departmentId)
                                                     .Where(e => e.Title?.TitleLevel < titleLevel).ToList();
            } else
            {
                output = (List<EmployeeModel>)output.Where(e => e.DepartmentId == departmentId)
                                                    .Where(e => e.Title?.TitleLevel == (titleLevel - 1)).ToList();
            }

            return output;
        }

        public void ClearCache()
        {
            _employeeData.ClearCache();
            _titleData.ClearCache();
            _departmentData.ClearCache();
        }
    }
}
