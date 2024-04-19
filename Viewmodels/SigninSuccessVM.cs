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
    public class SigninSuccessVM : BaseViewModel
    {
        #region Commands
        public ICommand BackCommand { get; set; }
        #endregion

        #region Properties
        #endregion

        #region Methods
        private void InitializeCommands()
        {
            BackCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                MainVM vm = p.DataContext as MainVM;
                if (vm != null)
                {
                    vm.CurrentView = vm.LoginView;
                }
            });
        }
        #endregion
        public SigninSuccessVM()
        {
            InitializeCommands();
        }
    }
}
