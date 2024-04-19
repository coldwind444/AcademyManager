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
        #endregion

        #region Properties
        private TextBox _emailBox;
        private PasswordBox _passwordBox;
        private Visibility _loginBtnV;
        private Visibility _backBtnV;
        private Visibility _loginFailurePanelV;
        public Visibility LoginFailurePanelV
        {
            get { return _loginFailurePanelV; }
            set { _loginFailurePanelV = value; OnPropertyChanged(); }
        }
        public Visibility LoginBtnV
        {
            get { return _loginBtnV; }
            set { _loginBtnV = value; OnPropertyChanged(); }
        }
        public Visibility BackBtnV
        {
            get { return _backBtnV; }
            set { _backBtnV = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        private void InitializeCommands()
        {
            EmailBoxTextChangedCommand = new RelayCommand<TextBox>(p => { return true; }, p =>
            {
                // Sign in button is only show when both password and email are not null or empty string
                _emailBox = p;
                bool validEmail = _emailBox != null && _emailBox.Text != null && _emailBox.Text != String.Empty;
                bool validPassword = _passwordBox != null && _passwordBox.Password != null && _passwordBox.Password != String.Empty;
                if (validEmail && validPassword)
                {
                    LoginBtnV = Visibility.Visible;
                    BackBtnV = Visibility.Hidden;
                }
                else
                {
                    LoginBtnV = Visibility.Hidden;
                    BackBtnV = Visibility.Visible;
                }
            });

            PasswordBoxTextChangedCommand = new RelayCommand<PasswordBox>(p => { return true; }, p =>
            {
                // Sign in button is only show when both password and email are not null or empty string
                _passwordBox = p;
                bool validEmail = _emailBox != null && _emailBox.Text != null && _emailBox.Text != String.Empty;
                bool validPassword = _passwordBox != null && _passwordBox.Password != null && _passwordBox.Password != String.Empty && _passwordBox.Password.Length >= 8;
                if (validEmail && validPassword)
                {
                    LoginBtnV = Visibility.Visible;
                    BackBtnV = Visibility.Hidden;
                }
                else
                {
                    LoginBtnV = Visibility.Hidden;
                    BackBtnV = Visibility.Visible;
                }
            });

            LoginCommand = new RelayCommand<MainWindow>(p => { return true; }, async p =>
            {
                // Check if password is matched
                DatabaseManager database = new DatabaseManager();
                Account acc = await database.GetAccountAsync(_emailBox.Text);
                MainVM vm = p.Viewmodel as MainVM;

                if (acc.Match(_passwordBox.Password, _emailBox.Text))
                {
                    // if password is matched, continue to check account type
                    // if account type = 0 (undefined), type select view will be the next view
                    // if not, navigate to home view
                    if (acc.Type != 0)
                    {
                        vm.CurrentAccount = acc;
                        vm.CurrentView = vm.HomeView;
                        vm.TabbarV = Visibility.Visible;
                    } else
                    {
                        vm.CurrentView = vm.TypeSelectionView;
                    }
                } else
                {
                    LoginFailurePanelV = Visibility.Visible;
                    Task.Delay(1000);
                    LoginFailurePanelV = Visibility.Hidden;
                }
            });
        }
        #endregion
        public LoginVM()
        {
            InitializeCommands();
        }
    }
}
