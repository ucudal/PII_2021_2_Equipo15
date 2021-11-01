using System.Collections.Generic;
using System.Collections;
using System;
namespace ClassLibrary
{
    public class Offer
    {
        //public string Material{get;set;}
        public string Name {get;set;}
        public double Cost{get;set;}

        public string Location{get;set;}
        public bool Availability{get;set;}
        public string RegularOffers{get;set;}
        public ArrayList Tags{get;set;}
        public DateTime DeliveryDate{get;set;}
        public DateTime PublicationDate{get;set;}
        public Company Company {get;set;}
        public Entrepreneur Entrepreneur {get;set;}
        public MaterialType Material{get;set;}
        public List<Permission> permissions = new List<Permission>();
        public int id {get;}
        
        public Offer(string name,string materialname,string location,string materialdescription,double cost,bool availability, /*string regularoffers*/ ArrayList tags, DateTime deliverydate, DateTime publicationdate, Company offer)
        {
            this.id = id +1; 
            this.Name = name;
            this.Material = new MaterialType(materialname,materialdescription);
            this.Location = location;
            this.Cost = cost;
            this.Availability = availability;
            this.Tags = tags;
            this.DeliveryDate = deliverydate;
            this.PublicationDate = publicationdate;
            this.Company =  offer;
        }
        
        public void AddPermission(string permission)
        {
            Permission newPermission = new Permission(permission);
            permissions.Add(newPermission);
        }
        public List<string> getOffert(Entrepreneur entrepreneur)
        {
            if (this.Availability)
            {
                this.Entrepreneur = entrepreneur;
                this.Availability = false;          
            }
            return this.Company.DataCompany();
        }
      
        
    }
}