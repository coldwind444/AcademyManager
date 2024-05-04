using AcademyManager.Models;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class StudentResultVM : BaseViewModel
    {
        #region Support class
        public class CourseRecord
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public double Daily {  get; set; }
            public double Project {  get; set; }
            public double Mid { get; set; }
            public double Final { get; set; }
            public double GPA { get; set; }
            public int Credits { get; set; }
            public CourseRecord(string id, string name, double d, double p, double m, double f, double g, int c)
            {
                ID = id;
                Name = name;
                Daily = d;
                Project = p;
                Mid = m;
                Final = f;
                GPA = g;
                Credits = c;
            }
        }
        #endregion
        #region Commands
        public ICommand BackCommand { get; set; }
        public ICommand SelectTermCommand { get; set; }
        #endregion
        #region Properties
        private MainVM ParentVM { get; set; }
        private ObservableCollection<CourseRecord> _result;
        private ObservableCollection<string> _terms;
        private int _passcredits;
        private int _termcredits;
        private int _totalcredits;
        private double _termgpa;
        private double _totalgpa;
        public ObservableCollection<CourseRecord> Result
        {
            get => _result;
            set { _result = value; OnPropertyChanged(); }
        }
        public ObservableCollection<string> Terms
        {
            get => _terms;
            set { _terms = value; OnPropertyChanged(); }
        }
        public int TermCredits
        {
            get { return _termcredits; }
            set { _termcredits = value; OnPropertyChanged(); }
        }
        public int PassCredits
        {
            get { return _passcredits; }
            set { _passcredits = value; OnPropertyChanged(); }
        }
        public int TotalCredits
        {
            get { return _totalcredits; }
            set { _totalcredits = value; OnPropertyChanged(); }
        }
        public double TermGPA
        {
            get { return _termgpa; }
            set { _termgpa = value; OnPropertyChanged(); }
        }
        public double TotalGPA
        {
            get { return _totalgpa; }
            set { _totalgpa = value; OnPropertyChanged(); }
        }
        #endregion
        #region Methods
        private void LoadTerms()
        {
            var list = new List<string>();
            foreach (ClassIdentifier c in MainVM.CurrentUser.StudyElements)
            {
                bool contain = list.Any(e => e == c.TermID);
                if (!contain) list.Add(c.TermID);
            }
            Terms.Clear();
            Terms = [.. list];
        }
        private void LoadResult(string termid)
        {
            var list = new List<CourseRecord>();
            foreach (Class cls in MainVM.UserClassList)
            {
                if (cls.TermID == termid)
                {
                    string sid = MainVM.CurrentUser.ID;
                    StudentRecord strc = cls.Students[sid];
                    CourseRecord rc = new CourseRecord(cls.CourseID, cls.CourseName, strc.DailyTestScore, strc.Project, strc.Mid_Term, strc.Final, strc.GPA, cls.CourseCredits);
                    list.Add(rc);
                }
            }
            Result.Clear();
            Result = [.. list];
        }
        private double CalculateTotalGPA()
        {
            string sid = MainVM.CurrentUser.ID;
            int totalcredits = 0;
            Dictionary<string, double> sums = new Dictionary<string, double>();
            Dictionary<string, int> credits = new Dictionary<string, int>();
            
            foreach (Class cls in MainVM.UserClassList)
            {
                totalcredits += cls.CourseCredits;
                if (!sums.ContainsKey(cls.TermID))
                {
                    sums[cls.TermID] = 0;
                    credits[cls.TermID] = 0;
                } else
                {
                    sums[cls.TermID] += (cls.Students[sid].GPA * cls.CourseCredits) ;
                    credits[cls.TermID] += cls.CourseCredits;
                }
            }

            double total = 0;
            foreach (string termid in sums.Keys)
            {
                double gpa = sums[termid] / credits[termid];
                total += gpa * credits[termid];
            }

            double result = Math.Round(total/totalcredits, 1);

            return result;
        }
        private void LoadOverall(string termid)
        {
            double sum = 0;
            string sid = MainVM.CurrentUser.ID;
            foreach (Class cls in MainVM.UserClassList)
            {
                if (cls.Students[sid].GPA >= 4.0) TotalCredits += cls.CourseCredits;
                if (cls.TermID == termid)
                {
                    TermCredits += cls.CourseCredits;
                    sum += (cls.CourseCredits * cls.Students[sid].GPA);
                    if (cls.Students[sid].GPA >= 4.0) PassCredits += cls.CourseCredits;
                }
            }
            double gpa = sum / TermCredits * 0.4;
            TermGPA = Math.Round(gpa, 1);
            TotalGPA = CalculateTotalGPA();
        }
        private void InitializeCommand()
        {
            BackCommand = new RelayCommand<object>(p => true, p =>
            {
                ParentVM.HomeNavigateCommand.Execute(null);
            });

            SelectTermCommand = new RelayCommand<ComboBox>(p => true, p =>
            {
                string? termid = p.SelectedValue as string;
                if (!string.IsNullOrEmpty(termid))
                {
                    LoadResult(termid);
                    LoadOverall(termid);
                }
            });
        }
        #endregion
        public StudentResultVM(MainVM vm)
        {
            ParentVM = vm;
            TermCredits = 0;
            PassCredits = 0;
            TotalCredits = 0;
            Terms = new ObservableCollection<string>();
            Result = new ObservableCollection<CourseRecord>();
            LoadTerms();
            InitializeCommand();
        }
    }
}
