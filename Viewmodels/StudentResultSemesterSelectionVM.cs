using AcademyManager.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AcademyManager.Views;

namespace AcademyManager.Viewmodels
{
    public class StudentResultSemesterSelectionVM : BaseViewModel
    {
        #region Commands
        public ICommand SemesterSelectionCommand { get; set; }
        public ICommand BackCommand { get; set; }
        #endregion

        #region Properties
        #endregion

        #region Methods
        private void InitializeCommand()
        {
            SemesterSelectionCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {

            });

            BackCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                MainVM vm = p.DataContext as MainVM;
                vm.CurrentView = vm.HomeView;
            });
        }
        #endregion
        public StudentResultSemesterSelectionVM()
        {
            InitializeCommand();
        }
    }
}
