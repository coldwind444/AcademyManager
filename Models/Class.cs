using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyManager.Models
{
    public class Class
    {
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string InstructorID { get; set; }
        public List<KeyValuePair<string,string>> Documents { get; set; }
        public DayOfWeek Weekday { get; set; }
        public TimeOnly BeginTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly BeginDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Room {  get; set; }
        public Class(string classID, string className, string instructorID, List<KeyValuePair<string, string>> documents, DayOfWeek weekday, TimeOnly beginTime, TimeOnly endTime, DateOnly beginDate, DateOnly endDate, string room)
        {
            ClassID = classID;
            ClassName = className;
            InstructorID = instructorID;
            Documents = documents;
            Weekday = weekday;
            BeginTime = beginTime;
            EndTime = endTime;
            BeginDate = beginDate;
            EndDate = endDate;
            Room = room;
        }
        public Class(string classID, string className, string instructorID, DayOfWeek day, TimeOnly bgT, TimeOnly endTime, DateOnly beginDate, DateOnly endDate, string room)
        {
            ClassID = classID;
            ClassName = className;
            InstructorID = instructorID;
            Weekday = day;
            BeginTime = bgT;
            EndTime = endTime;
            BeginDate = beginDate;
            EndDate = endDate;
            Room = room;
        }
    }
}
