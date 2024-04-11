using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyManager.Models
{
    public class InstructorUser : User
    {
        public string Certificate { get; set; }
        public string Speciality { get; set; }
        public async Task UpdateScore(string termID, string courseID, string classID, string studentID, int field, double score)
        {
            DatabaseManager database = new DatabaseManager();
            Term term = await database.GetTermAsync(termID);
            if (field == 1) term.Courses[courseID].Classes[classID].Students[studentID].DailyTestScore = score;
            else if (field == 2) term.Courses[courseID].Classes[classID].Students[studentID].Mid_Term = score;
            else term.Courses[courseID].Classes[classID].Students[studentID].Final = score;
            await database.UpdateTermAsync(term);
        }
        public InstructorUser(string id, string fullname, string email, DateOnly birthday, string faculty, string cert, string spec) 
            : base(id, fullname, email, birthday, faculty)
        {
            Certificate = cert;
            Speciality = spec;
        }
    }
}
