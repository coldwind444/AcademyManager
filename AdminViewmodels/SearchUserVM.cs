using AcademyManager.Models;
using AcademyManager.Viewmodel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace AcademyManager.AdminViewmodels
{
    public class SearchUserVM : BaseViewModel
    {
        #region Support Classes
        private class StudentDataRecord
        {
            public string CourseID { get; set; }
            public string CourseName { get; set; }
            public double MidTerm { get; set; }
            public double Final { get; set; }
            public double GPA { get; set; }
            public StudentDataRecord(string cid, string cname, double midTerm, double final, double gpa)
            {
                CourseID = cid;
                CourseName = cname;
                MidTerm = midTerm;
                Final = final;
                GPA = gpa;
            }
        }

        private class InsDataRecord
        {
            public string CourseID { get; set; }
            public string CourseName { get; set; }
            public string ClassID { get; set; }
            public DayOfWeek Workday { get; set; }
            public TimeOnly BeginTime { get; set; }
            public TimeOnly EndTime { get; set; }
            public InsDataRecord(string cid, string cname, string clid, DayOfWeek day, TimeOnly bgt, TimeOnly et)
            {
                CourseID = cid;
                CourseName = cname;
                ClassID = clid;
                Workday = day;
                BeginTime = bgt;
                EndTime = et;
            }
        }
        #endregion

        #region Commands
        public ICommand SearchCommand { get; set; }
        #endregion

        #region Properties
        private int _selectedIdx;
        private string _id;
        private string _userid;
        private string _fullname;
        private string _email;
        private string _birthday;
        private string _faculty;
        private string _avatar;
        private string _add1;
        private string _add2;
        private string _tabheader;
        private Visibility _dataV;
        private Visibility _notfound;
        public Visibility NotFound
        {
            get { return _notfound; }
            set { _notfound = value; OnPropertyChanged(); }
        }
        public Visibility DataV
        {
            get { return _dataV; }
            set { _dataV = value; OnPropertyChanged(); }
        }
        public string TabHeader
        {
            get { return _tabheader; }
            set { _tabheader = value; OnPropertyChanged(); }
        }
        public int SelectedIdx
        {
            get { return _selectedIdx; }
            set { _selectedIdx = value; OnPropertyChanged(); }
        }
        public string ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }
        public string UserID
        {
            get { return _userid; }
            set { _userid = value; OnPropertyChanged();}
        }
        public string Fullname
        {
            get { return _fullname; }
            set { _fullname = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }
        public string Birthday
        {
            get { return _birthday; }
            set { _birthday = value; OnPropertyChanged(); }
        }
        public string Faculty
        {
            get { return _faculty; }
            set { _faculty = value; OnPropertyChanged();}
        }
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; OnPropertyChanged(); }
        }
        public string Addition1
        {
            get { return _add1; }
            set { _add1 = value; OnPropertyChanged(); }
        }
        public string Addition2
        {
            get { return _add2; }
            set { _add2 = value; OnPropertyChanged(); }
        }
        #endregion 

        #region Methods
        private async Task<List<StudentDataRecord>> GetStudentResultData(StudentUser user)
        {
            List<StudentDataRecord> list = new List<StudentDataRecord>();
            DatabaseManager db = new DatabaseManager();
            foreach (ClassIdentifier cls in user.StudyElements)
            {
                Course c = await db.GetCourseAsync(cls.TermID, cls.CourseID);
                double mid = c.Classes[cls.ClassID].Students[ID].Mid_Term;
                double final = c.Classes[cls.ClassID].Students[ID].Final;
                double gpa = c.Classes[cls.ClassID].Students[ID].GPA;
                list.Add(new StudentDataRecord(c.CourseID, c.CourseName, mid, final, gpa));
            }
            return list;
        }
        private async Task<List<InsDataRecord>> GetInsData(InstructorUser user)
        {
            List<InsDataRecord> list = new List<InsDataRecord>();
            DatabaseManager db = new DatabaseManager();
            foreach (ClassIdentifier cls in user.StudyElements)
            {
                Course c = await db.GetCourseAsync(cls.TermID, cls.CourseID);
                DayOfWeek day = c.Classes[cls.ClassID].Weekday;
                TimeOnly bgt = c.Classes[cls.ClassID].BeginTime;
                TimeOnly et = c.Classes[cls.ClassID].EndTime;
                list.Add(new InsDataRecord(c.CourseID, c.CourseID, cls.ClassID, day, bgt, et));
            }
            return list;
        }
        private async Task LoadStudentDataGrid(DataGrid grid, StudentUser user)
        {
            DataGridTextColumn courseIdCol = new DataGridTextColumn(),
                               courseNameCol = new DataGridTextColumn(),
                               midTCol = new DataGridTextColumn(),
                               finalCol = new DataGridTextColumn(),
                               gpaCol = new DataGridTextColumn();

            courseIdCol.Header = "Mã môn học";
            courseIdCol.Binding = new Binding("CourseID");

            courseNameCol.Binding = new Binding("CourseName");
            courseNameCol.Header = "Tên môn học";

            midTCol.Header = "Giữa kỳ";
            midTCol.Binding = new Binding("MidTerm");

            finalCol.Header = "Cuối kỳ";
            finalCol.Binding = new Binding("Final");

            gpaCol.Header = "GPA";
            gpaCol.Binding = new Binding("GPA");

            grid.ItemsSource = await GetStudentResultData(user);

            grid.Columns.Add(courseIdCol);
            grid.Columns.Add(courseNameCol);
            grid.Columns.Add(midTCol);
            grid.Columns.Add(finalCol);
            grid.Columns.Add(gpaCol);
        }
        private async Task LoadInsDataGrid(DataGrid grid, InstructorUser user)
        {
            DataGridTextColumn courseIdCol = new DataGridTextColumn(),
                               courseNameCol = new DataGridTextColumn(),
                               cidCol = new DataGridTextColumn(),
                               dayCol = new DataGridTextColumn(),
                               bgtCol = new DataGridTextColumn(),
                               etCol = new DataGridTextColumn();

            courseIdCol.Header = "Mã môn học";
            courseIdCol.Binding = new Binding("CourseID");

            courseNameCol.Header = "Tên môn học";
            courseNameCol.Binding = new Binding("CourseName");

            cidCol.Header = "Lớp";
            cidCol.Binding = new Binding("ClassID");

            dayCol.Header = "Thứ";
            Binding b = new Binding("Workday");
            b.Converter = new DayOfWeekConverter();
            dayCol.Binding = b;

            bgtCol.Header = "Giờ bắt đầu";
            bgtCol.Binding = new Binding("BeginTime") { StringFormat = "HH:mm" };

            etCol.Header = "Giờ kết thúc";
            etCol.Binding = new Binding("EndTime") { StringFormat = "HH:mm" };

            grid.ItemsSource = await GetInsData(user);

            grid.Columns.Add(courseIdCol);
            grid.Columns.Add(courseNameCol);
            grid.Columns.Add(cidCol);
            grid.Columns.Add(dayCol);
            grid.Columns.Add(bgtCol);
            grid.Columns.Add(etCol);
        }
        private void LoadStudentPersonalInfo(StudentUser user)
        {
            Avatar = user.AvatarBase64;
            UserID = $"Mã số sinh viên: {user.ID}";
            Fullname = $"Họ và tên: {user.Fullname}";
            Email = $"Email: {user.Email}";
            string birthday = user.Birthday.ToString("dd/MM/yyyy");
            Birthday = $"Ngày sinh: {birthday}";
            Faculty = $"Khoa: {user.Faculty}";
            Addition1 = $"Ngành: {user.Major}";
        }
        private void LoadInsPersonalInfo(InstructorUser user)
        {
            Avatar = user.AvatarBase64;
            UserID = $"Mã số sinh viên: {user.ID}";
            Fullname = $"Họ và tên: {user.Fullname}";
            Email = $"Email: {user.Email}";
            string birthday = user.Birthday.ToString("dd/MM/yyyy");
            Birthday = $"Ngày sinh: {birthday}";
            Faculty = $"Khoa: {user.Faculty}";
            Addition1 = $"Bằng cấp: {user.Certificate}";
            Addition2 = $"Chuyên môn: {user.Speciality}";
        }
        private void InitializeCommands()
        {
            SearchCommand = new RelayCommand<DataGrid>(p => { return true; }, async p =>
            {
                if (SelectedIdx == -1 || ID == null || ID == String.Empty) return;

                DataV = Visibility.Visible;
                if (SelectedIdx == 0)
                {
                    DatabaseManager db = new DatabaseManager();
                    StudentUser user = await db.GetStudentAsync(ID);
                    if (user == null)
                    {
                        NotFound = Visibility.Visible;
                        return;
                    }
                    LoadStudentPersonalInfo(user);
                    await LoadStudentDataGrid(p, user);
                    TabHeader = "Kết quả học tập";
                    
                } else if (SelectedIdx == 1) 
                {
                    DatabaseManager db = new DatabaseManager();
                    InstructorUser user = await db.GetInstructorAsync(ID);
                    if (user == null)
                    {
                        NotFound= Visibility.Visible;
                        return;
                    }
                    LoadInsPersonalInfo(user);
                    await LoadInsDataGrid(p, user);
                    TabHeader = "Các lớp giảng dạy";
                }
            });
        }
        #endregion
        public SearchUserVM()
        {
            InitializeCommands();
            DataV = Visibility.Hidden;
            NotFound = Visibility.Hidden;
        }
    }
}