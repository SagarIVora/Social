using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGYM.ViewModels
{
    public class GymMemberViewModel
    {
        public int GYMID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string EmailId { get; set; }
        public DateTime DOB { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string JoinReason { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public List<GymWorkOutViewModel> WorkoutList { get; set; }
    }
    public class GymMemberDisplayViewModel
    {
        public int GYMID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string EmailId { get; set; }
        public DateTime DOB { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string JoinReason { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }

    public class GymWorkOutViewModel
    {
        public int GYMWID { get; set; }
        public int? GymMermberId { get; set; }
        public int? WorkOutId { get; set; }
        public string SetName { get; set; }
        public int? RoundNo { get; set; }
        public bool? Status { get; set; }
        public DateTime WorkTime { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
    public class WorkOutTypeViewModel
    {
        public int WTID { get; set; }
        public string Name { get; set; }
    }
}
