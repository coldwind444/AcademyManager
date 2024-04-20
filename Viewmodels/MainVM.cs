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

namespace AcademyManager.Viewmodels
{
    public class MainVM : BaseViewModel
    {
        #region Commands
        public ICommand HomeNavigateCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand NotificationCommand { get; set; }
        public ICommand CloseCommand { get; set; }  // Thêm ExitCommand vào phần khai báo
        #endregion

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
        private void InitializeCommands()
        {
            HomeNavigateCommand = new RelayCommand<object>(p => true, p =>
            {
                if (HomeView != null) CurrentView = HomeView;
                else
                {
                    // create new home
                    CurrentView = HomeView;
                }
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
        }
        #endregion

        public MainVM()
        {
            InitializeCommands();
        }
    }
}