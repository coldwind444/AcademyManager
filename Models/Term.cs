using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyManager.Models
{
    public class Term
    {
        public string TermID { get; set; }
        public List<Course> Courses { get; set; }
        public Term(string id)
        {
            TermID = id;
            Courses = new List<Course>();
        }
    }
}
