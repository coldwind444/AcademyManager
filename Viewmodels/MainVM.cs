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
        #region Commands
        public ICommand HomeNavigateCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand NotificationCommand { get; set; }
        public ICommand CloseCommand { get; set; } //Close App
        public ICommand MinimizeCommand { get; set; }//Minimize App
        public ICommand LoginCommand { get; set; }//Login Screen
        public ICommand ConfirmCommand { get; set; }//Home
        public ICommand LectureInforCommand { get; set; }//Information of lecture Screen
        public ICommand LectureCommand { get; set; }//Lecture Home
        public ICommand LectureSubjectListCommand { get; set; }//List Subject of lecture
        public ICommand WelcomeCommand { get; set; } //Sign in / Sign up
        public ICommand WelcomeTeacherCommand { get; set; } //Sign in / Sign up (IsTeacher = True)
        public ICommand RegisterCommand { get; set; } //Sign up Screen
        public ICommand StudentInforCommand { get; set; } //Information of Student Screen
        public ICommand StudentMainScreenCommand { get; set; } //Student Home 
        public ICommand StudentSubjectListCommand { get; set; } //List subject
        public ICommand SubjectRegisterCommand { get; set; } //List subject to register
        public ICommand RegisterSuccessCommand { get; set; } //Register successfully
        public ICommand ResultCommand { get; set; } //Study Result
        public ICommand ExamScheduleCommand { get; set; } //Exam schedule
        public ICommand StudyScheduleCommand { get; set; } //Calendar
        public ICommand ForgetPassCommand { get; set; } //Forget Password
        public ICommand ForgetPass2Command { get; set; } //Input code
        public ICommand SetNewPassCommand { get; set; } //Set new password
        public ICommand HomeViewCommand { get; set; } //HomeView
        public ICommand CloseUserControlCommand { get; set; } //Close UserControl
        public ICommand InformationCommand { get; set; } //Information
        public ICommand WhoAreYouCommand { get; set; }// Lecture or student
        #endregion
        #region Properties
        // current account
        public Account CurrentAccount { get; set; }
        // authentication pages
        public UserControl RootView { get; set; }
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
        public UserControl LoginView { get; set; }
        public UserControl ConfirmView { get; set; }
        public UserControl LectureInforView { get; set; }//Information
        public UserControl LectureMainScreenView { get; set; }//Lecture HomeView
        public UserControl LectureSubjectListView { get; set; }
        public UserControl WelcomeView { get; set; }
        public UserControl WelcomeTeacherView { get; set; }
        public UserControl RegisterView { get; set; }
        public UserControl StudyScheduleView { get; set; }
        public UserControl ForgetPassView { get; set; }
        public UserControl ForgetPass2View { get; set; }
        public UserControl SetNewPassView { get; set; }
        public UserControl StudentInforView { get; set; } // Thông tin học sinh
        public UserControl StudentMainScreenView { get; set; } // trở lại trang Thông tin học sinh
        public UserControl StudentSubjectListView { get; set; } //Xem danh sách môn học
        public UserControl SubjectRegisterView { get; set; } //Xem Đăng ký môn học
        public UserControl RegisterSuccessView { get; set; } //Xem Đăng ký thành công
        public UserControl ResultView { get; set; } //Xem kết quả
        public UserControl ExamScheduleView { get; set; } // xem lich thi
        public UserControl NotificationView { get; set; } // xem lich thi
        public UserControl InformationView { get; set; } // xem lich thi
        public UserControl WhoAreYouView { get; set; } // xem lich thi
        
        // current view
        private UserControl _currentView;
        private Visibility _navBtnV;
        private bool _atnoticficationpage;
        private bool _atinfopage;
        private bool _athomepage;
        private bool _islogout;
        private bool _isTeacher;

        public bool IsTeacher
        {
            get { return _isTeacher; }
            set
            {
                if (_isTeacher != value)
                {
                    _isTeacher = value;
                    OnPropertyChanged();
                }
            }
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
        private void InitializeViews()
        {
            // Khởi tạo một lần và sử dụng lại các views
            HomeView = new AcademyManager.Views.WhoAreYou();
            LoginView = new AcademyManager.Views.Login();
            ConfirmView = new AcademyManager.Views.WelcomeScreen();
            LectureInforView = new AcademyManager.Views.LectureInfor();
            LectureMainScreenView = new AcademyManager.Views.LectureMainScreen();
            LectureSubjectListView = new AcademyManager.Views.LectureSubjectList();
            WelcomeView = new AcademyManager.Views.WelcomeScreen();
            WelcomeTeacherView = new AcademyManager.Views.WelcomeScreen();
            RegisterView = new AcademyManager.Views.SetNewPass();
            StudyScheduleView = new AcademyManager.Views.StudySchedule();
            ForgetPassView = new AcademyManager.Views.ForgetPass();
            ForgetPass2View = new AcademyManager.Views.ForgetPass2();
            SetNewPassView = new AcademyManager.Views.SetNewPass();
            NotificationView = new AcademyManager.Views.Noti();
            WhoAreYouView = new AcademyManager.Views.WhoAreYou();
            StudentInforView = new AcademyManager.Views.StudentInfor(); //thông tin học sinh
            StudentMainScreenView = new AcademyManager.Views.StudentMainScreen(); //trở lại trang thông tin học sinh
            StudentSubjectListView = new AcademyManager.Views.StudentSubjectList(); //Xem danh sách môn học
            SubjectRegisterView = new AcademyManager.Views.SubjectRegister(); //xem Đăng ký môn học
            RegisterSuccessView = new AcademyManager.Views.NotiRegisterSuccess(); //xem Đăng ký thành công
            ResultView = new AcademyManager.Views.Result();  // xem kết quả
            ExamScheduleView = new AcademyManager.Views.ExamSchedule();//xem lich thi
        }
        private void InitializeCommands()
        {
            HomeNavigateCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                AtHomePage = true;
                AtInfoPage = false;
                AtNotificationPage = false;
                IsLogout = false;
                if (IsTeacher != true)
                {
                    HomeView = StudentMainScreenView;
                    CurrentView = StudentMainScreenView;
                }
                else {
                    HomeView = StudentMainScreenView;
                    CurrentView = LectureMainScreenView;
                }      
            });

            HomeViewCommand = new RelayCommand<object>(p => true, p =>
            {
                if (IsTeacher != true)
                    CurrentView = StudentMainScreenView;
                else CurrentView = LectureMainScreenView;
            });

            LogoutCommand = new RelayCommand<object>(p => true, p =>
            {
                AtHomePage = false;
                AtInfoPage = false;
                IsTeacher = false;
                AtNotificationPage = false;
                IsLogout = true;
                CurrentView = WhoAreYouView;
            });

            NotificationCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                AtHomePage = false;
                AtInfoPage = false;
                IsLogout = false;
                AtNotificationPage = true;
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

            LoginCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = LoginView;
            });

            ConfirmCommand = new RelayCommand<object>(p => true, p =>
            {
                if (IsTeacher != true)
                    CurrentView = StudentMainScreenView;
                else CurrentView = LectureMainScreenView;
            });

            LectureInforCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = LectureInforView;
            });

            LectureCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = LectureMainScreenView;
            });

            LectureSubjectListCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = LectureSubjectListView;
            });

            WelcomeCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = WelcomeView;
            });

            WelcomeTeacherCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = WelcomeView;
                IsTeacher = true;
            });

            RegisterCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = RegisterView;
            });

            ForgetPassCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = ForgetPassView;
            });

            ForgetPass2Command = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = ForgetPass2View;
            });

            SetNewPassCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = SetNewPassView;
            });
            InformationCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                AtInfoPage = true;
                AtHomePage = false;
                AtNotificationPage = false;
                IsLogout = false;
                if (IsTeacher != true)
                    CurrentView = StudentInforView;
                else CurrentView = LectureInforView;
            });
            WhoAreYouCommand = SetNewPassCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = WhoAreYouView;
                IsTeacher = false;
            });

            //sam
            StudentInforCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = StudentInforView;
            });

            StudentSubjectListCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = StudentSubjectListView;
            });

            StudentMainScreenCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = StudentMainScreenView;
            });

            SubjectRegisterCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = SubjectRegisterView;
            });

            RegisterSuccessCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = RegisterSuccessView;
            });

            ResultCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = ResultView;
            });

            ExamScheduleCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = ExamScheduleView;
            });

            StudyScheduleCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentView = StudyScheduleView;
            });

            CloseUserControlCommand = new RelayCommand<object>(
                execute: p => CloseUserControl(p as UserControl),
                canExecute: p => true
            );

        }
        #endregion
        private void CloseUserControl(UserControl controlToClose)
        {
            if (controlToClose != null)
                controlToClose.Visibility = Visibility.Collapsed;
        }
        public MainVM()
        {
            InitializeViews();
            InitializeCommands();
            CurrentView = new AcademyManager.Views.WhoAreYou();
            AtNotificationPage = false;
            AtHomePage = false;
            AtInfoPage = false;
            IsLogout = false;
            NavigationButtonV = Visibility.Hidden;
        }
    }
}