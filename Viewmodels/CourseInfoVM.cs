using AcademyManager.Models;
using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class CourseInfoVM : BaseViewModel
    {
        #region Commands
        public ICommand ViewStudentListCommand { get; set; }
        public ICommand UpdateScoreCommand { get; set; }
        public ICommand AddDocumentCommand { get; set; }
        #endregion
        #region Properties
        public Class Data {  get; set; }
        
        #endregion
        #region Methods
        private void InitializeCommands()
        {
            ViewStudentListCommand = new RelayCommand<object>(p => true, p =>
            {

            });

            UpdateScoreCommand = new RelayCommand<object>(p => true, p =>
            {

            });

            AddDocumentCommand = new RelayCommand<object>(p => true, p =>
            {

            });
        }
        #endregion
        public CourseInfoVM(Class data)
        {
            Data = data;
            InitializeCommands();
        }
    }
}
