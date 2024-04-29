﻿using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using AcademyManager.Views;
using AcademyManager.Models;

namespace AcademyManager.Viewmodels
{
    public class LoginVM : BaseViewModel
    {
        #region Commands
        public ICommand EmailBoxTextChangedCommand { get; set; }
        public ICommand PasswordBoxTextChangedCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand ForgetPassCommand { get; set; }
        #endregion
        #region Properties
        private MainVM ParentVM { get; set; }
        private int type;
        private string _notilb;
        private string _userid;
        private string _password;
        private PasswordBox _passwordBox;
        private Visibility _load;
        private Visibility _notiV;
        public Visibility NotiV
        {
            get { return _notiV; }
            set { _notiV = value; OnPropertyChanged(); }
        }
        public Visibility Loading
        {
            get { return _load; }
            set { _load = value; OnPropertyChanged(); }
        }
        public string NotiLabel
        {
            get { return _notilb; }
            set { _notilb = value; OnPropertyChanged(); }
        }
        public string UserID
        {
            get { return _userid; }
            set { _userid = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        private void ResetAll()
        {
            UserID = "";
            if (_passwordBox != null) _passwordBox.Clear();
            NotiV = Visibility.Hidden;
        }
        private bool NullOrEmpty(string s)
        {
            return (s == null || s == String.Empty);
        }
        private void InitializeCommands()
        {
            PasswordBoxTextChangedCommand = new RelayCommand<PasswordBox>(p => { return true; }, p =>
            {
                _passwordBox = p;
                _password = p.Password;
            });

            LoginCommand = new RelayCommand<MainWindow>(p => { return !NullOrEmpty(_userid) && !NullOrEmpty(_password); }, async p =>
            {
                Loading = Visibility.Visible;
                DatabaseManager database = new DatabaseManager();
                Account acc = await database.GetAccountAsync(_userid, type);

                if (acc != null)
                {
                    if (acc.Match(_password, _userid))
                    {
                        if (type == 1)
                        {
                            MainVM.CurrentAccount = acc;
                            MainVM.CurrentUser = await database.GetInstructorAsync(_userid);
                            ParentVM.HomeView = new LectureMainScreen(ParentVM);
                            ParentVM.CurrentView = ParentVM.HomeView;
                        } else
                        {
                            MainVM.CurrentAccount = acc;
                            MainVM.CurrentUser = await database.GetStudentAsync(_userid);
                            ParentVM.HomeView = new StudentMainScreen(ParentVM);
                            ParentVM.CurrentView = ParentVM.HomeView;
                        }
                        ResetAll();
                        ParentVM.NavigationButtonV = Visibility.Visible;
                    }
                    else
                    {
                        Loading = Visibility.Hidden;
                        if (acc.IsActivated()) NotiLabel = "Sai mật khẩu.";
                        else NotiLabel = "Tài khoản chưa được kích hoạt.";
                        NotiV = Visibility.Visible;
                        await Task.Delay(1500);
                        NotiV = Visibility.Hidden;
                    }
                } else
                {
                    Loading = Visibility.Hidden;
                    NotiLabel = "Tài khoản không tồn tại.";
                    NotiV = Visibility.Visible;
                    await Task.Delay(1500);
                    NotiV = Visibility.Hidden;
                }
                Loading = Visibility.Hidden;
            });

            BackCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                ParentVM.CurrentView = ParentVM.WelcomeView;
                ResetAll();
            });

            ForgetPassCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                ParentVM.ResetPWView = new ForgetPass(type, ParentVM);
                ParentVM.CurrentView = ParentVM.ResetPWView;
            });
        }
        #endregion
        public LoginVM(int t, MainVM vm)
        {
            ParentVM = vm;
            type = t;
            Loading = Visibility.Hidden;
            NotiV = Visibility.Hidden;
            InitializeCommands();
        }
    }
}
