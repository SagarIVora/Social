using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
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
    public class GymMemberController : ControllerBase
    {
        private readonly IGymMember _gymMember;
        public GymMemberController(IGymMember GymMember)
        {
            _gymMember = GymMember;
        }
        // GET: api/GymMember
        [HttpGet]
        public IEnumerable<GymMemberDisplayViewModel> get()
        {
            return _gymMember.GetGymMemberList();
        }


        // GET: api/GymMember/5
        //[HttpGet("{id}", Name = "GetGymMember")]
        //public GymMemberViewModel Get(int id)
        //{
        //    try
        //    {
        //        return _gymMember.GetGymMemberbyId(id);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        // POST: api/GymMember
        [HttpPost]
        public HttpResponseMessage Post([FromBody] GymMemberViewModel GymMemberViewModel)
        {
            try
            {
                if (_gymMember.CheckGymMemberExits(GymMemberViewModel.FirstName))
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
                    var tempGymMember = AutoMapper.Mapper.Map<TempGymMembers>(GymMemberViewModel);
                    tempGymMember.CreatedBy = Convert.ToInt32(userId);
                    _gymMember.InsertGymMember(tempGymMember);

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


        // PUT: api/GymMember/5
        //[HttpPut("{id}")]
        //public HttpResponseMessage Put(int id, [FromBody] GymMemberViewModel GymMemberViewModel)
        //{
        //    try
        //    {
        //        var gymMemberId = this.User.FindFirstValue(ClaimTypes.Name);
        //        var tempGymMember = AutoMapper.Mapper.Map<GymMember>(GymMemberViewModel);
        //        tempGymMember.CreatedBy = Convert.ToInt32(id);
        //        _gymMember.UpdateGymMember(tempGymMember);
        //        var response = new HttpResponseMessage()
        //        {
        //            StatusCode = HttpStatusCode.OK
        //        };

        //        return response;
        //    }
        //    catch (Exception)
        //    {
        //        var response = new HttpResponseMessage()
        //        {
        //            StatusCode = HttpStatusCode.InternalServerError
        //        };
        //        return response;
        //    }
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public HttpResponseMessage Delete(int id)
        //{
        //    try
        //    {

        //        _gymMember.DeleteGymMember(id);
        //        var response = new HttpResponseMessage()
        //        {
        //            StatusCode = HttpStatusCode.OK
        //        };

        //        return response;
        //    }
        //    catch (Exception)
        //    {
        //        var response = new HttpResponseMessage()
        //        {
        //            StatusCode = HttpStatusCode.InternalServerError
        //        };
        //        return response;
        //    }
        //}
    }
}