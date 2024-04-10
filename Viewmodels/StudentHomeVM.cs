using AcademyManager.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class StudentHomeVM : BaseViewModel
    {
        #region Command
        public ICommand ScheduleCommand { get; set; }
        public ICommand SubjectCommand { get; set; }
        public ICommand InfoCommand { get; set; }
        public ICommand ResultCommand { get; set; }
        #endregion

        #region Properties
        #endregion

        #region Methods
        private void InitializeCommand()
        {
            ScheduleCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                MainVM? vm = p.DataContext as MainVM;
                if (vm.ScheduleView != null) vm.CurrentView = vm.ScheduleView;
                else
                {
                    // create new then assign to Current View
                }
            });

            SubjectCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                MainVM? vm = p.DataContext as MainVM;
                if (vm.CourseView != null) vm.CurrentView = vm.CourseView;
                else
                {
                    // create new then assign to Current View
                }
            });

            InfoCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                MainVM? vm = p.DataContext as MainVM;
                if (vm.InfoView != null) vm.CurrentView = vm.InfoView;
                else
                {
                    // create new then assign to Current View
                }
            });

            ResultCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                MainVM vm = p.DataContext as MainVM;
                if (vm.ResultView_Semester != null) vm.CurrentView = vm.ResultView_Semester;
                else
                {
                    // create new then assign to Current View
                }
            });
        }
        #endregion
        public StudentHomeVM()
        {
            InitializeCommand();
        }
    }
}
