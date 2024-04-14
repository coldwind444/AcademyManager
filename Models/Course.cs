using Newtonsoft.Json;
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
        public Dictionary<string, Class> Classes { get; set; }
        public Course(string id, string name, int credits)
        {
            CourseID = id;
            CourseName = name;
            Credits = credits;
            Classes = new Dictionary<string, Class>();
        }
        [JsonConstructor]public Course(string courseID, string courseName, int credits, Dictionary<string, Class> classes) : this(courseID, courseName, credits)
        {
            Classes = classes;
        }
    }
}
