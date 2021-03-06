﻿using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using StormLoader.modder_control_panel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StormLoader.repository
{
    /// <summary>
    /// Interaction logic for RepoLoginPanel.xaml
    /// </summary>
    public partial class RepoLoginPanel : UserControl
    {
        SQLManager sqcm;
        public RepoLoginPanel()
        {
            InitializeComponent();
            this.sqcm = GlobalVar.sqcm;
            
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            // check login details
            if (sqcm.checkUser(UsernameTxbx.Text, PasswordTxbx.Password.ToString()))
            {
                DbgLog.WriteLine("authenitcation successful");
                ModderPanelRoot mpr = new ModderPanelRoot(UsernameTxbx.Text);
                DialogHost.CloseDialogCommand.Execute(null, null);
                mpr.Show();
                
            } else
            {
                DbgLog.WriteLine("authentication failure");
                InfoPopup ifp = new InfoPopup();
                ifp.titleText.Content = "Authentication failure";
                Label errorText = new Label();
                errorText.Content = "Username and password combination don't exist";
                ifp.infoContainer.Children.Add(errorText);
                infoDialog.ShowDialog(ifp);
            }
            
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTxbx.Text != "" && PasswordTxbx.Password.ToString() != "")
            {
                // uniqueness checking is server side, so we can ignore it here, addUser will return false if the user already exists!
                if (sqcm.addUser(UsernameTxbx.Text, PasswordTxbx.Password.ToString()))
                {
                    DbgLog.WriteLine("added new user successfully");
                    InfoPopup ifp = new InfoPopup();
                    ifp.titleText.Content = "Added user";
                    Label errorText = new Label();
                    errorText.Content = "You may now login with the details you provided";
                    ifp.infoContainer.Children.Add(errorText);
                    infoDialog.ShowDialog(ifp);
                } else
                {
                    DbgLog.WriteLine("could not add new user");
                    InfoPopup ifp = new InfoPopup();
                    ifp.titleText.Content = "User not added";
                    Label errorText = new Label();
                    errorText.Content = "The user could not be created";
                    ifp.infoContainer.Children.Add(errorText);
                    infoDialog.ShowDialog(ifp);
                }
            }
            
        }
    }
}
