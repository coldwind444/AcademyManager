using AcademyManager.Models;
using AcademyManager.UCViews;
using AcademyManager.Viewmodels;
using System;
using System.Collections.Generic;
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
        public DateTime? ScheduleDate
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }
        public string CurrentDate
        {
            get { return _currdate; }
            set { _currdate = value; OnPropertyChanged(); }
        }
        #endregion
        #region Methods
        private void LoadTodayTasks(List<Class> list, StackPanel panel)
        {
            TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);
            foreach (Class c in list)
            {
                Item item;
                string bg = c.BeginTime.ToString("HH:mm");
                string e = c.EndTime.ToString("HH:mm");
                string time = $"{bg} - {e}";
                if (c.EndTime >= now) item = new Item(c.CourseName, time, c.Room, FontAwesome.WPF.FontAwesomeIcon.CircleOutline);
                else item = new Item(c.CourseName, time, c.Room, FontAwesome.WPF.FontAwesomeIcon.CheckCircle);
                panel.Children.Add(item);
            }
        }
        private void InitializeCommands()
        {
            SelectedDateChangedCommand = new RelayCommand<StackPanel>(p => true, p =>
            {
                CurrentDate = ScheduleDate.Value.ToString("dd/MM/yyyy");
                DateOnly input = DateOnly.FromDateTime((DateTime)ScheduleDate);
                List<Class> todaytasks = MainVM.CurrentUser.GetSchedule(input, MainVM.UserClassList);
                LoadTodayTasks(todaytasks, p);
            });
        }
        #endregion
        public FullCalendarVM(StackPanel panel)
        {
            ScheduleDate = DateTime.Now;
            CurrentDate = ScheduleDate.Value.ToString("dd/MM/yyyy");
            DateOnly input = DateOnly.FromDateTime((DateTime)ScheduleDate);
            List<Class> todaytasks = MainVM.CurrentUser.GetSchedule(input, MainVM.UserClassList);
            LoadTodayTasks(todaytasks, p);
            InitializeCommands();
        }
    }
}
