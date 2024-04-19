using AcademyManager.Models;
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
    public class AccountTypeSelectionView : BaseViewModel
    {
        #region Commands
        public ICommand InstructorCommand { get; set; }
        public ICommand StudentCommand { get; set; }
        #endregion

        #region Methods
        private void InitializeCommand()
        {
            InstructorCommand = new RelayCommand<MainWindow>(p => { return true; }, async p =>
            {
                MainVM vm = p.DataContext as MainVM;
                vm.CurrentAccount.Type = 1;
                DatabaseManager database = new DatabaseManager();
                await database.UpdateAccountAsync(vm.CurrentAccount);
            });

            StudentCommand = new RelayCommand<MainWindow>(p => { return true; }, async p =>
            {
                MainVM vm = p.DataContext as MainVM;
                vm.CurrentAccount.Type = 2;
                DatabaseManager database = new DatabaseManager();
                await database.UpdateAccountAsync(vm.CurrentAccount);
            });
        }
        #endregion
        public AccountTypeSelectionView()
        {
            InitializeCommand();
        }
    }
}
