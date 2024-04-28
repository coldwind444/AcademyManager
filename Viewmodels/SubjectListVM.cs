using AcademyManager.Viewmodel;
using AcademyManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class SubjectListVM : BaseViewModel
    {
        #region Commands
        public ICommand BackCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
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

            RegisterCommand = new RelayCommand<object>(p => true, p =>
            {
                if (ParentVM.CourseRegisterView == null)
                {
                    ParentVM.CourseRegisterView = new SubjectRegister();
                    ParentVM.CurrentView = ParentVM.CourseRegisterView;
                } else
                    ParentVM.CurrentView = ParentVM.CourseRegisterView;
            });
        }
        #endregion
        public SubjectListVM(MainVM vm)
        {
            ParentVM = vm;
            InitializeCommands();
        }
    }
}
