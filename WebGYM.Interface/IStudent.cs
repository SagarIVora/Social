using System.Collections.Generic;
using WebGYM.Models;
using WebGYM.ViewModels;

namespace WebGYM.Interface
{
    public interface IStudent
    {
        void InsertStudent(Student stud);
        bool CheckStudentExits(string Name);
        List<StudentDisplayViewModel> GetStudentList();
        StudentViewModel GetStudentbyId(int studId);
        bool DeleteStudent(int studId);
        void UpdateStudent(Student student);
        List<StateViewModel> GetStateList();
        List<CityViewModel> GetCityList(int sid);
    }
}
