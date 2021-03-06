using System.Collections.Generic;
using System.Collections;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace ClassLibrary
{

    /// <summary>
    /// En esta clase se puede ver el uso del patrona Expert, y que es experto en el manejo
    /// de las ofertas de la aplicación, incluso cuando se instancia esta clase la instanciamos
    /// a través de singleton de modo de manejar una única instancia.
    /// CREATOR: Esta clase cumple con el patrón creator, ya que contiene, agrega y guarda instancias de otra clase además de ser el responsable de la carcón de las mismas.
    /// </summary>
    public class OfferManager : IJsonConvertible
    {
        public List<Entrepreneur> buyers = new List<Entrepreneur>();
        /// <summary>
        /// Catalogo de ofertas de nuestra aplicacion
        /// </summary>
        /// <typeparam List="Offer"></typeparam>
        /// <returns></returns>
        [JsonInclude]
        public List<Offer> catalog = new List<Offer>();
        /// <summary>
        /// Este es el constructor de la clase
        /// </summary>
        [JsonConstructor]
        public OfferManager()
        {
            this.catalog = new List<Offer>();
        }
        /// <summary>
        /// Este metodo lo que hace es, una vez creada la oferta se guarda en el catalogo de la aplicacion
        /// </summary>
        /// <param name="offer"></param>
        public void SaveOffer(Offer offer)
        {
            catalog.Add(offer);
        }

        public List<Offer> getLista()
        {
            this.LoadFromJsonOffer();
            return this.catalog;
        }
        /// <summary>
        /// Este metodo se utiliza para re publicar ofertas que son priodicas 
        /// </summary>
        /// <param name="id"></param>
        public void PublishOffer(int id)
        {
            Offer offer = catalog[id];
            offer.Availability = true;
        }
        /// <summary>
        /// Este metodo desabilita una oferta del catalogo
        /// </summary>
        /// <param name="id"></param>
        public void DiseableOffer(int id)
        {
            Offer offer = catalog[id];
            offer.Availability = false;
        }

        public bool Remove(long id)
        {
            foreach (Offer offer in this.catalog)
            {
                if (offer.Idd == id)
                {
                    this.catalog.Remove(offer);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Este metodo retorna las ofertas del catalogo que estan habilitadas
        /// </summary>
        /// <returns></returns>
        public string GetOffersAvailability()
        {
            string data = $"Las ofertas habilitadas son: \n";
            foreach (Offer offer in catalog)
            {
                if (offer.Availability)
                {
                    data = data + $"{offer.Idd} {offer.Name} Costo {offer.Cost} Fecha y hora de publicacion {offer.PublicationDate} \n";
                }
                else
                {
                    data = "No tienes Ofertas habilitadas para mostrar";
                }

            }
            return data;
        }
        /// <summary>
        /// El metodo siguiente permite comprar la oferta al emprendedor
        /// </summary>
        /// <param name="buyer"></param>
        /// <param name="index"></param>
        public void BuyOffer(string buyer, long index)
        {
            LoadFromJsonOffer();
            Singleton<DataManager>.Instance.LoadFromJsonEntrepreneur();
            this.buyers = Singleton<DataManager>.Instance.entrepreneurs;
            DateTime date2 = DateTime.UtcNow;


            foreach (Offer item in this.catalog)
            {
                if (item.Idd == index)
                {
                    item.Availability = false;
                    foreach (var item2 in this.buyers)
                    {
                        if (item2.Id == buyer)
                        {
                            item.Entrepreneur = buyer;
                            item.DeliveryDate = date2;

                        }
                    }
                    //  item.Entrepreneur = new Entrepreneur buyer;
                    ConvertToJsonOffer();
                }
            }
        }

        /// <summary>
        ///  El metodo crea una instacia de la oferta y la agrega al catalogo.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="material"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="department"></param>
        /// <param name="cost"></param>
        /// <param name="regularoffers"></param>
        /// <param name="tags"></param>
        /// <param name="deliverydate"></param>
        /// <param name="publicationdate"></param>
        /// <param name="offer"></param>
        public void AddOffer(string name, Material material, int quantity, double cost, string street, string city, string department, List<Permission> offerpermissions, bool regularoffers, List<string> tags, DateTime deliverydate, DateTime publicationdate, Company company)
        {
            LocationApiClient Loc = new LocationApiClient();
            Location locationoffer = Loc.GetLocation(street, city, department);
            Singleton<OfferManager>.Instance.LoadFromJsonOffer();
            long valorUltimoId = this.catalog.Count + 1;
            this.catalog.Add(new Offer(valorUltimoId, name, material, quantity, cost, locationoffer, offerpermissions, regularoffers, tags, deliverydate, publicationdate, company));
            this.ConvertToJsonOffer();
        }
        public string ConvertToJsonOffer()
        {
            string result = "{\"Items\":[";

            foreach (Offer item in this.catalog)
            {
                result = result + item.ConvertToJsonOffer() + ",";
            }

            result = result.Remove(result.Length - 1);
            result = result + "]}";

            string temp = JsonSerializer.Serialize(this.catalog);
            File.WriteAllText(@"Offer.json", temp);
            return result;
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(this.catalog, options);
        }
        public void LoadFromJsonOffer()
        {

            string json = File.ReadAllText(@"Offer.json");
            if (json != "")
            {

                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = MyReferenceHandler.Instance,
                    WriteIndented = true
                };

                this.catalog = JsonSerializer.Deserialize<List<Offer>>(json, options);
            }
        }
        public string ConvertToJsonEntrepreneur()
        { return null; }
        public string ConvertToJsonCompany()
        { return null; }
        public string ConvertToJsonPermissions()
        { return null; }
        public string ConvertToJsonMaterialTypes()
        { return null; }
        public string ConvertToJsonAreaOfWork()
        { return null; }
    }

}