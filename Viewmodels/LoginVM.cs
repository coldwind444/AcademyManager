using AcademyManager.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using AcademyManager.Models;
using AcademyManager.Views;

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
        private string _userid;
        private string _password;
        private string _notilb;
        private PasswordBox _passwordBox;
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
        private void InitializeCommands()
        {
            PasswordBoxTextChangedCommand = new RelayCommand<PasswordBox>(p => { return true; }, p =>
            {
                _passwordBox = p;
                _password = p.Password;
            });

            LoginCommand = new RelayCommand<MainWindow>(p => { return true; }, async p =>
            {
    //            DatabaseManager database = new DatabaseManager();
    //            Account acc = await database.GetAccountAsync(_userid);

                if (/*acc != null*/ true)
                {
                    if (/*acc.Match(_password, _userid)*/ true)
                    {
                        if (/*acc.Type == 1*/!true)
                        {
                            ParentVM.HomeView = new LectureMainScreen(ParentVM);
                            ParentVM.CurrentView = ParentVM.HomeView;
                        } else
                        {
                            ParentVM.HomeView = new StudentMainScreen(ParentVM);
                            ParentVM.CurrentView = ParentVM.HomeView;
                        }
                    //    _passwordBox.Clear(); 
                    //    UserID = "";
                        ParentVM.NavigationButtonV = Visibility.Visible;
                    }
                    else
                    {
                        NotiLabel = "Sai mật khẩu.";
                        await Task.Delay(1000);
                        NotiLabel = "";
                    }
                } else
                {
                    NotiLabel = "Tài khoản không tồn tại.";
                    await Task.Delay(1000);
                    NotiLabel = "";
                }
                
            });

            BackCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                ParentVM.CurrentView = ParentVM.WelcomeView;
            });

            ForgetPassCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                ParentVM.ResetPWView = new ForgetPass();
                ParentVM.CurrentView = ParentVM.ResetPWView;
            });
        }
        #endregion
        public LoginVM(MainVM vm)
        {
            ParentVM = vm;
            InitializeCommands();
        }
    }
}
