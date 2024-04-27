using AcademyManager.Models;
using AcademyManager.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AcademyManager.Views;
using AcademyManager.AdminViews;

namespace AcademyManager.Viewmodels
{
    public class WhoAreYouVM : BaseViewModel
    {
        #region Commands
        public ICommand InstructorCommand { get; set; }
        public ICommand StudentCommand { get; set; }
        public ICommand AdminCommand { get; set; }
        #endregion
        #region Properties
        private MainVM ParentVM { get; set; }
        #endregion
        #region Methods
        private void InitializeCommand()
        {
            InstructorCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                if (ParentVM.WelcomeView == null)
                {
                    ParentVM.WelcomeView = new WelcomeScreen(1, ParentVM);
                    ParentVM.CurrentView = ParentVM.WelcomeView;
                }
                else
                    ParentVM.CurrentView = ParentVM.WelcomeView;
            });

            StudentCommand = new RelayCommand<object>(p => { return true; },  p =>
            {
                if (ParentVM.WelcomeView == null)
                {
                    ParentVM.WelcomeView = new WelcomeScreen(2, ParentVM);
                    ParentVM.CurrentView = ParentVM.WelcomeView;
                }
                else
                    ParentVM.CurrentView = ParentVM.WelcomeView;
            });

            AdminCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                AdminAuthWindow adm = new AdminAuthWindow();
                adm.ShowDialog();
            });
        }
        #endregion
        public WhoAreYouVM(MainVM vm)
        {
            ParentVM = vm;
            InitializeCommand();
        }
    }
}
