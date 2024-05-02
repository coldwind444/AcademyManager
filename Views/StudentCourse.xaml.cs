using AcademyManager.Models;
using AcademyManager.Viewmodels;
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

namespace AcademyManager.Views
{
    /// <summary>
    /// Interaction logic for StudentCourse.xaml
    /// </summary>
    public partial class StudentCourse : UserControl
    {
        public CourseInfoVM Viewmodel { get; set; }
        public StudentCourse(Class data, MainVM vm, SubjectListVM p)
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new CourseInfoVM(data, vm, DocumentsPanel);
        }
    }
}
