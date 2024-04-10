using AcademyManager.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class StudentResultVM : BaseViewModel
    {
        #region Commands
        public ICommand BackCommand { get; set; }
        #endregion

        #region Properties
        #endregion

        #region Methods
        private void InitializeCommand()
        {
            BackCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                MainVM vm = p.DataContext as MainVM;
                vm.CurrentView = vm.ResultView_Semester;
            });
        }
        #endregion
        public StudentResultVM()
        {
            InitializeCommand();
        }
    }
}
