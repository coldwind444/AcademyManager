using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
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
    /// Interaction logic for SubjectContentUC.xaml
    /// </summary>
    public partial class SubjectContentUC : UserControl
    {
        public SubjectContentUC(string name, string url)
        {
            Title = name;
            URL = url;
            InitializeComponent();
        }
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(SubjectContentUC));
        public string URL
        {
            get { return (string)GetValue(URLProperty); }
            set { SetValue(URLProperty, value); }
        }

        public static readonly DependencyProperty URLProperty = DependencyProperty.Register("Title", typeof(string), typeof(SubjectContentUC));

        private void content_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo(URL) { UseShellExecute = true });
        }
    }
}
