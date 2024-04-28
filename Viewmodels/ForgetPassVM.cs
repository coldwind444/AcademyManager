using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class ForgetPassVM : BaseViewModel
    {
        #region Commands
        public ICommand BackCommand { get; set; }
        #endregion
        #region Properties
        private MainVM ParentVM { get; set; }
        #endregion
        #region Methods
        private void InitializeCommands()
        {
            BackCommand = new RelayCommand<object>(p => true, p =>
            {
                ParentVM.CurrentView = ParentVM.WhoAreYouView;
            });
        }
        #endregion
        public ForgetPassVM(MainVM vm)
        {
            ParentVM = vm;
        }
    }
}
