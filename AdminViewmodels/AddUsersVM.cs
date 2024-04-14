using AcademyManager.Models;
using AcademyManager.Viewmodel;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using OfficeOpenXml;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

namespace AcademyManager.AdminViewmodels
{
    public class AddUsersVM : BaseViewModel
    {
        #region Commands
        public ICommand UploadCommand { get; set; }
        public ICommand BrowseCommand { get; set; }
        public ICommand DownloadCommand { get; set; }
        #endregion

        #region Properties
        private string _path;
        private string _content;
        private Brush _iconbrush;
        private PackIconKind _icon;
        private Visibility _notice;
        private Visibility _loading;
        public Visibility Loading
        {
            get { return _loading; }
            set { _loading = value; OnPropertyChanged(); }
        }
        public Brush IconBrush
        {
            get { return _iconbrush; }
            set { _iconbrush = value; OnPropertyChanged(); }
        }
        public string Path
        {
            get { return _path; }
            set { _path = value; OnPropertyChanged(); }
        }
        public PackIconKind Icon
        {
            get { return _icon; }
            set { _icon = value; OnPropertyChanged(); }
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
        private List<StudentUser>? GetStudentsDataFromExcel()
        {
            List<StudentUser> data = new List<StudentUser>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(_path)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;
                if (colCount < 7) return null;

                for (int row = 2; row <= rowCount; row++)
                {
                    // read data as string
                    string? studentID = worksheet.Cells[row, 1].Value.ToString();
                    string? fullname = worksheet.Cells[row, 2].Value.ToString();
                    string? bd = worksheet.Cells[row, 3].Value.ToString();
                    string? email = worksheet.Cells[row, 4].Value.ToString();
                    string? faculty = worksheet.Cells[row, 5].Value.ToString();
                    string? major = worksheet.Cells[row, 6].Value.ToString();
                    string? imgpath = worksheet.Cells[row, 7].Value.ToString();

                    // Check string input
                    if (NullOrEmpty(studentID) || NullOrEmpty(fullname) || NullOrEmpty(email) || NullOrEmpty(faculty)
                        || NullOrEmpty(major) || NullOrEmpty(imgpath)) return null;

                    // Image to Base64

                    // try to convert some data to correct type
                    DateOnly birthday;
                    string avtbase64;
                    try
                    {
                        birthday = StringToDateOnly(bd);
                        byte[] img = File.ReadAllBytes(imgpath);
                        avtbase64 = Convert.ToBase64String(img);
                    }
                    catch
                    {
                        return null;
                    }
                    if (data.Any(student => student.ID == studentID)) return null;
                    else data.Add(new StudentUser(studentID, fullname, email, birthday, faculty, avtbase64, major));
                }
            }
            return data;
        }
        private List<InstructorUser>? GetInstructorsDataFromExcel()
        {
            List<InstructorUser> data = new List<InstructorUser>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(_path)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;
                if (colCount < 7) return null;

                for (int row = 2; row <= rowCount; row++)
                {
                    // read data as string
                    string? insID = worksheet.Cells[row, 1].Value.ToString();
                    string? fullname = worksheet.Cells[row, 2].Value.ToString();
                    string? bd = worksheet.Cells[row, 3].Value.ToString();
                    string? email = worksheet.Cells[row, 4].Value.ToString();
                    string? faculty = worksheet.Cells[row, 5].Value.ToString();
                    string? certificate = worksheet.Cells[row, 6].Value.ToString();
                    string? speciality = worksheet.Cells[row, 7].Value.ToString();
                    string? imgpath = worksheet.Cells[row, 7].Value.ToString();

                    // Check string input
                    if (NullOrEmpty(insID) || NullOrEmpty(fullname) || NullOrEmpty(email) || NullOrEmpty(certificate)
                        || NullOrEmpty(speciality) || NullOrEmpty(faculty)) return null;

                    // try to convert some data to correct type
                    DateOnly birthday;
                    string avtbase64;
                    try
                    {
                        birthday = StringToDateOnly(bd);
                        byte[] img = File.ReadAllBytes(imgpath);
                        avtbase64 = Convert.ToBase64String(img);
                    }
                    catch
                    {
                        return null;
                    }
                    if (data.Any(ins => ins.ID == insID)) return null;
                    else data.Add(new InstructorUser(insID, fullname, email, birthday, faculty, avtbase64, certificate, speciality));
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

            UploadCommand = new RelayCommand<ComboBox>(p => { return _path != null && _path.Length > 0 && p.SelectedIndex != -1; }, async p =>
            {
                if (p.SelectedIndex == 0)
                {
                    List<StudentUser>? students = GetStudentsDataFromExcel();
                    if (students != null)
                    {
                        var accbatch = new List<Task>();
                        var userbatch = new List<Task>();
                        DatabaseManager db = new DatabaseManager();
                        Loading = Visibility.Visible;
                        foreach (StudentUser std in students)
                        {
                            Account acc = new Account(std.ID, std.Email, null, 0);
                            string uuid = acc.UUID;
                            Task acctask = db.UpdateAccountAsync(acc);
                            Task usertask = db.UpdateStudentAsync(uuid, std);
                            accbatch.Add(acctask);
                            userbatch.Add(usertask);
                        }
                        await Task.WhenAll(accbatch);
                        await Task.WhenAll(userbatch);
                        accbatch.Clear();
                        userbatch.Clear();
                        Content = "Cập nhật thành công";
                        Icon = PackIconKind.Check;
                        IconBrush = Brushes.GreenYellow;
                        Loading = Visibility.Hidden;
                        Notice = Visibility.Visible;
                        await Task.Delay(3000);
                        Notice = Visibility.Hidden;
                    }
                    else
                    {
                        Content = "Sai định dạng";
                        Icon = PackIconKind.Close;
                        IconBrush = Brushes.OrangeRed;
                        Notice = Visibility.Visible;
                        await Task.Delay(3000);
                        Notice = Visibility.Hidden;
                    }
                } else if (p.SelectedIndex == 1)
                {
                    List<InstructorUser>? instructors = GetInstructorsDataFromExcel();
                    if (instructors != null)
                    {
                        var accbatch = new List<Task>();
                        var userbatch = new List<Task>();
                        DatabaseManager db = new DatabaseManager();
                        Loading = Visibility.Visible;
                        foreach (InstructorUser ins in instructors)
                        {
                            Account acc = new Account(ins.ID, ins.Email, null, 0);
                            string uuid = acc.UUID;
                            Task acctask = db.UpdateAccountAsync(acc);
                            Task usertask = db.UpdateInstructorAsync(uuid, ins);
                            accbatch.Add(acctask);
                            userbatch.Add(usertask);
                        }
                        await Task.WhenAll(accbatch);
                        await Task.WhenAll(userbatch);
                        accbatch.Clear();
                        userbatch.Clear();
                        Content = "Cập nhật thành công";
                        Icon = PackIconKind.Check;
                        IconBrush = Brushes.GreenYellow;
                        Loading = Visibility.Hidden;
                        Notice = Visibility.Visible;
                        await Task.Delay(2000);
                        Notice = Visibility.Hidden;
                    }
                    else
                    {
                        Content = "Sai định dạng";
                        Icon = PackIconKind.Close;
                        Notice = Visibility.Visible;
                        await Task.Delay(2000);
                        Notice = Visibility.Hidden;
                    }
                }
                Path = String.Empty;
            });

            DownloadCommand = new RelayCommand<ComboBox>(p => { return p.SelectedIndex != -1; }, p =>
            {
                string url1 = "https://firebasestorage.googleapis.com/v0/b/academymanager-5ea2b.appspot.com/o/excelformat%2FStudentFileFormat.xlsx?alt=media&token=31b0bddc-9fa7-424a-a714-1fbad7a7d9e1",
                       url2 = "https://firebasestorage.googleapis.com/v0/b/academymanager-5ea2b.appspot.com/o/excelformat%2FInstructorFileFormat.xlsx?alt=media&token=b748facf-bdb0-4889-ada6-edb279fa2b8e";
                if (p.SelectedIndex == 0)
                {
                    Process.Start(new ProcessStartInfo(url1) { UseShellExecute = true });
                } else if (p.SelectedIndex == 1)
                {
                    Process.Start(new ProcessStartInfo(url2) { UseShellExecute = true });
                }
            });
        }
        #endregion
        public AddUsersVM()
        {
            InitializeCommands();
            Notice = Visibility.Hidden;
            Loading = Visibility.Hidden;
        }
    }
}
