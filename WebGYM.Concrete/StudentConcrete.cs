using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using WebGYM.Interface;
using WebGYM.Models;
using WebGYM.ViewModels;

namespace WebGYM.Concrete
{
    public class StudentConcrete : IStudent
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;
        public StudentConcrete(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }


        public void InsertStudent(Student stud)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@SID", stud.SID);
                paramater.Add("@Name", stud.Name);
                paramater.Add("@Contact", stud.Contact);
                paramater.Add("@Address", stud.Address);
                paramater.Add("@Gender", stud.Gender);
                paramater.Add("@CityId", stud.CityId);
                paramater.Add("@StateId", stud.StateId);
                paramater.Add("@Hobies", stud.Hobies);
                paramater.Add("@DOB", stud.DOB);
                paramater.Add("@CreatedBy", stud.CreatedBy);
                paramater.Add("@CreatedOn", stud.CreatedOn);

                var value = con.Query<int>("sprocStudentInsertUpdateSingleItem", paramater, null, true, 0, commandType: CommandType.StoredProcedure);
            }
        }

        public bool CheckStudentExits(string Name)
        {
            var result = (from student in _context.Student
                          where student.Name == Name
                          select student).Count();

            return result > 0 ? true : false;
        }

        public List<StudentDisplayViewModel> GetStudentList()
        {
            var result = (from student in _context.Student
                          join state in _context.State on student.StateId equals state.SId
                          join city in _context.City on student.CityId equals city.CId
                          select new StudentDisplayViewModel
                          {
                              SID = student.SID,
                              Name = student.Name,
                              Contact = student.Contact,
                              Address = student.Address,
                              Gender = student.Gender,
                              CityId = student.CityId,
                              StateId = student.StateId,
                              CityName = city.Name,
                              StateName = state.Name,
                              Hobies = student.Hobies,
                              DOB = student.DOB
                          }).ToList();

            return result;
        }

        public void UpdateStudent(Student student)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
                {
                    var paramater = new DynamicParameters();
                    paramater.Add("@SID", student.SID);
                    paramater.Add("@Name", student.Name);
                    paramater.Add("@Contact", student.Contact);
                    paramater.Add("@Address", student.Address);
                    paramater.Add("@Gender", student.Gender);
                    paramater.Add("@CityId", student.CityId);
                    paramater.Add("@StateId", student.StateId);
                    paramater.Add("@Hobies", student.Hobies);
                    paramater.Add("@DOB", student.DOB);
                    paramater.Add("@CreatedBy", student.CreatedBy);
                    paramater.Add("@CreatedOn", student.CreatedOn);

                    var value = con.Query<int>("sprocStudentInsertUpdateSingleItem", paramater, null, true, 0, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public bool DeleteStudent(int studId)
        {
            var student = (from stud in _context.Student
                           where stud.SID == studId
                           select stud).FirstOrDefault();
            if (student != null)
            {
                _context.Student.Remove(student);
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
            else
            {
                return false;
            }
        }

        public StudentViewModel GetStudentbyId(int studId)
        {
            var result = (from stud in _context.Student
                          where stud.SID == studId
                          select new StudentViewModel
                          {
                              SID = stud.SID,
                              Name = stud.Name,
                              Contact = stud.Contact,
                              Address = stud.Address,
                              Gender = stud.Gender,
                              CityId = stud.CityId,
                              StateId = stud.StateId,
                              Hobies = stud.Hobies,
                              DOB = stud.DOB,
                          }).FirstOrDefault();

            return result;
        }
        public List<StateViewModel> GetStateList()
        {
            var result = (from state in _context.State
                          select new StateViewModel
                          {
                              SId = state.SId,
                              Name = state.Name,
                          }).ToList();

            return result;
        }

        public List<CityViewModel> GetCityList(int sid)
        {
            var result = (from city in _context.City
                          where city.StateId == sid
                          select new CityViewModel
                          {
                              CId = city.CId,
                              Name = city.Name,
                              StateId = city.StateId,

                          }).ToList();

            return result;
        }
    }
}
