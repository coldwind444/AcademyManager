using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyManager.Models
{
    public class Course
    {
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public List<Class> Classes { get; set; }
        public Course(string id, string name)
        {
            CourseID = id;
            CourseName = name;
            Classes = new List<Class>();
        }
    }
}
