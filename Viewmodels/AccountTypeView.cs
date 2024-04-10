using AcademyManager.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class AccountTypeView : BaseViewModel
    {
        #region Commands
        public ICommand InstructorCommand { get; set; }
        public ICommand StudentCommand { get; set; }
        #endregion

        #region Methods
        private void InitializeCommand()
        {
            InstructorCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                MainVM vm = p.DataContext as MainVM;
                vm.CurrentAccount.Type = 1;
            });

            StudentCommand = new RelayCommand<MainWindow>(p => { return true; }, p =>
            {
                MainVM vm = p.DataContext as MainVM;
                vm.CurrentAccount.Type = 2;
            });
        }
        #endregion
        public AccountTypeView()
        {
            InitializeCommand();
        }
    }
}
