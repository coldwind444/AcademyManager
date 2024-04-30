using AcademyManager.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AcademyManager.Views;

namespace AcademyManager.Viewmodels
{
    public class MainVM : BaseViewModel
    {
        #region Commands
        public ICommand HomeNavigateCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand NotificationCommand { get; set; }
        public ICommand CloseCommand { get; set; } 
        public ICommand MinimizeCommand { get; set; }
        #endregion
        #region Properties
        // current account
        public static Account CurrentAccount { get; set; }
        public static User CurrentUser { get; set; }
        public static List<Class> UserClassList { get; set; }

        // authentication
        public UserControl WhoAreYouView { get; set; }
        public UserControl WelcomeView { get; set; }
        public UserControl LoginView { get; set; }
        public UserControl SigninView { get; set; }
        public UserControl ResetPWView { get; set; }

        // app 
        // general
        public UserControl HomeView { get; set; }
        public UserControl InfoView { get; set; }
        public UserControl DailyScheduleView { get; set; }
        public UserControl CourseListView { get; set; }
        public UserControl CourseContent {  get; set; }

        // student
        public UserControl CourseRegisterView { get; set; }
        public UserControl ResultView { get; set; }
        public UserControl ExamScheduleView { get; set; } 
        public UserControl NotificationView { get; set; } 


        // current view
        private UserControl _currentView;
        private Visibility _navBtnV;
        private bool _atnoticficationpage;
        private bool _atinfopage;
        private bool _athomepage;
        private bool _islogout;
        private Visibility _noti;
        public Visibility NewNotifications
        {
            get { return _noti; }
            set { _noti = value; OnPropertyChanged(); }
        }
        public Visibility NavigationButtonV
        {
            get { return _navBtnV; }
            set { _navBtnV = value; OnPropertyChanged(); }
        }
        public bool IsLogout
        {
            get { return _islogout; }
            set { _islogout = value; OnPropertyChanged(); }
        }
        public bool AtNotificationPage
        {
            get { return _atnoticficationpage; }
            set { _atnoticficationpage = value; OnPropertyChanged(); }
        }
        public bool AtInfoPage
        {
            get { return _atinfopage; }
            set { _atinfopage = value; OnPropertyChanged(); }
        }
        public bool AtHomePage
        {
            get { return _athomepage; }
            set { _athomepage = value; OnPropertyChanged(); }
        }
        public UserControl CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        // Tab bar
        private Visibility _tabbarV;
        private string _url;
        public string URL
        {
            get { return _url; }
            set { _url = value; OnPropertyChanged(); }
        }
        public Visibility TabbarV
        {
            get { return _tabbarV; }
            set { _tabbarV = value; OnPropertyChanged(); }
        }
        #endregion
        #region Methods
        private void SetNotificationDot()
        {
            if (CurrentUser == null) return;
            if (CurrentUser.Notifications != null)
            {
                if (CurrentUser.Notifications.Count > 0) NewNotifications = Visibility.Visible;
                else NewNotifications = Visibility.Hidden;
            } else
            {
                NewNotifications = Visibility.Hidden;
            }
        }
        private void InitializeCommands()
        {
            HomeNavigateCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                AtHomePage = true;
                AtNotificationPage = false;
                CurrentView = HomeView;
                SetNotificationDot();
            });

            LogoutCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                CurrentView = WhoAreYouView;
                NavigationButtonV = Visibility.Hidden;
                IsLogout = false;
                AtHomePage = false;
                AtInfoPage = false;
            });

            NotificationCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                AtHomePage = false;
                AtNotificationPage = true;
                if (NotificationView == null)
                {
                    NotificationView = new Noti(this);
                    CurrentView = NotificationView;
                } else 
                    CurrentView = NotificationView;
            });

            CloseCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                p.Close();
            });

            MinimizeCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                p.WindowState = WindowState.Minimized;
            });
        }
        #endregion
        public MainVM()
        {
            InitializeCommands();

            // first view 
            if (WhoAreYouView == null)
            {
                WhoAreYouView = new WhoAreYou(this);
                CurrentView = WhoAreYouView;
            } else
                CurrentView = WhoAreYouView;

            // init state
            AtNotificationPage = false;
            AtHomePage = false;
            AtInfoPage = false;
            IsLogout = false;
            NavigationButtonV = Visibility.Hidden;
            SetNotificationDot();
        }
    }
}