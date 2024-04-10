using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyManager.Models
{
    public class StudentUser : User
    {
        public double AverageGPA { get; set; }
        public int Credits { get; set; }
        public string State { get; set; }
        public async Task RegisterClass(string termID, string courseID, string classID)
        {
            DatabaseManager database = new DatabaseManager();
            Term term = await database.GetTermAsync(termID);
            bool flag = false;
            foreach (Course course in term.Courses)
            {
                if (course.CourseID == courseID)
                {
                    flag = true;
                    foreach (Class _class in course.Classes)
                    {
                        if (_class.ClassID == classID)
                        {
                            _class.Students.Add(new StudentRecord(this.ID, 0, 0, 0, 0));
                            TermSupportElement ele = new TermSupportElement(termID, courseID, classID);
                            if (!StudyElements.Contains(ele)) StudyElements.Add(ele);
                            break;
                        }
                    }
                }
                if (flag) break;
            }
        }
        public List<StudentRecord> ViewScore(string termID, string courseID, string classID)
        {
            
            return new List<StudentRecord>();
        }
        public StudentUser(string id, string fullname, string email, DateOnly birthday, string faculty, double gpa, int credits, string state)
            : base(id, fullname, email, birthday, faculty)
        {
            AverageGPA = gpa;
            Credits = credits;
            State = state;
        }
    }
}
