﻿using AcademyManager.Models;
using AcademyManager.Viewmodels;
using AcademyManager.Views;
using System.Windows.Input;

namespace AcademyManager.UCViewmodels
{
    public class SubjectVM : BaseViewModel
    {
        #region Commands
        public ICommand ViewCommand { get; set; }
        #endregion
        #region Properties
        private MainVM GParentVM { get; set; }
        private Class ClassData { get; set; }
        #endregion
        #region Methods
        private void InitializeCommands()
        {
            ViewCommand = new RelayCommand<object>(p => true, p =>
            {
                if (GParentVM.CourseContent == null)
                {
                    if (MainVM.CurrentAccount.Type == 1)
                    {
                        GParentVM.CourseContent = new LectureCourse(ClassData);
                        GParentVM.CurrentView = GParentVM.CourseContent;
                    } else
                    {
                        GParentVM.CourseContent = new StudentCourse(ClassData);
                        GParentVM.CurrentView = GParentVM.CourseContent;
                    }
                }
            });
        }
        #endregion
        public SubjectVM(MainVM p, Class data)
        {
            ClassData = data;
            GParentVM = p;
            InitializeCommands();
        }
    }
}
