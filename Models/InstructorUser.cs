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
        public void CreateClass(string termID, string courseID, string id, string name, TimeOnly bgT, TimeOnly eT, DateOnly bgD, DateOnly eD, string room)
        {

        }
        public void UpdateScore(string termID, string courseID, string classID, string studentID, int field, double score)
        {

        }
        public InstructorUser(string id, string fullname, string email, DateOnly birthday, string faculty, string cert, string spec) 
            : base(id, fullname, email, birthday, faculty)
        {
            Certificate = cert;
            Speciality = spec;
        }
    }
}
