﻿using System;
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
        public List<Term> Terms { get; set; }
        public List<Class> GetSchedule(DateOnly date)
        {
            var list = new List<Class>();
            return list;
        }
        public User(string id, string fullname, string email, DateOnly birthday, string faculty)
        {
            ID = id;
            Fullname = fullname;
            Email = email;
            Birthday = birthday;
            Faculty = faculty;
            Terms = new List<Term>();
        }
    }
}