﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVeloManager.tools.UI;

namespace BoVeloManager.Classes
{
    public class BikeTemplate
    {
        private readonly CatalogBike catalogBike;
        private int id;
        private List<KitTemplate> KitTemplList;
        private string color;
        private string size;

        public BikeTemplate(int Id, CatalogBike catalogBike_){
            catalogBike = catalogBike_;
            id = Id;
            KitTemplList = new List<KitTemplate>();
        }

        public string Key {
            get {
                KitTemplate kt_fram = KitTemplList.Where(x => x.getCategory() == KitCategory.frame).ToList()[0];
                return getName() + kt_fram.getProperties();
            }
        }

        public string getName(){
            return catalogBike.getName();
        }

        public CatalogBike getCat() {
            return catalogBike;
        }
        
        public int getPrice(){
            int price = 0;
            foreach (KitTemplate kit in KitTemplList) {
                price += kit.getPrice();
            }
            
            price = price + (price * getCat().getPriceMulDiv())/100;
            return price;
        }

        public string Color {
            get { return color; }
            set { color = value; }
        }

        public string Size
        {
            get { return size; }
            set { size = value; }
        }

        public List<KitTemplate> getListKit() {
            return KitTemplList;
        }

        public int getId(){
            return id;
        }

        public void setId(int id_) {
            id = id_;
        }

        public void linkKitTemplate(KitTemplate kt){
            KitTemplList.Add(kt);
        }
        
        public void unlinkKitTemplate(KitTemplate kt){
            KitTemplList.Remove(kt);
        }

        public string getPropkitString() {
            string propKit = "";
            foreach (KitTemplate kit in getListKit()) {
                propKit += kit.getPropkitString() + "\n";
            }
            return propKit;
        }

        
        public displayInfo getDisplayInfo()
        {
            displayInfo dI = new displayInfo();
            dI.id = getId();
            dI.price = (this.getPrice()/100).ToString("c2");
            dI.priceMul = (this.getCat().getPriceMulDiv()).ToString("c2");
            dI.name = getName();
            dI.BikeTemp = this;
            dI.propKit = getPropkitString();
            dI.KitTemplList = KitTemplList;
            return dI;
        }

        public class displayInfo {
            public BikeTemplate BikeTemp;
            public List<KitTemplate> KitTemplList;
            public string propKit { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string price { get; set; }
            public string priceMul { get; set; }
            public string fullname { get {
                    KitTemplate kt_fram = KitTemplList.Where(x => x.getCategory() == KitCategory.frame).ToList()[0];
                    return name + " " + kt_fram.getFullName();
                } }
        }
        
    }
}
