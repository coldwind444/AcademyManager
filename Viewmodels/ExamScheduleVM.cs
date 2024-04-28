using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class ExamScheduleVM : BaseViewModel
    {
        #region Commands
        public ICommand BackCommand { get; set; }
        #endregion
        #region Properties
        private MainVM ParentVM { get; set; }
        #endregion
        #region Methods
        private void InitializeCommand()
        {
            BackCommand = new RelayCommand<object>(p => true, p =>
            {
                ParentVM.CurrentView = ParentVM.HomeView;
            });
        }
        #endregion
        public ExamScheduleVM(MainVM vm)
        {
            ParentVM = vm;
            InitializeCommand();
        }
    }
}
