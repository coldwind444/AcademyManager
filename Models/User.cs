using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyManager.Models
{
    public class User
    {
        public string ID { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateOnly Birthday { get; set; }
        public string Faculty {  get; set; }
        public string Major { get; set; }
        public string AvatarBase64 { get; set; }
        public List<ClassIdentifier> StudyElements { get; set; }
        private bool InSchedule(DateOnly date, Class cls)
        {
            bool inPeriod = cls.BeginDate <= date && cls.EndDate >= date;
            bool inDate = cls.Weekday == date.DayOfWeek;
            return inPeriod && inDate;
        }
        public async Task<List<Class>> GetSchedule(DateOnly date)
        {
            var list = new List<Class>();
            DatabaseManager dbManager = new DatabaseManager();
            Term term = null;
            foreach (ClassIdentifier cid in StudyElements)
            {
                if (term != null) if (term.TermID != cid.TermID) term = await dbManager.GetTermAsync(term.TermID);
                else term = await dbManager.GetTermAsync(term.TermID);
                Dictionary<string,Class> classList = term.Courses[cid.CourseID].Classes;
                foreach (KeyValuePair<string,Class> _class in classList)
                {
                    if (_class.Key == cid.ClassID && InSchedule(date, _class.Value))
                    {
                        list.Add(_class.Value);
                        break;
                    }
                }
            }
            return list;
        }
        public User(string id, string fullname, string email, DateOnly birthday, string faculty)
        {
            ID = id;
            Fullname = fullname;
            Email = email;
            Birthday = birthday;
            Faculty = faculty;
            StudyElements = new List<ClassIdentifier>();
        }
    }
}
