using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGYM.Models
{
    public class Student
    {
        [Key]
        public int SID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string Hobies { get; set; }
        public DateTime DOB { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        
    }

    public class City
    {
        [Key]
        public int CId { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
    }

    public class State
    {
        [Key]
        public int SId { get; set; }
        public string Name { get; set; }
    }
}
