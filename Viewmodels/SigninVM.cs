using AcademyManager.Viewmodel;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AcademyManager.Models;

namespace AcademyManager.Viewmodels
{
    public class SigninVM : BaseViewModel
    {
        #region Commands
        public ICommand EmailBoxTextChangedCommand { get; set; }
        public ICommand PasswordBoxTextChangedCommand { get; set; }
        public ICommand SigninCommand { get; set; }
        public ICommand BackCommand { get; set; }
        #endregion

        #region Properties
        private TextBox _emailBox;
        private PasswordBox _passwordBox;
        private Visibility _signinBtnV;
        private Visibility _backBtnV;
        public Visibility SigninBtnV
        {
            get { return _signinBtnV; }
            set { _signinBtnV = value; OnPropertyChanged(); }
        }
        public Visibility BackBtnV
        {
            get { return _backBtnV; }
            set { _backBtnV = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        private int ConfirmEmail()
        {
            string senderEmail = Authentication.Email;
            string senderPassword = Authentication.Password;
            string recipientEmail = _emailBox.Text;

            // Email configuration
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587; // Gmail SMTP port
            smtpClient.EnableSsl = true; // Use SSL
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            // Email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderEmail);
            try
            {
                mailMessage.To.Add(recipientEmail);
            }
            catch (FormatException)
            {
                return -1; // Invalid format
            }
            mailMessage.Subject = "Create new account";
            mailMessage.Body = "Your account has been created successfully !";

            try
            {
                // Send the email
                smtpClient.Send(mailMessage);
                return 1; // Success
            }
            catch (Exception ex)
            {
                return 0; // Error sending email
            }
        }
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
                    SigninBtnV = Visibility.Visible;
                    BackBtnV = Visibility.Hidden;
                } else
                {
                    SigninBtnV = Visibility.Hidden;
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
                    SigninBtnV = Visibility.Visible;
                    BackBtnV = Visibility.Hidden;
                }
                else
                {
                    SigninBtnV = Visibility.Hidden;
                    BackBtnV = Visibility.Visible;
                }
            });

            SigninCommand = new RelayCommand<MainWindow>(p => { return true; }, async p =>
            {
                MainVM? vm = p.DataContext as MainVM;

                // Check if email has already used
                DatabaseManager database = new DatabaseManager();
                Account acc = await database.GetAccountAsync(_emailBox.Text);
                if (acc != null)
                {
                   if (vm.EmailExistView != null ) vm.CurrentView = vm.EmailExistView;
                   else
                    {
                        // create new current view
                        //
                        vm.CurrentView = vm.EmailExistView;
                    }
                    return;
                }

                // Switch view
                if (ConfirmEmail() == 1) // success
                {
                    await database.AddAccount(vm.CurrentAccount);
                    if (vm.SigninSuccessView != null) vm.CurrentView = vm.SigninSuccessView;
                    else
                    {
                        // create new success sign in view
                        // assign it to current view in main viewmodel
                    }
                } else 
                {
                    if (vm.SigninFailureView != null) vm.CurrentView = vm.SigninFailureView;
                    else
                    {
                        // create new failure sign in view
                        // assign it to current view in main viewmodel
                    }
                } 
            });
        }
        #endregion
        public SigninVM()
        {
            InitializeCommands();
        }
    }
}
