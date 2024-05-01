using AcademyManager.Models;
using AcademyManager.Views;
using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class CourseInfoVM : BaseViewModel
    {
        #region Commands
        public ICommand ViewStudentListCommand { get; set; }
        public ICommand UpdateScoreCommand { get; set; }
        public ICommand AddDocumentCommand { get; set; }
        public ICommand BackCommand { get; set; }
        #endregion
        #region Properties
        public Class Data {  get; set; }
        private MainVM GGParentVM { get; set; }
        private SubjectListVM GParentVM { get; set; }
        
        #endregion
        #region Methods
        private void InitializeCommands()
        {
            ViewStudentListCommand = new RelayCommand<object>(p => true, p =>
            {
                StudentListWindow window = new StudentListWindow(Data);
                window.ShowDialog();
            });

            UpdateScoreCommand = new RelayCommand<object>(p => true, p =>
            {
                ScoreUpdateWindow window = new ScoreUpdateWindow(Data);
                window.ShowDialog();
            });

            AddDocumentCommand = new RelayCommand<object>(p => true, p =>
            {
                DocumentsUploadWindow window = new DocumentsUploadWindow(Data);
                window.ShowDialog();
            });

            BackCommand = new RelayCommand<object>(p => true, p =>
            {
                GGParentVM.CurrentView = GGParentVM.CourseListView;
            });
        }
        #endregion
        public CourseInfoVM(Class data, MainVM vm, SubjectListVM p)
        {
            Data = data;
            GGParentVM = vm;
            GParentVM = p;
            InitializeCommands();
        }
    }
}
