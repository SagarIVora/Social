using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebGYM.Interface;
using WebGYM.ViewModels;

namespace WebGYM.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StateCityController : ControllerBase
    {
        private readonly IStudent _student;
        public StateCityController(IStudent Student)
        {
            _student = Student;
        }
        // GET: api/Student
        [HttpGet]
      //  [Route("StateList")]
        public IEnumerable<StateViewModel> Get()
        {
            return _student.GetStateList();
        }

        [HttpGet("{sid}")]
       // [Route("CityList/{cityId}")]
        public IEnumerable<CityViewModel> Get(int sid)
        {
            return _student.GetCityList(sid);
        }
    }
}