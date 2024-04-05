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
        public void RegisterClass(string termID, string courseID, string classID)
        {

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
