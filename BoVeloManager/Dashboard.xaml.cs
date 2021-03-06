﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using BoVeloManager.Classes;
using BoVeloManager.tools;

namespace BoVeloManager {
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window {

        public Controler crtl;

        bool isOpen = false;
        string old_select = "Catalog";

        public Dashboard() {
            InitializeComponent();

            crtl = Controler.Instance;

            init();

        }



        private void init() {
            if (crtl.getCurrentUser().getName() != "God") {
                ManagementBtn.Visibility = Visibility.Hidden;
            }


            frame.Content = Catalogue.Catalog.Instance;

            //status bar log
            lb_user.Text = crtl.getCurrentUser().getName();

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(30);
            dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e) {
            if (Controler.Instance.resync(Database.checkTable())) {
                Catalogue.Catalog.Instance.init();
                UI.Commande.Command.Instance.init();
                UI.Planning.Planning.Instance.init();
                Management.Management.Instance.init();
                stock.stock.Instance.init();
                Sales.Sales.Instance.init();
            }
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e) {
            isOpen = true;

            ButtonOpenMenu.Visibility = Visibility.Hidden;
            ButtonCloseMenu.Visibility = Visibility.Visible;
            lb_user.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e) {
            isOpen = false;
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Hidden;
            lb_user.Visibility = Visibility.Hidden;
        }

        /*
         * Function added to every button on the side bar
         *      Their goal is to set the page into the frame (center object of the window)
        */
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (isOpen) {
                var sb = this.Resources["CloseMenu"] as System.Windows.Media.Animation.Storyboard;
                sb.Begin();
            }

            isOpen = false;
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Hidden;
            lb_user.Visibility = Visibility.Hidden;

            string selected = ((ListViewItem)((ListView)sender).SelectedItem).Tag.ToString();

            if (old_select != selected) {
                old_select = selected;
                //clear the content
                frame.Content = null;
                frame.NavigationService.RemoveBackEntry();

                switch (selected) {
                    case "Catalog":
                        frame.Content = Catalogue.Catalog.Instance;
                        break;
                    case "Command":
                        frame.Content = UI.Commande.Command.Instance;
                        break;
                    case "Stock":
                        frame.Content = stock.stock.Instance;
                        break;
                    case "Planning":
                        frame.Content = UI.Planning.Planning.Instance;
                        break;
                    case "Management":
                        frame.Content = Management.Management.Instance;
                        break;
                    case "Sales":
                        frame.Content = Sales.Sales.Instance;
                        break;
                }
            }

        }

        private void lb_user_MouseDown(object sender, MouseButtonEventArgs e)
        {
            User usr = Controler.Instance.getCurrentUser();

            Management.user.modUserWindow MUW = new Management.user.modUserWindow(usr);
            MUW.ShowDialog();
            init();
        }
    }
}
