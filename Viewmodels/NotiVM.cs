﻿using AcademyManager.Models;
using AcademyManager.UCViews;
using System.Windows.Controls;
using System.Windows.Input;

namespace AcademyManager.Viewmodels
{
    public class NotiVM : BaseViewModel
    {
        #region Commands
        public ICommand BackCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        #endregion
        #region Properties
        private MainVM ParentVM { get; set; }
        private StackPanel NotiPanel { get; set; }
        public SpecificNoti DeletedNoti { get; set; }
        #endregion
        #region Methods
        public async void DeleteNoti()
        {
            DatabaseManager db = new DatabaseManager();
            if (MainVM.CurrentUser.Notifications.ContainsKey(DeletedNoti.ID))
            {
                MainVM.CurrentUser.Notifications.Remove(DeletedNoti.ID);
                await db.RemoveNotificationAsync(MainVM.CurrentAccount.UUID, MainVM.CurrentAccount.Type, DeletedNoti.ID);
            }
            NotiPanel.Children.Remove(DeletedNoti);
        }
        private void LoadNotifications()
        {
            Dictionary<int, Notification> list = MainVM.CurrentUser.Notifications;
            if (list == null || list.Count == 0) return;
            foreach (Notification n in list.Values)
            {
                SpecificNoti noti = new SpecificNoti(n.ID, n.Title, n.Message, n.UpdateDate, this);
                NotiPanel.Children.Add(noti);
            }
        }
        private void InitializeCommands()
        {
            BackCommand = new RelayCommand<object>(p => true, p =>
            {
                ParentVM.HomeNavigateCommand.Execute(null);
                LoadNotifications();
            });

            ReloadCommand = new RelayCommand<object>(p => true, p =>
            {
                LoadNotifications();
            });
        }
        #endregion
        public NotiVM(MainVM vm, StackPanel pn)
        {
            NotiPanel = pn;
            ParentVM = vm;
            LoadNotifications();
            InitializeCommands();
        }
    }
}
