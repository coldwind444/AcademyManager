﻿using AcademyManager.AdminViews;
using AcademyManager.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AcademyManager.AdminViewmodels
{
    public class AdminWindowVM : BaseViewModel
    {
        #region Commands
        public ICommand CloseCommand { get; set; }
        public ICommand AddCoursesCommand { get; set; }
        public ICommand AddUsersCommand { get; set; }
        public ICommand SearchUsersCommand { get; set; }
        #endregion
        #region Properties
        private AddCoursesUC _addcoursesView;
        private UserControl _currentView;
        private string _currentUrl;
        private bool _addcourses;
        private bool _addusers;
        private bool _searchusers;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        public bool AddCourses
        {
            get { return _addcourses; }
            set { _addcourses = value; OnPropertyChanged(); }
        }
        public bool AddUsers
        {
            get { return _addusers; }
            set { _addusers = value; OnPropertyChanged(); }
        }
        public bool SearchUsers
        {
            get { return _searchusers; }
            set { _searchusers = value; OnPropertyChanged(); }
        }
        public string CurrentUrl
        {
            get { return _currentUrl; }
            set { _currentUrl = value; OnPropertyChanged(); }
        }
        #endregion
        #region Methods
        private void InitializeCommands()
        {
            CloseCommand = new RelayCommand<AdminWindow>(p => { return true; }, p =>
            {
                p.Close();
            });

            AddCoursesCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                if (_addcoursesView != null) CurrentView = _addcoursesView;
                else
                {
                    _addcoursesView = new AddCoursesUC();
                    CurrentView = _addcoursesView;
                }
                AddUsers = false;
                SearchUsers = false;
            });

            AddUsersCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                AddCourses = false;
                SearchUsers = false;
            });

            SearchUsersCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                AddUsers = false;
                AddCourses = false;
            });
        }
        #endregion
        public AdminWindowVM()
        {
            InitializeCommands();
        }
    }
}
