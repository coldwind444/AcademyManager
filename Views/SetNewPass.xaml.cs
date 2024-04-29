using System.Windows.Controls;
using AcademyManager.Viewmodels;

namespace AcademyManager.Views
{
    /// <summary>
    /// Interaction logic for SetNewPass.xaml
    /// </summary>
    public partial class SetNewPass : UserControl
    {
        public SetPasswordVM Viewmodel { get; set; }
        public SetNewPass(int t, MainVM vm)
        {
            this.DataContext = Viewmodel = new SetPasswordVM(t, vm);
            InitializeComponent();
        }
    }
}
