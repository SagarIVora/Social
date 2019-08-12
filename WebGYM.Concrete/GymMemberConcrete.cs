using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using WebGYM.Interface;
using WebGYM.Models;
using WebGYM.ViewModels;
namespace WebGYM.Concrete
{
    public class GymMemberConcrete : IGymMember
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;

        public GymMemberConcrete(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        public void InsertGymMember(TempGymMembers gymMember)
        {
            DataTable dataListToTable = new DataTable();
            dataListToTable = ToDataTable(gymMember.WorkoutList);
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                var parameter1 = new SqlParameter("@WorkData", SqlDbType.Structured);
                parameter1.TypeName = "UTGymWorkout";
                parameter1.Value = dataListToTable;
                parameter1.SqlValue = dataListToTable;

                var paramater = new DynamicParameters();
                paramater.Add("@GYMID", gymMember.GYMID);
                paramater.Add("@FirstName", gymMember.FirstName);
                paramater.Add("@LastName", gymMember.LastName);
                paramater.Add("@ContactNo", gymMember.ContactNo);
                paramater.Add("@Address", gymMember.Address);
                paramater.Add("@EmailId", gymMember.EmailId);
                paramater.Add("@DOB", gymMember.DOB);
                paramater.Add("@Age", gymMember.Age);
                paramater.Add("@Gender", gymMember.Gender);
                paramater.Add("@JoinReason", gymMember.JoinReason);
                paramater.Add("@CreatedBy", gymMember.CreatedBy);
                paramater.Add("@WorkData", dataListToTable, DbType.Object);
                var value = con.Query<int>("TempInsertGyamMembersDetails", paramater, null, true, 0, commandType: CommandType.StoredProcedure);
            }
        }

        public bool CheckGymMemberExits(string Name)
        {
            var result = (from TempGymMembers in _context.TempGymMembers
                          where TempGymMembers.FirstName == Name
                          select TempGymMembers).Count();

            return result > 0 ? true : false;
        }


        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


        public List<GymMemberDisplayViewModel> GetGymMemberList()
        {
            var result = (from TempGymMembers in _context.TempGymMembers
                          select new GymMemberDisplayViewModel
                          {
                              GYMID = TempGymMembers.GYMID,
                              FirstName = TempGymMembers.FirstName,
                              LastName = TempGymMembers.LastName,
                              ContactNo = TempGymMembers.ContactNo,
                              Address = TempGymMembers.Address,
                              EmailId = TempGymMembers.EmailId,
                              DOB = TempGymMembers.DOB,
                              Age = TempGymMembers.Age,
                              Gender = TempGymMembers.Gender,
                          }).ToList();

            return result;
        }

        //public void UpdateStudent(Student student)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
        //        {
        //            var paramater = new DynamicParameters();
        //            paramater.Add("@SID", student.SID);
        //            paramater.Add("@Name", student.Name);
        //            paramater.Add("@Contact", student.Contact);
        //            paramater.Add("@Address", student.Address);
        //            paramater.Add("@Gender", student.Gender);
        //            paramater.Add("@CityId", student.CityId);
        //            paramater.Add("@StateId", student.StateId);
        //            paramater.Add("@Hobies", student.Hobies);
        //            paramater.Add("@DOB", student.DOB);
        //            paramater.Add("@CreatedBy", student.CreatedBy);
        //            paramater.Add("@CreatedOn", student.CreatedOn);

        //            var value = con.Query<int>("sprocStudentInsertUpdateSingleItem", paramater, null, true, 0, commandType: CommandType.StoredProcedure);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //public bool DeleteStudent(int studId)
        //{
        //    var student = (from stud in _context.Student
        //                   where stud.SID == studId
        //                   select stud).FirstOrDefault();
        //    if (student != null)
        //    {
        //        _context.Student.Remove(student);
        //        var result = _context.SaveChanges();

        //        if (result > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public StudentViewModel GetStudentbyId(int studId)
        //{
        //    var result = (from stud in _context.Student
        //                  where stud.SID == studId
        //                  select new StudentViewModel
        //                  {
        //                      SID = stud.SID,
        //                      Name = stud.Name,
        //                      Contact = stud.Contact,
        //                      Address = stud.Address,
        //                      Gender = stud.Gender,
        //                      CityId = stud.CityId,
        //                      StateId = stud.StateId,
        //                      Hobies = stud.Hobies,
        //                      DOB = stud.DOB,
        //                  }).FirstOrDefault();

        //    return result;
        //}

        public List<WorkOutTypeViewModel> GetWorkOutList()
        {
            var result = (from TempWorkOutType in _context.TempWorkOutType
                          select new WorkOutTypeViewModel
                          {
                              WTID = TempWorkOutType.WTID,
                              Name = TempWorkOutType.Name,
                          }).ToList();
            return result;
        }
    }
}
