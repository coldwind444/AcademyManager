﻿using AcademyManager.AdminViewmodels;
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

namespace AcademyManager.AdminViews
{
    /// <summary>
    /// Interaction logic for AddCoursesUC.xaml
    /// </summary>
    public partial class AddTermsUC : UserControl
    {
        public AddTermsVM Viewmodel { get; set ; }
        public AddTermsUC()
        {
            this.DataContext = Viewmodel = new AddTermsVM();
            InitializeComponent();
        }
    }
}