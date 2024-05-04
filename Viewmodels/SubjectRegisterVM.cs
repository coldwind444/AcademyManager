using AcademyManager.Models;
using System.Windows.Controls;
using System.Windows.Input;
using AcademyManager.UCViews;
using Flattinger.UI.ToastMessage.Controls;
using Flattinger.Core.Theme;
using Flattinger.UI.ToastMessage;

namespace AcademyManager.Viewmodels
{
    public class SubjectRegisterVM : BaseViewModel
    {
        #region Commands
        public ICommand BackCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        #endregion

        #region Properties
        private MainVM GParentVM { get; set; }
        private SubjectListVM ParentVM { get; set; }
        private AppTheme _theme;
        private ToastProvider _toastProvider;
        private string _cid;
        public string CID
        {
            get { return _cid; }
            set { _cid = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public void ShowNotification(bool success, int ex)
        {
            if (success)
                _toastProvider.NotificationService.AddNotification(Flattinger.Core.Enums.ToastType.SUCCESS, "Thành công!", "Đăng ký môn thành công.", 1000);
            else
                if (ex == 1) _toastProvider.NotificationService.AddNotification(Flattinger.Core.Enums.ToastType.ERROR, "Thất bại!", "Đăng ký môn thất bại.", 1000);
                else _toastProvider.NotificationService.AddNotification(Flattinger.Core.Enums.ToastType.ERROR, "Thất bại!", "Đăng ký trùng lịch học.", 1000);
        }
        private async Task LoadAvailableCourses(StackPanel panel)
        {
            if (panel != null) panel.Children.Clear();
            DatabaseManager db = new DatabaseManager();
            string termid = await db.GetCurrentTermAsync();
            Course course = await db.GetCourseAsync(termid, CID);
            if (course == null) return;
            foreach (Class c in course.Classes.Values)
            {
                bool contain = MainVM.CurrentUser.StudyElements.Any(e => e.TermID == c.TermID && e.CourseID == c.CourseID && e.ClassID == c.ClassID);
                SubjectRegisterUC item;
                if (!contain) item = new SubjectRegisterUC(c, MaterialDesignThemes.Wpf.PackIconKind.PencilBox, this);
                else item = new SubjectRegisterUC(c, MaterialDesignThemes.Wpf.PackIconKind.BoxCancel, this);
                panel.Children.Add(item);
            }
        }
        private void InitializeCommands()
        {
            BackCommand = new RelayCommand<object>(p => true, p =>
            {
                GParentVM.CurrentView = GParentVM.CourseListView;
                ParentVM.LoadClasses();
            });

            SearchCommand = new RelayCommand<StackPanel>(p => _cid != null && _cid.Length > 0, async p =>
            {
                await LoadAvailableCourses(p);
            });
        }
        #endregion
        public SubjectRegisterVM(MainVM vm, SubjectListVM p, NotificationContainer container)
        {
            GParentVM = vm;
            ParentVM = p;
            _theme = new AppTheme();
            _theme.ChangeTheme(Flattinger.Core.Enums.Theme.LIGHT);
            _toastProvider = new ToastProvider(container);
            InitializeCommands();
        }
    }
}
