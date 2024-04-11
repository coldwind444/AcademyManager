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
        public Dictionary<string, Course> Courses { get; set; }
        public Term(string id)
        {
            TermID = id;
            Courses = new Dictionary<string, Course>();
        }
    }
}
