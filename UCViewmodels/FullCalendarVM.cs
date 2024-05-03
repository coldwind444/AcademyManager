using AcademyManager.Models;
using AcademyManager.UCViews;
using AcademyManager.Viewmodels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AcademyManager.UCViewmodels
{
    public class FullCalendarVM : BaseViewModel
    {
        #region Commands
        public ICommand SelectedDateChangedCommand { get; set; }
        #endregion
        #region Properties
        private DateTime? _date;
        private string _currdate;
        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
        public DateTime? ScheduleDate
        {
            get { return _date; }
            set 
            { 
                _date = value; 
                OnPropertyChanged(); 
                SelectedDateChangedCommand.Execute(null);
            }
        }
        public string CurrentDate
        {
            get { return _currdate; }
            set { _currdate = value; OnPropertyChanged(); }
        }
        #endregion
        #region Methods
        private void LoadTodayTasks(List<Class> list)
        {
            TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);
            Items = new ObservableCollection<Item>();
            foreach (Class c in list)
            {
                Item item;
                string bg = c.BeginTime.ToString("HH:mm");
                string e = c.EndTime.ToString("HH:mm");
                string time = $"{bg} - {e}";
                if (c.EndTime >= now) item = new Item(c.CourseName, time, c.Room, FontAwesome.WPF.FontAwesomeIcon.CircleOutline);
                else item = new Item(c.CourseName, time, c.Room, FontAwesome.WPF.FontAwesomeIcon.CheckCircle);
                Items.Add(item);
            }
        }
        private void InitializeCommands()
        {
            SelectedDateChangedCommand = new RelayCommand<object>(p => true, p =>
            {
                CurrentDate = ScheduleDate.Value.ToString("MMMM d, yyyy");
                DateOnly input = DateOnly.FromDateTime((DateTime)ScheduleDate);
                List<Class> todaytasks = MainVM.CurrentUser.GetSchedule(input, MainVM.UserClassList);
                LoadTodayTasks(todaytasks);
            });
        }
        #endregion
        public FullCalendarVM()
        {
            CurrentDate = DateTime.Now.ToString("MMMM d, yyyy");
            DateOnly input = DateOnly.FromDateTime(DateTime.Now);
            List<Class> todaytasks = MainVM.CurrentUser.GetSchedule(input, MainVM.UserClassList);
            LoadTodayTasks(todaytasks);
            InitializeCommands();
        }
    }
}
