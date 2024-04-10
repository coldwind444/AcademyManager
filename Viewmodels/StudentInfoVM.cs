using AcademyManager.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class StudentInfoVM : BaseViewModel
    {
        #region Commands
        public ICommand UpdateInfoCommand { get; set; }
        public ICommand BackCommand { get; set; }
        #endregion

        #region Properties
        #endregion

        #region Methods
        private void InitializeCommand()
        {
            UpdateInfoCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                MainVM vm = p.DataContext as MainVM;
                if (vm.UpdateInfoView != null) vm.CurrentView = vm.UpdateInfoView;
                else
                {
                    // create new and assign to Current View
                }
            });

            BackCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                MainVM vm = p.DataContext as MainVM;
                vm.CurrentView = vm.HomeView;
            });
        }
        #endregion
        public StudentInfoVM()
        {
            InitializeCommand();
        }
    }
}
