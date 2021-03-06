﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    public class Human 
    {

        private int id;
        private string etp_name;
        private string etp_adress;
        private string email;
        private string phone_num;
        private string name;
        private string first_name;
        private string last_name;
        private DateTime insc_date;

        public Human(int id_, string first_name_, string last_name_, string etp_name_, string etp_adress_, string email_, string phone_num_, DateTime insc_date_) {

            etp_name = etp_name_;
            etp_adress = etp_adress_;
            email = email_;
            phone_num = phone_num_;
            insc_date = insc_date_;
            name = first_name_ + " " + last_name_;
            id = id_;
            first_name = first_name_;
            last_name = last_name_;
        }

        public string getEtpName() {
            return etp_name;
        }
        public string getEtpAdress() {
            return etp_adress;
        }
        public string getEmail() {
            return email;
        }
        public string getPhoneNumb() {
            return phone_num;
        }
        public string getName() {
            return name;
        }
        public string getFirstName() {
            return first_name;
        }
        public string getLastName() {
            return last_name;
        }
        public int getId() {
            return id;
        }
        public DateTime getInscDate() {
            return insc_date;
        }
        public void setEditHuman(string entreprise_name, string entreprise_adress, string email_, string phone_num_) {

            etp_name = entreprise_name;
            etp_adress = entreprise_adress;
            email = email_;
            phone_num = phone_num_;

        }
        public displayInfo GetDisplayInfo() {
            displayInfo temp = new displayInfo();

            temp.CurInstance = this;
            temp.name = this.getName();
            temp.id = this.getId();
            temp.etp_name = this.getEtpName();
            temp.etp_adress = this.getEtpAdress();
            temp.email = this.getEmail();
            temp.phone_num = this.getPhoneNumb();
            temp.insc_date = this.getInscDate().ToString("yyyy-MM-dd");

            return temp;
        }

        public class displayInfo {
            public Human CurInstance { get; set; }
            public string name { get; set; }
            public int id { get; set; }
            public string etp_name { get; set; }
            public string etp_adress { get; set; }
            public string email { get; set; }
            public string phone_num { get; set; }
            public string insc_date { get; set; }
        }

    }
}