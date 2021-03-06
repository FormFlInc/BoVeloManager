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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BoVeloManager.Classes;


namespace BoVeloManager.UI.Commande {
    /// <summary>
    /// Interaction logic for Command.xaml
    /// </summary>
    /// 

    public partial class Command : Page {

        private static Command instance = new Command();
        Controler crtl;

        public Command() {
            crtl = Controler.Instance;

            InitializeComponent();
            init();
        }

        public void init() {
            update_dg_supplierList();
            update_dg_CommandeList();
        }

        public static Command Instance {
            get {
                return instance;
            }
        }

        private void update_dg_supplierList() {
            
            dg_supplierList.ItemsSource = null;
            dg_supplierList.ItemsSource = crtl.GetSupplierDisplayInfo();
        }

        private void update_dg_CommandeList() {
            dg_CommandesList.ItemsSource = null;
            dg_CommandesList.ItemsSource = crtl.GetCommandeDisplayInfo();
        }

        private void bt_addSupplier_Click(object sender, RoutedEventArgs e) {

            BoVeloManager.Sales.Client.AddHumanWindow ACW = new BoVeloManager.Sales.Client.AddHumanWindow(1);
            ACW.ShowDialog();

            update_dg_supplierList();
           
        }

        private void bt_editSupplier_Click(object sender, RoutedEventArgs e) {

            Classes.Supplier c = (Supplier)((Classes.Supplier.displayInfo)((System.Windows.Controls.Button)e.Source).DataContext).CurInstance;
            BoVeloManager.UI.Sales.Client.EditHumanWindow ECW = new UI.Sales.Client.EditHumanWindow(c);
            ECW.ShowDialog();
            update_dg_supplierList();


        }

        private void btnView_Click(object sender, RoutedEventArgs e) {
            Classes.Commande cmd = ((Classes.Commande.displayInfo)((System.Windows.Controls.Button)e.Source).DataContext).CurCmd;

            description.Description dsc = new description.Description(cmd);
            dsc.ShowDialog();
            update_dg_CommandeList();
        }
        private void bt_addCommand_Click(object sender, RoutedEventArgs e) {
            description.AddCommand CA = new description.AddCommand();
            CA.ShowDialog();
            update_dg_CommandeList();

        }
    }

    
}
