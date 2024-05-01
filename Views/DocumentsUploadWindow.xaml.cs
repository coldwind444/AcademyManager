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
    /// Interaction logic for DocumentsUploadWindow.xaml
    /// </summary>
    public partial class DocumentsUploadWindow : Window
    {
        public DocumentsUploadVM Viewmodel { get; set; }
        public DocumentsUploadWindow(Class data)
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new DocumentsUploadVM(data, notification);
        }
    }
}
