using System.Windows.Controls;
using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class SetPasswordVM : BaseViewModel
    {
        #region Commands
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand ConfirmPasswordChangedCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand BackCommand { get; set; }
        #endregion
        #region Properties
        private MainVM ParentVM { get; set; }
        private string _userid;
        private string _email;
        public string UserID
        {
            get { return _userid; } 
            set { _userid = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }
        #endregion
        #region Methods
        private void InitializeCommands()
        {
            PasswordChangedCommand = new RelayCommand<PasswordBox>(p => { return true; }, p =>
            {

            });

            ConfirmPasswordChangedCommand = new RelayCommand<PasswordBox>(p => { return true; }, p =>
            {

            });

            ConfirmCommand = new RelayCommand<object>(p => { return true; }, p =>
            {

            });


            BackCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                ParentVM.CurrentView = ParentVM.WelcomeView;
            });
        }
        #endregion

        public SetPasswordVM(MainVM vm)
        {
            InitializeCommands();
            ParentVM = vm;
        }
    }
}
