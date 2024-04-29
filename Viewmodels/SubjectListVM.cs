using AcademyManager.Models;
using AcademyManager.UCViews;
using AcademyManager.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class SubjectListVM : BaseViewModel
    {
        #region Commands
        public ICommand BackCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        #endregion
        #region Properties
        private MainVM ParentVM { get; set; }
        private WrapPanel SubjList {  get; set; }
        private string _noti;
        private Visibility _notiV;
        public string Noti
        {
            get { return _noti; }
            set { _noti = value; OnPropertyChanged(); }
        }
        public Visibility NotiV
        {
            get { return _notiV; }
            set { _notiV = value; OnPropertyChanged(); }
        }
        #endregion
        #region Methods
        private async Task<List<Class>> GetClassList()
        {
            var list = new List<Class>();
            DatabaseManager db = new DatabaseManager();
            if (MainVM.CurrentAccount.Type == 1)
            {
                InstructorUser? user = MainVM.CurrentUser as InstructorUser;
                if (user != null)
                {
                    var batch = new List<Task<Class>>();
                    foreach (ClassIdentifier c in user.StudyElements)
                    {
                        batch.Add(db.GetClassAsync(c.TermID, c.CourseID, c.ClassID));
                    }
                    var result = await Task.WhenAll(batch);
                    list.AddRange(result);
                }
            }
            else
            {
                StudentUser? user = MainVM.CurrentUser as StudentUser;
                if (user != null)
                {
                    var batch = new List<Task<Class>>();
                    foreach (ClassIdentifier c in user.StudyElements)
                    {
                        batch.Add(db.GetClassAsync(c.TermID, c.CourseID, c.ClassID));
                    }
                    var result = await Task.WhenAll(batch);
                    list.AddRange(result);
                }
            }
            return list;
        }
        private async void LoadClasses()
        {
            DatabaseManager db = new DatabaseManager();
            List<Class> list = await GetClassList();
            if (list.Count == 0)
            {
                if (MainVM.CurrentAccount.Type == 1)
                {
                    Noti = "Bạn chưa được phân công giảng dạy.";
                    NotiV = Visibility.Visible;
                } else
                {
                    Noti = "Bạn chưa đăng ký khóa học nào.";
                    NotiV = Visibility.Visible;
                }
                return;
            }

            foreach (Class c in list)
            {
                string bg = c.BeginTime.ToString("HH:mm"),
                       e = c.EndTime.ToString("HH:mm");
                string time = $"{bg} - {e}";
                SubjectUC item = new SubjectUC(ParentVM, c, c.CourseID, c.CourseName, c.InstructorName, c.ClassID, c.Room, time);
                SubjList.Children.Add(item);
            }
        }
        private void InitializeCommands()
        {
            BackCommand = new RelayCommand<object>(p => true, p =>
            {
                ParentVM.CurrentView = ParentVM.HomeView;
            });

            RegisterCommand = new RelayCommand<object>(p => true, p =>
            {
                if (ParentVM.CourseRegisterView == null)
                {
                    ParentVM.CourseRegisterView = new SubjectRegister();
                    ParentVM.CurrentView = ParentVM.CourseRegisterView;
                } else
                    ParentVM.CurrentView = ParentVM.CourseRegisterView;
            });
        }
        #endregion
        public SubjectListVM(MainVM vm, WrapPanel list)
        {
            ParentVM = vm;
            SubjList = list;
            NotiV = Visibility.Hidden;
            LoadClasses();
            InitializeCommands();
        }
    }
}
