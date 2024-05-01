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
using System.Windows.Shapes;

namespace AcademyManager.Views
{
    /// <summary>
    /// Interaction logic for ScoreUpdateWindow.xaml
    /// </summary>
    public partial class ScoreUpdateWindow : Window
    {
        public ScoreUpdateVM Viewmodel {  get; set; }
        public ScoreUpdateWindow(Class data)
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new ScoreUpdateVM(data, noti);
        }
    }
}
