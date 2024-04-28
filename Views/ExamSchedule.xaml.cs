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
    /// Interaction logic for ExamSchedule.xaml
    /// </summary>
    public partial class ExamSchedule : UserControl
    {
        public ExamScheduleVM Viewmodel { get; set; }
        public ExamSchedule(MainVM vm)
        {
            this.DataContext = Viewmodel = new ExamScheduleVM(vm);
            InitializeComponent();
        }
    }
}
