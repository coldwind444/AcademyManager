using AcademyManager.Models;
using AcademyManager.Viewmodel;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OfficeOpenXml;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using System.Windows.Media;

namespace AcademyManager.AdminViewmodels
{
    public class AddScheduleVM : BaseViewModel
    {
        #region Commands
        public ICommand UploadCommand { get; set; }
        public ICommand BrowseCommand { get; set; }
        public ICommand DownloadCommand { get; set; }
        #endregion

        #region Properties
        private string _path;
        private string _content;
        private Visibility _loading;
        private Visibility _notice;
        private PackIconKind _icon;
        private Brush _iconBrush;
        public Visibility Loading
        {
            get { return _loading; }
            set { _loading = value; OnPropertyChanged(); }
        }
        public PackIconKind Icon
        {
            get { return _icon; }
            set { _icon = value; OnPropertyChanged(); }
        }
        public Brush IconBrush
        {
            get { return _iconBrush; }
            set { _iconBrush = value; OnPropertyChanged(); }
        }
        public string Path
        {
            get { return _path; }
            set { _path = value; OnPropertyChanged(); }
        }
        public string Content
        {
            get { return _content; }
            set { _content = value; OnPropertyChanged(); }
        }
        public Visibility Notice
        {
            get { return _notice; }
            set { _notice = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        private DayOfWeek StringToDayOfWeek(string vietnameseDay)
        {
            var vietnameseToEnglishDayMapping = new Dictionary<string, string>
            {
                {"Thứ Hai", "Monday"},
                {"Thứ Ba", "Tuesday"},
                {"Thứ Tư", "Wednesday"},
                {"Thứ Năm", "Thursday"},
                {"Thứ Sáu", "Friday"},
                {"Thứ Bảy", "Saturday"},
                {"Chủ Nhật", "Sunday"}
            };

            if (vietnameseToEnglishDayMapping.TryGetValue(vietnameseDay, out string englishDay))
            {
                // Parse English day name to DayOfWeek enum
                if (Enum.TryParse<DayOfWeek>(englishDay, true, out DayOfWeek dayOfWeek))
                {
                    return dayOfWeek;
                }
            }
            throw new Exception("Cannot convert");
        }
        private TimeOnly StringToTimeOnly(string s)
        {
            if (TimeOnly.TryParseExact(s, "HH:mm", null, System.Globalization.DateTimeStyles.None, out TimeOnly time))
            {
                return time;
            }
            else
            {
                throw new Exception("Cannot convert");
            }
        }
        private DateOnly StringToDateOnly(string s)
        {
            if (DateOnly.TryParseExact(s, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateOnly date))
            {
                return date;
            }
            else
            {
                throw new Exception("Cannot convert");
            }
        }
        private bool NullOrEmpty(string s)
        {
            return s == null || s.Length == 0;
        }
        private bool InvalidClass(Class c1, Class c2)
        {
            if (c1.ClassID != c2.ClassID)
            {
                if (c1.InstructorID != c2.InstructorID) return false;
                else
                {
                    bool manage2class1time = (c1.Weekday == c2.Weekday) && ((c1.BeginTime >= c2.BeginTime && c1.EndTime <= c2.EndTime) || 
                        (c1.BeginTime <= c2.BeginTime && c1.EndTime >= c2.EndTime));
                    if (manage2class1time) return true;
                }
            }
            return false;
        }
        private List<Term>? GetDataFromExcel()
        {
            List<Term> data = new List<Term>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 
            using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(_path)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;
                if (colCount < 12) return null;

                for (int row = 2; row <= rowCount; row++)
                {
                    // read data as string
                    string? termID = worksheet.Cells[row, 1].Value.ToString();
                    string? courseID = worksheet.Cells[row, 2].Value.ToString();
                    string? courseName = worksheet.Cells[row, 3].Value.ToString();
                    string? crd = worksheet.Cells[row, 4].Value.ToString();
                    string? classID = worksheet.Cells[row, 5].Value.ToString();
                    string? insID = worksheet.Cells[row, 6].Value.ToString();
                    string? room = worksheet.Cells[row, 7].Value.ToString();

                    // Check string input
                    if (NullOrEmpty(termID) || NullOrEmpty(courseID) || NullOrEmpty(courseName) || NullOrEmpty(classID)
                        || NullOrEmpty(insID) || NullOrEmpty(room) || NullOrEmpty(crd)) return null;

                    string? day = worksheet.Cells[row, 8].Value.ToString();
                    string? bgt = worksheet.Cells[row, 9].Value.ToString();
                    string? et = worksheet.Cells[row, 10].Value.ToString();
                    string? bgd = worksheet.Cells[row, 11].Value.ToString();
                    string? ed = worksheet.Cells[row, 12].Value.ToString();

                    // try to convert some data to correct type
                    DayOfWeek dayOfWeek ;
                    TimeOnly beginTime ;
                    TimeOnly endTime;
                    DateOnly beginDate;
                    DateOnly endDate;
                    int credits;

                    try
                    {
                        dayOfWeek = StringToDayOfWeek(day);
                        beginTime = StringToTimeOnly(bgt);
                        endTime = StringToTimeOnly(et);
                        beginDate = StringToDateOnly(bgd);
                        endDate = StringToDateOnly(ed);
                        credits = Convert.ToInt32(crd);
                    } catch 
                    {
                        return null;
                    }

                    // Check date time
                    if (beginTime >= endTime || beginDate >= endDate) return null;

                    int Tidx = data.FindIndex(t => t.TermID == termID);
                    if (Tidx != -1) // Check if term is contained in list
                    {
                        // if contains
                        if (data[Tidx].Courses.ContainsKey(courseID)) // Check if course is contained in term
                        {
                            // if contains
                            if (!data[Tidx].Courses[courseID].Classes.ContainsKey(classID)) // Check if class is not contained in course
                            {
                                // if not contain
                                // check if there is at least one class in the same time that manage by the same instructor
                                Class cls = new Class(classID, insID, dayOfWeek, beginTime, endTime, beginDate, endDate, room);
                                foreach (Class c in data[Tidx].Courses[courseID].Classes.Values)
                                {
                                    if (InvalidClass(cls, c)) return null;
                                }
                                data[Tidx].Courses[courseID].Classes[classID] = cls;
                            }
                            else return null;
                        } else
                        {
                            data[Tidx].Courses[courseID] = new Course(courseID, courseName, credits);
                            data[Tidx].Courses[courseID].Classes[classID] = new Class(classID, insID, dayOfWeek, beginTime, endTime, beginDate, endDate, room);
                        }
                    } else
                    {
                        Term term = new Term(termID);
                        term.Courses[courseID] = new Course(courseID, courseName, credits);
                        term.Courses[courseID].Classes[classID] = new Class(classID, insID, dayOfWeek, beginTime, endTime, beginDate, endDate, room);
                        data.Add(term);
                    }
                }
            }
            return data;
        }
        private void InitializeCommands()
        {
            BrowseCommand = new RelayCommand<TextBox>(p => { return true; }, p =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Pick a file";
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                if (openFileDialog.ShowDialog() == true)
                {
                    Path = openFileDialog.FileName;
                }
            });

            UploadCommand = new RelayCommand<object>(p => { return _path != null && _path.Length > 0; }, async p =>
            {
                
                List<Term>? terms = GetDataFromExcel();
                if (terms != null)
                {
                    DatabaseManager db = new DatabaseManager();
                    Loading = Visibility.Visible;
                    foreach (Term term in terms) await db.UpdateTermAsync(term);
                    Content = "Cập nhật thành công";
                    Notice = Visibility.Visible;
                    Loading = Visibility.Hidden;
                    Icon = PackIconKind.Check;
                    IconBrush = Brushes.GreenYellow;
                    await Task.Delay(3000);
                    Notice = Visibility.Hidden;
                }
                else
                {
                    Content = "Sai định dạng";
                    Notice = Visibility.Visible;
                    Icon = PackIconKind.Close;
                    IconBrush = Brushes.OrangeRed;
                    await Task.Delay(3000);
                    Notice = Visibility.Hidden;
                }
                Path = String.Empty;
            });

            DownloadCommand = new RelayCommand<object>(p => { return true; }, p => 
            {
                string url = "https://firebasestorage.googleapis.com/v0/b/academymanager-5ea2b.appspot.com/o/excelformat%2FScheduleFileFormat.xlsx?alt=media&token=9af4068f-0fc1-4e92-bff1-35bd82c690d9";
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            });
        }
        #endregion
        public AddScheduleVM()
        {
            Notice = Visibility.Hidden;
            Loading = Visibility.Hidden;
            InitializeCommands();
        }
    }
}
