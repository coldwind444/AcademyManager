using AcademyManager.Models;
using AcademyManager.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AcademyManager.Views;

namespace AcademyManager.Viewmodels
{
    public class MainVM : BaseViewModel
    {
        public ICommand HomeNavigateCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand NotificationCommand { get; set; }
        public ICommand CloseCommand { get; set; } //Close App
        public ICommand MinimizeCommand { get; set; }//Minimize App
        public ICommand LoginCommand { get; set; }//Login Screen

        #region Properties
        // current account
        public Account CurrentAccount { get; set; }
        // authentication pages
        public UserControl RootView { get; set; }
        public UserControl LoginView { get; set; }
        public UserControl SigninView { get; set; }
        public UserControl SigninSuccessView { get; set; }
        public UserControl SigninFailureView { get; set; }
        public UserControl TypeSelectionView { get; set; }
        public UserControl ResetPWView { get; set; }
        public UserControl EmailExistView { get; set; }
        // app pages
        public UserControl HomeView { get; set; }
        public UserControl InfoView { get; set; }
        public UserControl UpdateInfoView { get; set; }
        public UserControl ScheduleView { get; set; }
        public UserControl ResultView_Semester { get; set; }
        public UserControl ResultView_Show { get; set; }
        public UserControl CourseView { get; set; }
        public UserControl CourseRegisterView { get; set; }
        public UserControl CourseInfoView { get; set; }
        // current view
        private UserControl _currentView;
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
        private void InitializeViews()
        {
            // Khởi tạo một lần và sử dụng lại các views
            HomeView = new AcademyManager.Views.WelcomeScreen();
            LoginView = new AcademyManager.Views.Login();  // Đảm bảo rằng bạn có UserControl này trong project của bạn
                                                           // Khởi tạo các views khác...
        }
        private void InitializeCommands()
        {
            HomeNavigateCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = HomeView;
            });
            LogoutCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = RootView;
            });
            NotificationCommand = new RelayCommand<object>(p => true, p =>
            {
                // Logic for notification here
            });

            CloseCommand = new RelayCommand<object>(p => true, p =>
            {
                Application.Current.Shutdown();  // Lệnh để tắt ứng dụng
            });

            MinimizeCommand = new RelayCommand<object>(p => true, p =>
            {
                MinimizeApplication();
            });

            LoginCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = LoginView;
            });
        }
        #endregion
        private void MinimizeApplication()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var mainWindow = Application.Current.MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.WindowState = WindowState.Minimized;
                }
            });           
        }
        public MainVM()
        {
            InitializeViews();
            InitializeCommands();
            CurrentView = new WelcomeScreen();
        }
    }
}