﻿using AcademyManager.Models;
using AcademyManager.Viewmodels;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace AcademyManager.UCViews
{
    /// <summary>
    /// Interaction logic for SubjectResgisterUC.xaml
    /// </summary>
    public partial class SubjectRegisterUC : UserControl
    {
        public SubjectRegisterUC()
        {
            InitializeComponent();
        }
        public SubjectRegisterUC(Class cls, PackIconKind icon, SubjectRegisterVM parent)
        {
            Room = cls.Room;
            Day = cls.Weekday;
            string bg = cls.BeginTime.ToString("HH:mm"),
                    e = cls.EndTime.ToString("HH:mm");
            Time = $"{bg} - {e}";
            LecturerName = cls.InstructorName;
            Class = $"{cls.CourseName} - {cls.ClassID}";
            ClassData = cls;
            ParentVM = parent;
            InitializeComponent();
            if (icon == PackIconKind.PencilBox)
            {
                IsRegisterd = false;
                RegisterButton.ToolTip = "Đăng ký";
            }
            else
            {
                IsRegisterd = true;
                RegisterButton.ToolTip = "Hủy đăng ký";
            }
            Icon.Kind = icon;
        }
        private bool IsRegisterd { get; set; }
        private Class ClassData { get; set; }
        private SubjectRegisterVM ParentVM { get; set; }
        public string Room
        {
            get { return (string)GetValue(RoomProperty); }
            set { SetValue(RoomProperty, value); }
        }

        public static readonly DependencyProperty RoomProperty = DependencyProperty.Register("Room", typeof(string), typeof(SubjectRegisterUC));
        public DayOfWeek Day
        {
            get { return (DayOfWeek)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register("Day", typeof(DayOfWeek), typeof(SubjectRegisterUC));
        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(string), typeof(SubjectRegisterUC));
        public string LecturerName
        {
            get { return (string)GetValue(LecturerNameProperty); }
            set { SetValue(LecturerNameProperty, value); }
        }

        public static readonly DependencyProperty LecturerNameProperty = DependencyProperty.Register("LectureName", typeof(string), typeof(SubjectRegisterUC));
        public string Class
        {
            get { return (string)GetValue(ClassProperty); }
            set { SetValue(ClassProperty, value); }
        }

        public static readonly DependencyProperty ClassProperty = DependencyProperty.Register("Class", typeof(string), typeof(SubjectRegisterUC));
        private bool TimeConflict(Class a, Class b)
        {
            bool invaliddate = (a.BeginDate >= b.BeginDate && a.BeginDate <= b.EndDate)
                            || (a.EndDate >= b.BeginDate && a.EndDate <= b.EndDate);
            bool invalidday = a.Weekday == b.Weekday;
            bool invalidtime = (a.BeginTime >= b.BeginTime && a.BeginTime <= b.EndTime)
                            || (a.EndTime >= b.BeginTime && a.EndTime <= b.EndTime);
            return invaliddate || invalidday || invalidtime;
        }
        private bool ExistTimeConflict()
        {
            foreach (Class c in MainVM.UserClassList)
            {
                if (TimeConflict(c, ClassData)) return true;
            }
            return false;
        }
        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            if (button != null) button.IsEnabled = false;
            bool conflict = ExistTimeConflict();
            if (conflict)
            {
                ParentVM.ShowNotification(!conflict, 2);
                button.IsEnabled = true;
                return;
            }
            if (!IsRegisterd)
            {
                IsRegisterd = true;
                StudentUser? user = MainVM.CurrentUser as StudentUser;
                if (user != null)
                {
                    bool success = await user.RegisterClass(ClassData.TermID, ClassData.CourseID, ClassData.ClassID, MainVM.CurrentAccount.UUID);
                    ParentVM.ShowNotification(success, 1);
                    if (!success) return;
                    MainVM.CurrentUser = user;
                    MainVM.UserClassList.Add(ClassData);
                }
                Icon.Kind = PackIconKind.BoxCancelOutline;
                RegisterButton.ToolTip = "Huỷ đăng ký";
            }
            else
            {
                IsRegisterd = false;
                StudentUser? user = MainVM.CurrentUser as StudentUser;
                if (user != null)
                {
                    bool success = await user.CancelRegisterClass(ClassData.TermID, ClassData.CourseID, ClassData.ClassID, MainVM.CurrentAccount.UUID);
                    ParentVM.ShowNotification(success, 1);
                    if (!success) return;
                    MainVM.CurrentUser = user;
                    MainVM.UserClassList.Remove(ClassData);
                }
                Icon.Kind = PackIconKind.PencilBoxOutline;
                RegisterButton.ToolTip = "Đăng ký";
            }
            if (button != null) button.IsEnabled = true;
        }
    }
}
