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
        public string Major {  get; set; }
        public async Task<bool> RegisterClass(string termID, string courseID, string classID, string uuid)
        {
            DatabaseManager database = new DatabaseManager();
            this.StudyElements.Add(new ClassIdentifier(termID, courseID, classID));
            bool success = await database.UpdateStudentAsync(uuid, this);
            if (!success) return false;
            await database.AddStudentAsync(termID, courseID, classID, ID, new StudentRecord(ID, Fullname, 0, 0, 0, 0, 0));
            return true;
        }
        public async Task<bool> CancelRegisterClass(string termID, string courseID, string classID, string uuid)
        {
            DatabaseManager db = new DatabaseManager();
            ClassIdentifier? ci = StudyElements.Find(c => c.TermID == termID && c.CourseID == courseID && c.ClassID == classID);
            if (ci != null) StudyElements.Remove(ci);
            bool success = await db.UpdateStudentAsync(uuid, this);
            if (!success) return false;
            await db.RemoveStudentAsync(termID, courseID, classID, ID);
            return true;
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
        public StudentUser(string id, string fullname, string email, DateOnly birthday, string faculty, string avt, string major, double gpa = 0, int credits = 0)
            : base(id, fullname, email, birthday, faculty, avt)
        {
            AverageGPA = gpa;
            Credits = credits;
            Major = major;
        }
    }
}
