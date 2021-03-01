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

namespace BoVeloManager.Management.item
{
    /// <summary>
    /// Logique d'interaction pour modItemWindow.xaml
    /// </summary>
    public partial class modItemWindow : Window
    {
        private int itemId;
        public modItemWindow(int itemId_)
        {
            InitializeComponent();

            //initialize the windows
            itemId = itemId_;
            //get the item data
            string q = tools.DatabaseQuery.getItem_by_id(itemId);
            DataTable res = tools.Database.getData(q);
            //diplay the item data
            tb_newItemName.Text = (string)res.Rows[0]["name"];
            sl_pricemul.Value = Convert.ToInt32(res.Rows[0]["PriceMul"]);
            lb_pricemul.Content = "Price " + (sl_pricemul.Value / 100).ToString("P");
        }

        private void BT_update_Click(object sender, RoutedEventArgs e)
        {
            
            string newItemName = tb_newItemName.Text;
            if ((newItemName.Length >= 2) && (newItemName != ""))
            {
                //string q = tools.DatabaseQuery.setItemName(itemId, newItemName);
                int price = (int)sl_pricemul.Value;
                //tools.Database.setData(q);
                updateItemName(itemId, newItemName,price);
            }
            else
            {
                MessageBox.Show("The new name is invalid");
            }
        }

        private void updateItemName(int id, string name,int priceMul)
        {
            string q = tools.DatabaseQuery.updateItem(id, name,priceMul);
            int res = tools.Database.setData(q);

            if (res == -1)
            {
                MessageBox.Show("An error has occured");
            }
            else if (res == 1)
            {
                MessageBox.Show("Done");
            }
            else
            {
                MessageBox.Show("The database is corrupted");
            }
            this.Close();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            lb_pricemul.Content = "Price " + (sl_pricemul.Value / 100).ToString("P");
        }

        private void BTCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}