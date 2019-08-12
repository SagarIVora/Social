using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebGYM.Common;
using WebGYM.Interface;
using WebGYM.Models;
using WebGYM.ViewModels;

namespace WebGYM.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;
        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _employee.GetAllEmployees();
        }

        // POST: api/Employee
        [HttpPost]
        public HttpResponseMessage Post([FromBody] EmployeeViewModels employee)
        {
            if (ModelState.IsValid)
            {
                if (_employee.CheckEmployeeExits(employee.FirstName))
                {
                    var response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.Conflict
                    };

                    return response;
                }
                else
                {
                    var userId = this.User.FindFirstValue(ClaimTypes.Name);
                    var tempEmployee = AutoMapper.Mapper.Map<Employee>(employee);
                    tempEmployee.FirstName = Convert.ToString("FirstName");
                    tempEmployee.LastName = Convert.ToString("LastName");
                    tempEmployee.Contact = Convert.ToString("Contact");
                    tempEmployee.Gender = Convert.ToString("Gender");
                    tempEmployee.CreatedDate = DateTime.Now;
                    tempEmployee.Createdby = Convert.ToInt32(userId);
                    _employee.InsertEmployee(tempEmployee);

                    var response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    };

                    return response;
                }
            }
            else
            {
                var response = new HttpResponseMessage()
                {

                    StatusCode = HttpStatusCode.BadRequest
                };

                return response;
            }

        }
    }
}