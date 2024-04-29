using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyManager.Models
{
    public class Notification
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime UpdateDate { get; set; }
        public Notification(string title, string message, DateTime updateDate)
        {
            Title = title;
            Message = message;
            UpdateDate = updateDate;
        }
    }
}
