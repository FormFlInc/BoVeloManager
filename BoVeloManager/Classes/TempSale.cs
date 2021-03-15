﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    // Rassemble tous les composants nécaissaires a l'encodage de la vente
    public class TempSale
    {       
        private Dictionary<BikeTemplate, int> basket = new Dictionary<BikeTemplate, int>();

        private Client client;
        private User seller;

        DateTime sale_date;
        DateTime prevision_date;

        int saleID;

        private List<Bike> bikeList;
        private BikeTemplate tempBikeTemplate;
        private List<BikeTemplate> newBikeTemplates;

        public TempSale() 
        {
            bikeList = new List<Bike>();
            newBikeTemplates = new List<BikeTemplate>();
        }

        public void setSeller()
        {
            seller = Controler.Instance.getCurrentUser();
        }
        public void setClient(Client c)
        {
            client = c;
        }

        public Dictionary<BikeTemplate, int> getBasket()
        {
            return basket;
        }

        public void addItems(CatalogBike catBike, KitTemplate size, KitTemplate color, int qnt) 
        {
            BikeTemplate tBike;
            if (existBikeTemplate(catBike, size, color, Controler.Instance.GetBikeTemplateList()))
            {
                tBike = tempBikeTemplate;
            }
            else
            {
                int tBike_id = Controler.Instance.getLastBikeTemplateId() + 1;
                tBike = new BikeTemplate(tBike_id, catBike);
                tBike.linkKitTemplate(size);
                tBike.linkKitTemplate(color);
                // add new bike template to controller list and newBikeTemplates to update db
                Controler.Instance.GetBikeTemplateList().Add(tBike);
                newBikeTemplates.Add(tBike);
            }

            addBikeTemplateToBasket(qnt, tBike);

            #region affichage console
            foreach (KeyValuePair<BikeTemplate, int> kvp in basket)
            {
                Console.WriteLine("------------");
                Console.WriteLine("Name : " + kvp.Key.getName());
                foreach (KitTemplate kit in kvp.Key.getListKit())
                {
                    Console.WriteLine("kit : " + kit.getName());
                }
                Console.WriteLine("Quantity : " + kvp.Value);                
            }
            #endregion
        }

        // Verification existence BikeTemplate dans la db
        private bool existBikeTemplate(CatalogBike catBike, KitTemplate size, KitTemplate color, List<BikeTemplate> tBike_list)
        {
            List<KitTemplate> listKit = new List<KitTemplate>();
            listKit.Add(size);
            listKit.Add(color);

            foreach (BikeTemplate bikeTemplate in tBike_list)
            {
                //x.All(y.Contains)
                //if (bikeTemplate.getCat().getName() == catBike.getName() && bikeTemplate.getListKit() == listKit)

                if (bikeTemplate.getCat().getName() == catBike.getName() && bikeTemplate.getListKit().All(listKit.Contains) && listKit.All(bikeTemplate.getListKit().Contains))
                {
                    Console.WriteLine("le bike template ajouté existe dans la base de donnée");
                    tempBikeTemplate = bikeTemplate;
                    return true;
                }
            }

            return false;
        }

        // Verification existence BikeTemplate dans le panier (basket)
        private void addBikeTemplateToBasket(int qnt, BikeTemplate tBike)
        {
            if (basket.ContainsKey(tBike))
            {
                Console.WriteLine("basket contains this bike template");

                int qnt_ = default;
                BikeTemplate tBike_ = default;

                foreach (KeyValuePair<BikeTemplate, int> kvp in basket)
                {                    
                    if (kvp.Key == tBike)
                    {
                        Console.WriteLine("bike template founded");
                        qnt_ = kvp.Value;
                        tBike_ = kvp.Key;                                                
                    }
                }

                basket.Remove(tBike);
                basket.Add(tBike_, qnt_ + qnt);
            }
            else
            {
                basket.Add(tBike, qnt);
            }
        }
        public void saveSale()
        {
            saleID = Controler.Instance.getLastSaleId() + 1;
            setSeller();
            int sellerID = seller.getId();
            int clientID = client.getId();
            DateTime sale_date = DateTime.Now;
            DateTime prevision_date = getNextPrevisionDate();

            List<User> userList = Controler.Instance.getUserList();
            List<Client> clientList = Controler.Instance.getClientList();


            Sale sale = new Sale(saleID, sellerID, clientID, "Open", sale_date, prevision_date, bikeList, userList, clientList);
            Controler.Instance.createSale(sale);

            updateTBikeInDB();
            bikeList = addBasketToSale_and_DB(basket);

            drainTempSale();
        }
        

        // creation vente
        private void createSale() { }
        // ajout un par un des velos et les rattacher a la vente par son id
        private List<Bike> addBasketToSale_and_DB(Dictionary<BikeTemplate, int> basket)
        {
            List<Bike> temp = new List<Bike>();

            foreach (KeyValuePair<BikeTemplate, int> kvp in basket)
            {
                for (int i = 0 ; i < kvp.Value ; i++)
                {
                    int bikeID = Controler.Instance.getLastBikeId() + 1;

                    Console.WriteLine(Controler.Instance.getLastBikeId());
                    Console.WriteLine(bikeID);

                    DateTime constr_date = getConstrDate();
                    Bike b = new Bike(bikeID, 0, saleID, 0, kvp.Key, constr_date);

                    Controler.Instance.createBike(b);
                    temp.Add(b);
                }
            }

            return temp;
        }
        public void updateTBikeInDB()
        {
            foreach (BikeTemplate tbike in newBikeTemplates)
            {
                Controler.Instance.createBikeTemplate(tbike);
                foreach (KitTemplate tKit in tbike.getListKit())
                {
                    Controler.Instance.link_kit_to_tbike(tbike, tKit);
                }
            }
        }

        private DateTime getNextPrevisionDate()
        {
            return DateTime.Now;
        }

        private DateTime getConstrDate()
        {
            return DateTime.Now;
        }

        public void drainTempSale()
        {
            Controler.Instance.tempSale = new TempSale();
        }

    }
}