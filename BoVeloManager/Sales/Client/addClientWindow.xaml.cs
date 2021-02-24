﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace BoVeloManager.Sales.Client {
    /// <summary>
    /// Logique d'interaction pour AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window {
        public AddClientWindow() {
            InitializeComponent();
        }

        private void BT_Add_Click(object sender, RoutedEventArgs e) {

            string firstName = tb_first_name.Text;
            string lastName = tb_last_name.Text;
            string entrepriseName = tb_enterprise_name.Text;
            string entrepriseAdress = tb_enterprise_adress.Text;
            string email = tb_email.Text;
            string phoneNumber = tb_phoneNum.Text;

            int err = 0;

            //test the data
            if ((firstName.Length >= 4) && (firstName != "")) {    //check if the userName is 4 caracters min, and not empty
                if ((lastName.Length >= 4) && (lastName != "")) {
                    if ((entrepriseName.Length >= 4) && (entrepriseName != "")) {
                        if ((entrepriseAdress.Length >= 4) && (entrepriseAdress != "")) {
                            if ((email.Length >= 4) && (email != "")) {
                                if ((phoneNumber.Length >= 4) && (phoneNumber != "")) {
                                    addClient(firstName, lastName, entrepriseName, entrepriseAdress, email, phoneNumber);
                                    this.Close();

                                } else {
                                    err = 1;
                                }
                            } else {
                                err = 2;
                            }
                        } else {
                            err = 3;
                        }
                    } else {
                        err = 4;
                    }
                } else {
                    err = 5;
                }
            } else {
                err = 6;
            }
            lb_error.Visibility = Visibility.Visible;
            switch (err) {
                case 1:
                    lb_error.Text = "First name is invalid";
                    break;
                case 2:
                    lb_error.Text = "Last name is invalid";
                    break;
                case 3:
                    lb_error.Text = "Enterprise name is invalid";
                    break;
                case 4:
                    lb_error.Text = "Enterprise adress is invalid";
                    break;
                case 5:
                    lb_error.Text = "Email is invalid";
                    break;
                case 6:
                    lb_error.Text = "Phone number is invalid";
                    break;
            }
        }

        private void addClient(string first_name, string last_name, string entreprise_name, string entreprise_adress, string email, string phone_num) {

            string q = tools.DatabaseQuery.addClient(first_name, last_name, entreprise_name, entreprise_adress, email, phone_num);

            int res = tools.Database.setData(q);

            if (res == -1) {
                MessageBox.Show("An error has occured");
            } else if (res == 1) {
                MessageBox.Show("User added");
            } else {
                MessageBox.Show("The database might be corrupted");
            }

        }
        private void BT_cancel_Click(object sender, RoutedEventArgs e) {

            this.Close();
        }
    }
}