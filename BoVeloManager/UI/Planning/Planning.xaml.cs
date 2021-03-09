﻿using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace BoVeloManager.UI.Planning {
    /// <summary>
    /// Interaction logic for Planning.xaml
    /// </summary>
    public partial class Planning : Page {


        private int nbrWeek;
        private bool has_init = false;

        public Planning() {
            InitializeComponent();
            has_init = true;

            nbrWeek = Controler.getNBRWeek(DateTime.Now);

            init();
        }

        private void init() {
            lb_nbrWeek.Text = "Week : " + nbrWeek.ToString();


            List<Bike.displayInfo> bk_dpiList = Controler.Instance.GetBikeDisplayInfo_byWeekAndPost(nbrWeek, cb_poste.SelectedIndex);

            //sort them to only get the one for the week
            List<Bike.displayInfo> bk_dpiList_Mon = new List<Bike.displayInfo>();
            List<Bike.displayInfo> bk_dpiList_Tue = new List<Bike.displayInfo>();
            List<Bike.displayInfo> bk_dpiList_Wed = new List<Bike.displayInfo>();
            List<Bike.displayInfo> bk_dpiList_Thu = new List<Bike.displayInfo>();
            List<Bike.displayInfo> bk_dpiList_Fri = new List<Bike.displayInfo>();

            foreach(Bike.displayInfo bdpi in bk_dpiList) {
                switch (bdpi.CurBike.getPlannedtDate().DayOfWeek) {
                    case DayOfWeek.Monday:
                        bk_dpiList_Mon.Add(bdpi);
                        break;
                    case DayOfWeek.Tuesday:
                        bk_dpiList_Tue.Add(bdpi);
                        break;
                    case DayOfWeek.Wednesday:
                        bk_dpiList_Wed.Add(bdpi);
                        break;
                    case DayOfWeek.Thursday:
                        bk_dpiList_Thu.Add(bdpi);
                        break;
                    case DayOfWeek.Friday:
                        bk_dpiList_Fri.Add(bdpi);
                        break;
                }
            }

            lv_monday.ItemsSource = bk_dpiList_Mon;
            lv_tuesday.ItemsSource = bk_dpiList_Tue;
            lv_wednesday.ItemsSource = bk_dpiList_Wed;
            lv_thursday.ItemsSource = bk_dpiList_Thu;
            lv_friday.ItemsSource = bk_dpiList_Fri;

        }

        private void bt_nextWeek_Click(object sender, RoutedEventArgs e) {
            nbrWeek++;
            init();
        }

        private void bt_lastWeek_Click(object sender, RoutedEventArgs e) {
            nbrWeek--;
            init();
        }

        private void cb_poste_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (has_init) {
                init();
            }
           
        }

        private void bt_show_Click(object sender, RoutedEventArgs e) {

            Bike bk = ((Bike.displayInfo)((System.Windows.Controls.Button)e.Source).DataContext).CurBike;
            description.description desp = new description.description(bk);
            desp.ShowDialog();
            init();
        }
    }
}
