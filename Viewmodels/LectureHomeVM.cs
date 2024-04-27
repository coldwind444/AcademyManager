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
    public class LectureHomeVM : BaseViewModel
    {
        #region Commands
        public ICommand InfoCommand { get; set; }
        public ICommand CourseCommand { get; set; }
        public ICommand ScheduleCommand { get; set; }
        #endregion
        #region Properties
        private MainVM ParentVM { get; set; }
        #endregion
        #region Methods
        private void InitializeCommands()
        {
            InfoCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                ParentVM.InfoView = new LectureInfor();
                ParentVM.CurrentView = ParentVM.InfoView;
            });

            CourseCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                ParentVM.CourseListView = new LectureSubjectList();
                ParentVM.CurrentView = ParentVM.CourseListView;
            });

            ScheduleCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                ParentVM.DailyScheduleView = new StudySchedule();
                ParentVM.CurrentView = ParentVM.DailyScheduleView;
            });
        }
        #endregion
        public LectureHomeVM(MainVM vm)
        {
            InitializeCommands();
            ParentVM = vm;
        }
    }
}
