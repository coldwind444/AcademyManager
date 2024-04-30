﻿using AcademyManager.Models;
using System.Windows.Controls;
using System.Windows.Input;
using AcademyManager.UCViews;

namespace AcademyManager.Viewmodels
{
    public class SubjectRegisterVM : BaseViewModel
    {
        #region Commands
        public ICommand BackCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        #endregion

        #region Properties
        private MainVM GParentVM { get; set; }
        private string _cid;
        public string CID
        {
            get { return _cid; }
            set { _cid = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        private async Task LoadAvailableCourses(StackPanel panel)
        {
            DatabaseManager db = new DatabaseManager();
            string termid = await db.GetCurrentTermAsync();
            Course course = await db.GetCourseAsync(termid, CID);
            bool contain = MainVM.CurrentUser.StudyElements.Any(cls => cls.TermID == termid && cls.CourseID == CID);
            if (contain) return;
            foreach (Class c in course.Classes.Values)
            {
                SubjectRegisterUC item = new SubjectRegisterUC(c) ;
                panel.Children.Add(item);
            }
        }
        private void InitializeCommands()
        {
            BackCommand = new RelayCommand<object>(p => true, p =>
            {
                GParentVM.CurrentView = GParentVM.CourseListView;
            });

            SearchCommand = new RelayCommand<StackPanel>(p => _cid != null && _cid.Length > 0, async p =>
            {
                await LoadAvailableCourses(p);
            });
        }
        #endregion
        public SubjectRegisterVM(MainVM vm)
        {
            GParentVM = vm;
            InitializeCommands();
        }
    }
}