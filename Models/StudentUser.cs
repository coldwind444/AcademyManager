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
        public string Major {  get; set; }
        public async Task RegisterClass(string termID, string courseID, string classID)
        {
            DatabaseManager database = new DatabaseManager();
            Term term = await database.GetTermAsync(termID);
            StudyElements.Add(new ClassIdentifier(termID, courseID, classID));
            term.Courses[courseID].Classes[classID].Students[ID] = new StudentRecord(ID, 0, 0, 0, 0);
        }
        public async Task<List<StudentRecord>> ViewScore()
        {
            List<StudentRecord> result = new List<StudentRecord> ();
            DatabaseManager database = new DatabaseManager();
            Term term = null;
            foreach (ClassIdentifier cid in StudyElements)
            {
                if (term != null)
                    if (term.TermID != cid.TermID) term = await database.GetTermAsync(cid.TermID);
                else term = await database.GetTermAsync(cid.TermID);
                StudentRecord rc = term.Courses[cid.CourseID].Classes[cid.ClassID].Students[ID];
                result.Add(rc);
            }
            return result;
        }
        public StudentUser(string id, string fullname, string email, DateOnly birthday, string faculty, string major, double gpa = 0, int credits = 0, string state = null)
            : base(id, fullname, email, birthday, faculty)
        {
            AverageGPA = gpa;
            Credits = credits;
            State = state;
        }
    }
}
