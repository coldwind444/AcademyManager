﻿using System.Windows.Controls;
using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class StudentResultVM : BaseViewModel
    {
        #region Commands
        public ICommand BackCommand { get; set; }
        #endregion

        #region Properties
        private MainVM ParentVM { get; set; }
        private List<ListViewItem> _resultList;
        private int _semestercredits;
        private int _totalcredits;
        private double _semestergpa;
        private double _totalgpa;
        public List<ListViewItem> ResultList
        {
            get { return _resultList; }
            set { _resultList = value; OnPropertyChanged(); }
        }
        public int SemesterCredits
        {
            get { return _semestercredits; }
            set { _semestercredits = value; OnPropertyChanged(); }
        }
        public int TotalCredits
        {
            get { return _totalcredits; }
            set { _totalcredits = value; OnPropertyChanged(); }
        }
        public double SemesterGPA
        {
            get { return _semestergpa; }
            set { _semestergpa = value; OnPropertyChanged(); }
        }
        public double TotalGPA
        {
            get { return _totalgpa; }
            set { _totalgpa = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        private void LoadData()
        {
            // Get data from Current Account in Main VM
            // Create list of ListViewItems with user control that represent subject
            // Load all info
        }
        private void InitializeCommand()
        {
            BackCommand = new RelayCommand<object>(p => true, p =>
            {
                ParentVM.CurrentView = ParentVM.HomeView;
            });
        }
        #endregion
        public StudentResultVM(MainVM vm)
        {
            ParentVM = vm;
            InitializeCommand();
        }
    }
}
