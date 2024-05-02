using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AcademyManager.Models;
using AcademyManager.Views;
using Flattinger.Core.Theme;
using Flattinger.UI.ToastMessage;
using Flattinger.UI.ToastMessage.Controls;
using Microsoft.Win32;
using OfficeOpenXml;

namespace AcademyManager.Viewmodels
{
    public class ScoreUpdateVM : BaseViewModel
    {
        #region Commands
        public ICommand BrowseCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DownloadCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        #endregion
        #region Properties
        private Class ClassData { get; set; }
        private NotificationContainer _notification;
        private AppTheme _theme;
        private ToastProvider _toastProvider;
        private string _filepath;
        private Visibility _load;
        public Visibility Loading
        {
            get { return _load; }
            set { _load = value; OnPropertyChanged(); }
        }
        public string FilePath
        {
            get { return _filepath; }
            set { _filepath = value; OnPropertyChanged(); }
        }
        #endregion
        #region Methods
        private bool NullOrEmpty(string? s)
        {
            return (s == null || s.Length == 0);
        }
        private async Task SendNotification()
        {
            Random random = new Random();
            DatabaseManager db = new DatabaseManager();
            int id = random.Next(0, 1000);
            string title = $"{ClassData.CourseName} ({ClassData.CourseID} - {ClassData.ClassID})";
            string message = "Điểm số đã được cập nhật!";
            Notification noti = new Notification(id, title, message, DateTime.Now);
            var batch = new List<Task>();
            foreach (string s in ClassData.Students.Keys)
            {
                Task t = db.SendNotificationAsync(s, 2, noti);
            }
            await Task.WhenAll(batch);
        }
        private List<StudentRecord>? GetStudentResultData()
        {
            var list = new List<StudentRecord>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(_filepath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                if (rowCount != 7) return null;

                for (int row = 2; row <= rowCount; row++)
                {
                    string? id = worksheet.Cells[row, 1].Value.ToString();
                    string? name = worksheet.Cells[row, 2].Value.ToString();
                    string? daily = worksheet.Cells[row, 3].Value.ToString();
                    string? project = worksheet.Cells[row, 4].Value.ToString();
                    string? mid = worksheet.Cells[row, 5].Value.ToString();
                    string? final = worksheet.Cells[row, 6].Value.ToString();
                    string? gpa = worksheet.Cells[row, 7].Value.ToString();

                    if (NullOrEmpty(id) || NullOrEmpty(name)) return null;

                    double d, p, m, f, g;
                    try
                    {
                        d = Convert.ToDouble(daily);
                        p = Convert.ToDouble(project);
                        m = Convert.ToDouble(mid);
                        f = Convert.ToDouble(final);
                        g = Convert.ToDouble(gpa);
                    } catch
                    {
                        return null;
                    }

                    list.Add(new StudentRecord(id, name, d, p, m, f, g));
                }
            }
            return list;
        }
        private async Task UpdateScore(List<StudentRecord>? list)
        {
            if (list == null) return;
            Dictionary<string, StudentRecord> dict = new Dictionary<string, StudentRecord>();
            foreach (StudentRecord rc in list)
            {
                dict.Add(rc.ID, rc);
            }
            InstructorUser? user = MainVM.CurrentUser as InstructorUser;
            if (user != null) 
                await user.UpdateScore(ClassData.TermID, ClassData.CourseID, ClassData.ClassID, dict);
        }
        private void InitializeCommands()
        {
            BrowseCommand = new RelayCommand<object>(p => true, p =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Pick a file";
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                if (openFileDialog.ShowDialog() == true)
                {
                    FilePath = openFileDialog.FileName;
                }
            });

            UpdateCommand = new RelayCommand<object>(p => FilePath != null && FilePath.Length > 0, async p =>
            {
                Loading = Visibility.Visible;
                var list = GetStudentResultData();
                await UpdateScore(list);
                if (list != null)
                {
                    _toastProvider.NotificationService.AddNotification(Flattinger.Core.Enums.ToastType.SUCCESS, "Cập nhật thành công!", "Điểm đã được cập nhật!", 1000);
                    await SendNotification();
                }
                else
                    _toastProvider.NotificationService.AddNotification(Flattinger.Core.Enums.ToastType.ERROR, "Cập nhật thất bại!", "Dữ liệu không hợp lệ!", 1000);
                Loading = Visibility.Hidden;
            });

            DownloadCommand = new RelayCommand<object>(p => true, p =>
            {
                string url = "https://firebasestorage.googleapis.com/v0/b/academymanager-5ea2b.appspot.com/o/excelformat%2FStudentResultFormat.xlsx?alt=media&token=40467621-549b-43ee-be13-b1f0b327882a";
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            });

            CloseCommand = new RelayCommand<ScoreUpdateWindow>(p => true, p =>
            {
                p.Close();
            });
        }
        #endregion
        public ScoreUpdateVM(Class data, NotificationContainer n)
        {
            ClassData = data;
            _notification = n;
            _theme = new AppTheme();
            _theme.ChangeTheme(Flattinger.Core.Enums.Theme.LIGHT);
            _toastProvider = new ToastProvider(_notification);
            Loading = Visibility.Hidden;
            InitializeCommands();
        }
    }
}
