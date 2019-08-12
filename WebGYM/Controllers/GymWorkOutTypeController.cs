using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebGYM.Interface;
using WebGYM.ViewModels;

namespace WebGYM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymWorkOutTypeController : ControllerBase
    {
        private readonly IGymMember _student;
        public GymWorkOutTypeController(IGymMember Student)
        {
            _student = Student;
        }
        // GET: api/GymMember
        [HttpGet]
        //  [Route("WorkOutList")]
        public IEnumerable<WorkOutTypeViewModel> Get()
        {
            return _student.GetWorkOutList();
        }
    }
}