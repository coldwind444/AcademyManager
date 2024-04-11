using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyManager.Models
{
    public class ClassIdentifier
    {
        public string TermID { get; set; }
        public string CourseID { get; set; }
        public string ClassID { get; set; }
        public ClassIdentifier(string termid, string courseid, string classid)
        {
            TermID = termid;
            CourseID = courseid;
            ClassID = classid;
        }
    }
}
