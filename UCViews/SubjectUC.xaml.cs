using System.Windows;
using System.Windows.Controls;
using AcademyManager.Models;
using AcademyManager.UCViewmodels;
using AcademyManager.Viewmodels;

namespace AcademyManager.UCViews
{
    /// <summary>
    /// Interaction logic for SubjectUC.xaml
    /// </summary>
    public partial class SubjectUC : UserControl
    {
        public SubjectVM Viewmodel { get; set; }
        public SubjectUC(MainVM p, Class info, string cid, string sub, string lecturer, string cln, string room, string time)
        {
            LecturerName = lecturer;
            Subject = sub;
            CourseID = cid;
            Class = cln;
            Room = room;
            Time = time;
            ClassInfo = info;
            this.DataContext = Viewmodel = new SubjectVM(p, info);
            InitializeComponent();
        }
        public Class ClassInfo { get; set; }
        public string CourseID
        {
            get { return (string)GetValue(CourseIDProperty); }
            set { SetValue(CourseIDProperty, value); }
        }

        public static readonly DependencyProperty CourseIDProperty = DependencyProperty.Register("CourseID", typeof(string), typeof(SubjectUC));
        public string Subject
        {
            get { return (string)GetValue(SubjectProperty); }
            set { SetValue(SubjectProperty, value); }
        }

        public static readonly DependencyProperty SubjectProperty = DependencyProperty.Register("Subject", typeof(string), typeof(SubjectUC));
        public string LecturerName
        {
            get { return (string)GetValue(LecturerNameProperty); }
            set { SetValue(LecturerNameProperty, value); }
        }

        public static readonly DependencyProperty LecturerNameProperty = DependencyProperty.Register("LectureName", typeof(string), typeof(SubjectUC));
        public string Class
        {
            get { return (string)GetValue(ClassProperty); }
            set { SetValue(ClassProperty, value); }
        }

        public static readonly DependencyProperty ClassProperty = DependencyProperty.Register("Class", typeof(string), typeof(SubjectUC));
        public string Room
        {
            get { return (string)GetValue(RoomProperty); }
            set { SetValue(RoomProperty, value); }
        }

        public static readonly DependencyProperty RoomProperty = DependencyProperty.Register("Room", typeof(string), typeof(SubjectUC));
        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(string), typeof(SubjectUC));
    }
}
