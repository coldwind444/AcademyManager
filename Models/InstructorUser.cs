namespace AcademyManager.Models
{
    public class InstructorUser : User
    {
        public string Certificate { get; set; }
        public string Speciality { get; set; }
        public async Task<bool> UpdateScore(string termID, string courseID, string classID, Dictionary<string, StudentRecord> score)
        {
            DatabaseManager database = new DatabaseManager();
            bool success = await database.UpdateScoreAsync(termID, courseID, classID, score);
            if (success) return true;
            return false;
        }
        public InstructorUser(string id, string fullname, string email, DateOnly birthday, string faculty, string avt, string cert, string spec)
            : base(id, fullname, email, birthday, faculty, avt)
        {
            Certificate = cert;
            Speciality = spec;
        }
    }
}
