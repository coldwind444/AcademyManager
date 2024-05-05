namespace AcademyManager.Models
{
    public class StudentUser : User
    {
        public double AverageGPA { get; set; }
        public int Credits { get; set; }
        public string Major { get; set; }
        public async Task<bool> RegisterClass(string termID, string courseID, string classID, string uuid)
        {
            bool isregistered = this.StudyElements.Any(c => c.TermID == termID && c.CourseID == courseID);
            if (isregistered) return false;
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
        public StudentUser(string id, string fullname, string email, DateOnly birthday, string faculty, string avt, string major, double gpa = 0, int credits = 0)
            : base(id, fullname, email, birthday, faculty, avt)
        {
            AverageGPA = gpa;
            Credits = credits;
            Major = major;
        }
    }
}
