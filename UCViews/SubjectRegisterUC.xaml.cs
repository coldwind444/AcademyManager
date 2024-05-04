using AcademyManager.Models;
using AcademyManager.Viewmodels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            if (button != null) button.IsEnabled = false;
            if (!IsRegisterd)
            {
                IsRegisterd = true;
                StudentUser? user = MainVM.CurrentUser as StudentUser;
                if (user != null)
                {
                    bool success = await user.RegisterClass(ClassData.TermID, ClassData.CourseID, ClassData.ClassID, MainVM.CurrentAccount.UUID);
                    ParentVM.ShowNotification(success);
                    if (!success) return;
                    MainVM.CurrentUser = user;
                }
                Icon.Kind = PackIconKind.BoxCancelOutline;
                RegisterButton.ToolTip = "Huỷ đăng ký";
            } else
            {
                IsRegisterd = false;
                StudentUser? user = MainVM.CurrentUser as StudentUser;
                if (user != null)
                {
                    bool success = await user.CancelRegisterClass(ClassData.TermID, ClassData.CourseID, ClassData.ClassID, MainVM.CurrentAccount.UUID);
                    ParentVM.ShowNotification(success);
                    if (!success) return;
                    MainVM.CurrentUser = user;
                }
                Icon.Kind = PackIconKind.PencilBoxOutline;
                RegisterButton.ToolTip = "Đăng ký";
            }
            if (button != null) button.IsEnabled = true;
        }
    }
}
