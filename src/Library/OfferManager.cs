using System.Collections.Generic;
using System;
using System.Collections;

namespace ClassLibrary
{   
    
    public class OfferManager 
    {
        public List<Offer> catalog = new List<Offer>();
         public OfferManager()
        {
            
        }
        
        //public void SaveOffer(string name,string materialdescription,string materialname,double cost,bool availability,string regularoffers,ArrayList tags,DateTime deliverydate,DateTime publicationdate,Company company)
       // {
         //   Offer oferta =  new Offer(name,materialdescription,materialname,cost,availability,regularoffers,tags,deliverydate,publicationdate,company);
          //  this.catalog.Add(oferta);
       //}
       public void SaveOffer(Offer offer)
        {   
            this.catalog.Add(offer);
        }
        public void PublishOffer(int id)
        {
           Offer offer = this.catalog[id];
            offer.Availability = true;
        }
    
        public void PrintmyOfferts(Company company)
        {

            foreach (Offer offer in this.catalog)
            {
                if(offer.Company == company)
                {
                    Console.WriteLine($"{offer.id} {offer.Name} Costo {offer.Cost} Fecha y hora de publicacion {offer.PublicationDate}");
                }
                
            }
        }
        public void PrintOffertsAvilitiy(Company company)
        {
            foreach (Offer offer in this.catalog)
            {
                if(offer.Company == company)
                {
                    if(offer.Availability)
                    {
                    Console.WriteLine($"{offer.id} {offer.Name} Costo {offer.Cost} Fecha y hora de publicacion {offer.PublicationDate}");
                    }else Console.WriteLine("No tienes Ofertas habilitadas para mostrar");
                }
                
            }
        }
    }

}