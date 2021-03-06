﻿using System;
using System.Collections.Generic;
using System.Data;
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
using BoVeloManager.Classes;

namespace BoVeloManager.Management.kit
{
    /// <summary>
    /// Logique d'interaction pour modKitWindow.xaml
    /// </summary>
    public partial class modKitWindow : Window
    {
        private KitTemplate kt;
        public modKitWindow(KitTemplate kt_)
        {
            InitializeComponent();
            kt = kt_;

            foreach (KitCategory i in Enum.GetValues(typeof(KitCategory))) {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = tools.Converter.GetCatName(i);
                cbi.Tag = i;
                kit_cat.Items.Add(cbi);
            }

            //display the user data
            tb_editName.Text = kt.getName();
            tb_editProperties.Text = kt.getProperties();
            tb_editPrice.Text = kt.getPrice().ToString();
            kit_cat.SelectedIndex = (int)kt.getCategory();
            tb_editBikeQtt.Text = kt.getBikeQtt().ToString();
        }

        private void BT_update_Click(object sender, RoutedEventArgs e)
        {
            kt.setName(tb_editName.Text);
            kt.setProperties(tb_editProperties.Text);
            kt.setPrice(Convert.ToInt32(tb_editPrice.Text));
            kt.setCategory((KitCategory)kit_cat.SelectedIndex);
            kt.setBikeQtt(Convert.ToInt32(tb_editBikeQtt.Text));

            this.Close();
        }


        private void BTCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
