﻿using AcademyManager.AdminViews;
using AcademyManager.Models;
using AcademyManager.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AcademyManager.AdminViewmodels
{
    public class AdminLoginVM : BaseViewModel
    {
        #region Commands
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        #endregion

        #region Properties
        private string _password;
        private string _uuid;
        private string _notification;
        private PasswordBox _passwordBox;
        private Visibility _notificationV;
        public Visibility NotificationV
        {
            get { return _notificationV; }
            set { _notificationV = value; OnPropertyChanged(); }
        }
        public string Notification
        {
            get { return _notification; }
            set { _notification = value; OnPropertyChanged(); }
        }
        public string UUID
        {
            get { return _uuid; }
            set { _uuid = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        private bool NullOrEmpty(string s)
        {
            return s == null || s.Length == 0;
        }
        private FrameworkElement? GetWindowParent(UserControl p)
        {
            FrameworkElement? parent = p;

            while (parent?.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }
        private void InitializeCommands()
        {
            LoginCommand = new RelayCommand<AdminLoginUC>(p => { return !NullOrEmpty(_password) && !NullOrEmpty(_uuid); }, async p =>
            {
                DatabaseManager db = new DatabaseManager();
                Admin ad = await db.GetAdminAsync(UUID);
                if (ad != null)
                {
                    if (ad.Match(_uuid, _password))
                    {
                        Window w = GetWindowParent(p) as Window;
                        AdminWindow adwd = new AdminWindow();
                        if (w == null) return;
                        NotificationV = Visibility.Hidden;
                        w.Hide();
                        adwd.ShowDialog();
                        w.ShowDialog();
                    } else
                    {
                        Notification = "Sai mật khẩu.";
                        NotificationV = Visibility.Visible;
                        await Task.Delay(1000);
                        NotificationV = Visibility.Hidden;
                    }
                } else
                {
                    Notification = "Tài khoản không tồn tại.";
                    NotificationV = Visibility.Visible;
                    await Task.Delay(1000);
                    NotificationV = Visibility.Hidden;
                }
                UUID = String.Empty;
                _passwordBox.Clear();
            });

            PasswordChangedCommand = new RelayCommand<PasswordBox>(p => { return true; }, p =>
            {
                _password = p.Password;
                _passwordBox = p;
            });
        }
        #endregion
        public AdminLoginVM()
        {
            InitializeCommands();
            NotificationV = Visibility.Hidden;
        }
    }
}
