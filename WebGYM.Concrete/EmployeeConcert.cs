using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGYM.Interface;
using WebGYM.Models;
using WebGYM.ViewModels;
using System;

namespace WebGYM.Concrete
{
    public class EmployeeConcerte : IEmployee
    {
        private readonly DatabaseContext _context;
        public EmployeeConcerte(DatabaseContext context)
        {
            _context = context;
        }

        //public bool CheckEmployeeExits(string FirstName)
        //{
        //    throw new System.NotImplementedException();
        //}

        public bool CheckEmployeeExits(string firstname)
        {
            var result = (from employee in _context.EmployeeMaster
                          where employee.FirstName == firstname
                          select employee).Count();

            return result > 0 ? true : false;
        }

        public bool DeleteEmployee(int employeid)
        {
            throw new System.NotImplementedException();
        }

        public List<Employee> GetAllEmployees()
        {
            var result = (from employee in _context.EmployeeMaster
                          where employee.Status == true
                          select employee).ToList();

            return result;
        }

        public bool InsertEmployee(Employee employee)
        {
            _context.EmployeeMaster.Add(employee);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
