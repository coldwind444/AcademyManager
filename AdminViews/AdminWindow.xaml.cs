using AcademyManager.AdminViewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AcademyManager.AdminViews
{
    public partial class AdminWindow : Window
    {
        public AdminWindowVM Viewmodel {  get; set; }
        public AdminWindow()
        {
            this.DataContext = Viewmodel = new AdminWindowVM();
            InitializeComponent();
        }
    }
}
