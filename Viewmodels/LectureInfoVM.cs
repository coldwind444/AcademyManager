using AcademyManager.Views;
using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class LectureInfoVM : BaseViewModel
    {
        #region Commands
        public ICommand UpdateInfoCommand { get; set; }
        public ICommand BackCommand { get; set; }
        #endregion

        #region Properties
        private MainVM ParentVM { get; set; }
        private string _fullname;
        private string _id;
        private string _email;
        private string _faculty;
        private string _major;
        private string _avatar;
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; OnPropertyChanged(); }
        }
        public string Fullname
        {
            get { return _fullname; }
            set { _fullname = value; OnPropertyChanged(); }
        }
        public string ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }
        public string Faculty
        {
            get { return _faculty; }
            set { _faculty = value; OnPropertyChanged(); }
        }
        public string Major
        {
            get { return _major; }
            set { _major = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        private void LoadInfo()
        {
            // Get the current account from MainVM
            // Assign its info to the Binding propeties
        }
        private void InitializeCommand()
        {
            UpdateInfoCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                
            });

            BackCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                ParentVM.CurrentView = ParentVM.HomeView;
            });
        }
        #endregion
        public LectureInfoVM(MainVM vm)
        {
            ParentVM = vm;
            InitializeCommand();
        }
    }
}
