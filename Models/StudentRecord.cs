﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyManager.Models
{
    public class StudentRecord
    {
        public double DailyTestScore { get; set; }
        public double Mid_Term { get; set; }
        public double Final {  get; set; }
        public double GPA { get; set; }
        public StudentRecord(string studentID, double dailyTestScore, double mid_Term, double final, double gPA)
        {
            DailyTestScore = dailyTestScore;
            Mid_Term = mid_Term;
            Final = final;
            GPA = gPA;
        }
    }
}
