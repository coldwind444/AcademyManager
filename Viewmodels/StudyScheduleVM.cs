using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class StudyScheduleVM : BaseViewModel
    {
        #region Commands
        public ICommand BackCommand { get; set; }
        #endregion

        #region Properties
        private MainVM ParentVM {  get; set; }
        #endregion

        #region Methods
        private void InitializeCommands()
        {
            BackCommand = new RelayCommand<object>(p => true, p =>
            {
                ParentVM.HomeNavigateCommand.Execute(null);
            });
        }
        #endregion
        public StudyScheduleVM(MainVM vm)
        {
            ParentVM = vm;
            InitializeCommands();
        }
    }
}
