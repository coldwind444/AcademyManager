using AcademyManager.AdminViewmodels;
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

namespace AcademyManager.AdminViews
{
    /// <summary>
    /// Interaction logic for AdminAuthWindow.xaml
    /// </summary>
    public partial class AdminAuthWindow : Window
    {
        public AdminAuthWindowVM Viewmodel {  get; set; }
        public AdminAuthWindow()
        {
            this.DataContext = Viewmodel = new AdminAuthWindowVM();
            InitializeComponent();
        }
    }
}
