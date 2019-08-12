using System.Collections.Generic;
using WebGYM.Models;
using WebGYM.ViewModels;

namespace WebGYM.Interface
{
    public interface IEmployee
    {

        bool InsertEmployee(Employee employee);
        bool CheckEmployeeExits(string firstname);
        bool DeleteEmployee(int employeid);
        List<Employee> GetAllEmployees();
    }
}
