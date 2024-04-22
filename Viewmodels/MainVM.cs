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

        // *Dung*
        public UserControl LectureInforView { get; set; }//Login Screen
        public UserControl LectureMainScreenView { get; set; }//Login Screen
        public UserControl LectureSubjectListView { get; set; }
        public UserControl WelcomeView { get; set; }
        public UserControl WelcomeTeacherView { get; set; }
        public UserControl RegisterView { get; set; }
        public UserControl StudyScheduleView { get; set; }


        // Sam
        public UserControl StudentInforView { get; set; } // Thông tin học sinh
        public UserControl StudentMainScreenView { get; set; } // trở lại trang Thông tin học sinh
        public UserControl StudentSubjectListView { get; set; } //Xem danh sách môn học
        public UserControl SubjectRegisterView { get; set; } //Xem Đăng ký môn học
        public UserControl RegisterSuccessView { get; set; } //Xem Đăng ký thành công
        public UserControl ResultView { get; set; } //Xem kết quả
        public UserControl ExamScheduleView { get; set; } // xem lich thi
        //sam
        //teacher
        private bool _isTeacher;
        public bool IsTeacher
        {
            get { return _isTeacher; }
            set
            {
                if (_isTeacher != value)
                {
                    _isTeacher = value;
                    OnPropertyChanged("IsTeacher");
                }
            }
        }
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

            //sam
            StudentInforView = new AcademyManager.Views.StudentInfor(); //thông tin học sinh
            StudentMainScreenView = new AcademyManager.Views.StudentMainScreen(); //trở lại trang thông tin học sinh
            StudentSubjectListView = new AcademyManager.Views.StudentSubjectList(); //Xem danh sách môn học
            SubjectRegisterView = new AcademyManager.Views.SubjectRegister(); //xem Đăng ký môn học
            RegisterSuccessView = new AcademyManager.Views.NotiRegisterSuccess(); //xem Đăng ký thành công
            ResultView = new AcademyManager.Views.Result();  // xem kết quả
            ExamScheduleView = new AcademyManager.Views.ExamSchedule();//xem lich thi
            //sam
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
            // sam


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
            CurrentView = HomeView;
        }
    }
}