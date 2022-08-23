using Proje_ve_Görev_Yönetim_Uygulaması___Dapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje_ve_Görev_Yönetim_Uygulaması___Dapper.Managers
{
    public class EmployeeManager
    {
        DataAccessManager _dataAccess = new DataAccessManager();
        string _tableName = "Employees";
        
        public List<Employee> List()
        {
            return _dataAccess.Select<Employee>(_tableName);
        }
        public bool Add(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.NameSurname?.Trim()))
                throw new Exception("Çalışan adı boş bırakılamaz.");

            string[] columnNames = new string[]
            {
                nameof(Employee.NameSurname),
                nameof(Employee.Email),
                nameof(Employee.Phone),
                nameof(Employee.UpdatedOn),
                nameof(Employee.CreatedOn)
            };
            return _dataAccess.Insert<Employee>(columnNames, _tableName, employee);
        }
        public bool Edit(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.NameSurname?.Trim()))
                throw new Exception("Çalışan adı boş bırakılamaz.");

            string[] columnNames = new string[]
            {
                nameof(Employee.NameSurname),
                nameof(Employee.Email),
                nameof(Employee.Phone),
                nameof(Employee.UpdatedOn),
                nameof(Employee.CreatedOn)
            };
            return _dataAccess.Update<Employee>(columnNames, _tableName, employee);
        }
        public bool Delete(int id)
        {
            return _dataAccess.Delete(_tableName, id);
        }
    }
}
