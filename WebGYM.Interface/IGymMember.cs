using System.Collections.Generic;
using WebGYM.Models;
using WebGYM.ViewModels;

namespace WebGYM.Interface
{
    public interface IGymMember
    {
        void InsertGymMember(TempGymMembers gymMember);
        bool CheckGymMemberExits(string Name);
        List<GymMemberDisplayViewModel> GetGymMemberList();
        List<WorkOutTypeViewModel> GetWorkOutList();
        //        GymMemberViewModel GetGymMemberbyId(int gymId);
        //        bool DeleteGymMember(int studId);
        //        void UpdateGymMember(GymMember gymMember);
        //       List<CityViewModel> GetCityList(int sid);
    }
}
