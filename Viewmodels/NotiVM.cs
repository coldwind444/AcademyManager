using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class NotiVM : BaseViewModel
    {
        #region Commands
        public ICommand BackCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        #endregion
        #region Properties
        private MainVM ParentVM { get; set; }
        #endregion
        #region Methods
        private void InitializeCommands()
        {
            BackCommand = new RelayCommand<object>(p => true, p =>
            {
                ParentVM.CurrentView = ParentVM.HomeView;
            });

            ReloadCommand = new RelayCommand<object>(p => true, p =>
            {

            });
        }
        #endregion
        public NotiVM(MainVM vm)
        {
            ParentVM = vm;
            InitializeCommands();
        }
    }
}
