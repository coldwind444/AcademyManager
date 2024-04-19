using AcademyManager.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AcademyManager.Views;

namespace AcademyManager.Viewmodels
{
    public class RootVM : BaseViewModel
    {
        #region Commands
        public ICommand LoginCommand { get; set; }
        public ICommand SigninCommand { get; set; }
        #endregion
        #region Properties
        #endregion
        #region Methods
        private void InitializeCommands()
        {
            LoginCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                MainVM? vm = p.DataContext as MainVM;
                if (vm != null) vm.CurrentView = vm.LoginView;
            });

            SigninCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                MainVM? vm = p.DataContext as MainVM;
                if (vm != null) vm.CurrentView = vm.SigninView;
            });
        }
        #endregion
        public RootVM()
        {
            InitializeCommands();
        }
    }
}
