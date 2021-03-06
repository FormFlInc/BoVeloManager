﻿using BoVeloManager.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.tools {

    class DatabaseQuery {

        public static string getCheckSum_tableQuery(string table) {
            return "CHECKSUM TABLE `" + table + "`";
        }

        public static string getTable() {
            return "SHOW TABLES;";
        }

        public static string getUsers(int filter) {
            string f = "";
            switch (filter) {
                case 0:
                    break;
                case 1:
                    f = "WHERE `grade` = 2";
                    break;
                case 2:
                    f = "WHERE `grade` = 1";
                    break;
                case 3:
                    f = "WHERE `grade` = 0";
                    break;

            }


            return "SELECT * FROM `bv_user`" + f;
        }

        public static string addUser(string name, string pass, int grade) {
            return "INSERT INTO `bv_user`(`user`, `psw`, `grade`) VALUES ('" + name + "','" + pass + "'," + grade.ToString() + ")";
        }

        public static string setUserPass(int id, string pass) {
            return "UPDATE `bv_user` SET `psw`='" + pass + "'  WHERE `id` =" + id.ToString();
        }

        public static string setUserGrade(int id, int grade) {
            return "UPDATE `bv_user` SET `grade`= " + grade.ToString() + " WHERE `id` = " + id.ToString();
        }



        public static string updateHumans(int id, string entreprise_name, string enterprise_adress, string email, string phone_num) {
            return "UPDATE `bv_human` SET `enterprise_name`= '" + entreprise_name + "', `enterprise_adress`= '" + enterprise_adress + "',  `email`= '" + email + "', `phone_num`= '" + phone_num + "' WHERE `id` = " + id.ToString();
        }

        public static string getHumans(int fct) {
            return "SELECT *  FROM `bv_human` WHERE `fct` = " + fct.ToString();
        }

        public static string addHuman(Human c, int fct) {
            return "INSERT INTO `bv_human`(`id`,`first_name`, `last_name`, `enterprise_name`, `enterprise_adress`, `email`, `phone_num`,`date`,`fct`) VALUES (" + c.getId() + ",'" + c.getFirstName() + "','" + c.getLastName() + "','" + c.getEtpName() + "','" + c.getEtpAdress() + "','" + c.getEmail() + "','" + c.getPhoneNumb() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "'," + fct.ToString() + ")";
        }



        public static string getCatalogBike() {
            return "SELECT * FROM `bv_catalog`";
        }

        public static string addCatalogBike(CatalogBike cb) {
            return "INSERT INTO `bv_catalog` (`id`,`name`,`PriceMul`,`picture`) VALUES (" + cb.getId().ToString() + ",'" + cb.getName() + "'," + cb.getPriceMul().ToString() + ", 'Bike0.jpg')";
        }

        public static string updateCatalogBike(CatalogBike kt) {
            return "UPDATE `bv_catalog` SET `name` = '" + kt.getName() + "' , `PriceMul` = '" + kt.getPriceMul().ToString() + "' WHERE `id` = " + kt.getId().ToString();
        }



        public static string getKits() {
            return "SELECT * FROM `bv_kit`";
        }

        public static string getKit_by_catalogBikeId(int id_cat) {
            return "SELECT `id_tKit` FROM `bv_catalogkit` WHERE `id_cat` = " + id_cat.ToString();
        }

        public static string getKitId_byTBike(int id_tBike) {
            return "SELECT K.id FROM `bv_templatebikekit` AS B INNER JOIN `bv_kit` AS K ON B.id_tKit = K.id WHERE B.id_tBike =" + id_tBike.ToString();
        }

        public static string addKit(int id, string name, string prop, int price, int cat, int stockQtt, int locX, int locY, int bikeQtt) {
            return "INSERT INTO `bv_kit`(`id`,`name`, `properties`,`price`, `category`,`stock_qtt`,`location_x`,`location_y`,`bike_qtt`) VALUES ('" + id.ToString() + "','" + name + "','" + prop + "','" + price.ToString() + "','" + cat.ToString() + "','" + stockQtt.ToString() + "','" + locX.ToString() + "','" + locY.ToString() + "','" + bikeQtt.ToString() + "')";
        }

        public static string addCompatibleKit(int id_cat, int id_tKit) {
            return "INSERT INTO `bv_catalogkit` (`id_cat`, `id_tKit`) VALUES('" + id_cat + "', '" + id_tKit + "')";
        }

        public static string delCompatibleKit(int id_cat, int id_tKit) {
            return "DELETE FROM `bv_catalogkit` WHERE `bv_catalogkit`.`id_cat` = " + id_cat.ToString() + " AND `bv_catalogkit`.`id_tKit` = " + id_tKit.ToString();
        }

        public static string getSales() {
            return "SELECT * FROM `bv_transaction` WHERE `fct` = 0";
        }

        public static string updateKitTemplate(int id, string name, int cat, int price, string prop, int stock_qtt, int locX, int locY, int bike_qtt) {
            return "UPDATE `bv_kit` SET `name`= '" + name + "',`category`='" + cat.ToString() + "',`Price`='" + price.ToString() + "',`properties`='" + prop + "',`stock_qtt`='" + stock_qtt.ToString() + "',`bike_qtt`='" + bike_qtt.ToString() + "',`location_x`='" + locX.ToString() + "',`location_y`='" + locY.ToString() + "' WHERE `id`=" + id;

        }

        public static string addSale(Sale s) {
            return "INSERT INTO `bv_transaction`(`id`, `id_human`, `id_seller`,`state`, `prevision_date`, `date`) VALUES ('" + s.getId() + "', '" + s.getClient().getId() + "','" + s.getSeller().getId() + "','Open','" + s.getPreSaleDate().ToString("yyyy-MM-dd") + "','" + s.getSaleDate().ToString("yyyy-MM-dd") + "')";
        }


        public static string addBikeTemplate(BikeTemplate t) {
            return "INSERT INTO `bv_templatebike` (`id`, `id_cat`) VALUES (" + t.getId() + ", " + t.getCat().getId() + ")";
        }

        public static string addBike(Bike b) {
            //return "INSERT INTO `bv_bike` (`id`, `id_tBike`, `id_sale`, `state`, `planne_cDate`, `poste`) VALUES (" + b.getId() + ", " + b.getBikeTempl().getId() + ", " + b.getSaleId() + ", " + b.getState() + ", '" + b.getPlannedtDate().ToString("yyyy-MM-dd") + "', '" + b.getPoste() + ")";
            if (b.getSaleId() >= 0) {
                return "INSERT INTO `bv_bike` (`id`, `id_tBike`, `id_sale`, `state`, `planne_cDate`, `create_Date`, `poste`) VALUES (" + b.getId() + ", " + b.getBikeTempl().getId() + ", " + b.getSaleId() + ", " + b.getState() + ", '" + b.getPlannedtDate().ToString("yyyy-MM-dd") + "', '" + b.getPlannedtDate().ToString("yyyy-MM-dd") + "', " + b.getPoste() + ")";
            } else {
                return "INSERT INTO `bv_bike` (`id`, `id_tBike`, `state`, `planne_cDate`, `create_Date`, `poste`) VALUES (" + b.getId() + ", " + b.getBikeTempl().getId() + ", " + b.getState() + ", '" + b.getPlannedtDate().ToString("yyyy-MM-dd") + "', '" + b.getPlannedtDate().ToString("yyyy-MM-dd") + "', " + b.getPoste() + ")";
            }
        }

        public static string link_kit_to_tbike(BikeTemplate bt, KitTemplate kt) {
            return "INSERT INTO `bv_templatebikekit` (`id_tBike`, `id_tKit`) VALUES (" + bt.getId() + ", " + kt.getId() + ")";
        }

        public static string getBike() {
            return "SELECT * FROM `bv_bike`";
        }

        public static string updateBike(Bike bk) {
            if(bk.getSaleId() != -1) {
                return "UPDATE `bv_bike` SET `state`= " + bk.getState().ToString() + ",`id_sale` = "+ bk.getSaleId() +" ,`planne_cDate`='" + bk.getPlannedtDate().ToString("yyyy-MM-dd") + "' ,`poste`= " + bk.getPoste().ToString() + ",`create_Date`='" + bk.getConstructionDate().ToString("yyyy-MM-dd") + "' WHERE `id` = " + bk.getId();
            } else {
                return "UPDATE `bv_bike` SET `state`= " + bk.getState().ToString() + " ,`planne_cDate`='" + bk.getPlannedtDate().ToString("yyyy-MM-dd") + "' ,`poste`= " + bk.getPoste().ToString() + ",`create_Date`='" + bk.getConstructionDate().ToString("yyyy-MM-dd") + "' WHERE `id` = " + bk.getId();
            }
            
        }

        public static string getTBike() {
            return "SELECT * FROM `bv_templatebike`";
        }

        public static string addCommande(Commande cd) {

            return "INSERT INTO `bv_transaction`(`id`, `id_human`, `id_seller`, `state`, `prevision_date`, `date`, `fct`) VALUES ('" + cd.getId() + "', '" + cd.getClient().getId() + "','" + cd.getSeller().getId() + "','" + cd.getState() + "','" + cd.getPreSaleDate().ToString("yyyy-MM-dd") + "','" + cd.getSaleDate().ToString("yyyy-MM-dd") + "',1)";

        }
        public static string addCommandeItems(Commande_item ci, Commande cd) {

            return "INSERT INTO `bv_transaction_kit`(`id_sale`, `id_type_kit`, `qnt`) VALUES ('" + cd.getId() + "', '" + ci.kt.getId() + "','" + ci.qnt + "')";

        }

        public static string getCommande() {
            return "SELECT * FROM `bv_transaction` WHERE `fct` = 1";
        }

        public static string updateTransaction(Transaction c) {
            return "UPDATE `bv_transaction` SET `state` = '" + c.getState() + "' , `prevision_date` = '" + c.getPreSaleDate().ToString("yyyy-MM-dd") + "' WHERE `id` = " + c.getId().ToString();
        }


        public static string getCommandeItems(int id) {
            return "SELECT * FROM `bv_transaction_kit` WHERE `id_sale` = " + id.ToString();
        }

    }

}
