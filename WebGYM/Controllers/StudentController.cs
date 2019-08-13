using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebGYM.Interface;
using WebGYM.Models;
using WebGYM.ViewModels;

namespace WebGYM.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _student;
        public StudentController(IStudent Student)
        {
            _student = Student;
        }
        // GET: api/Student
        [HttpGet]
        public IEnumerable<StudentDisplayViewModel> Get()
        {
            return _student.GetStudentList();
        }
        
        // GET: api/Student/5
        [HttpGet("{id}", Name = "GetStudent")]
        public StudentViewModel Get(int id)
        {
            try
            {
                return _student.GetStudentbyId(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: api/Student
        [HttpPost]
        public HttpResponseMessage Post([FromBody] StudentViewModel StudentViewModel)
        {
            try
            {
                if (_student.CheckStudentExits(StudentViewModel.Name))
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
                    var tempStudent = AutoMapper.Mapper.Map<Student>(StudentViewModel);
                      tempStudent.CreatedBy = Convert.ToInt32(userId);
                    _student.InsertStudent(tempStudent);

                    var response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    };

                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
                return response;
            }
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public HttpResponseMessage Put(int id, [FromBody] StudentViewModel StudentViewModel)
        {
            try
            {
                var studentId = this.User.FindFirstValue(ClaimTypes.Name);
                var tempStudent = AutoMapper.Mapper.Map<Student>(StudentViewModel);
                 tempStudent.CreatedBy = Convert.ToInt32(id);
                _student.UpdateStudent(tempStudent);
                var response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK
                };

                return response;
            }
            catch (Exception)
            {
                var response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
                return response;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {

                _student.DeleteStudent(id);
                var response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK
                };

                return response;
            }
            catch (Exception)
            {
                var response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
                return response;
            }
        }

    }
}