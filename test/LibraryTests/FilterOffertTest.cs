//--------------------------------------------------------------------------------
// <copyright file="TrainTests.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary;
using NUnit.Framework;
using System;
using System.Collections;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase <see cref="OfferManager"/>.
    /// </summary>
    [TestFixture]
    public class SearchTest
    {
        /// <summary>
        /// emprededor para pruebas
        /// </summary>
        private Entrepreneur entrepreneur;
        /// <summary>
        /// Catalogo para pruebas.
        /// </summary>
        private OfferManager offerAdmin;
        /// <summary>
        /// Material para pruebas
        /// </summary>
        private Material material;
        /// <summary>
        /// Compania para la empresa
        /// </summary>
        private Company company;

        /// <summary>
        /// Buscador para pruebas
        /// </summary>
        private Search searcher ;
        private Offer offer ;

        /// <summary>
        /// Crea las intancias utiilzadas en los test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.offerAdmin =  new OfferManager();
            this.searcher =  new Search();
            this.company = new Company("compania1","098239334","Las Piedras","Construcción");
            Permission permissionA = new Permission("Materiales inflamables");
            Permission permissionB = new Permission("Residuos medicos");

            DataManager dataManager  = new DataManager();
            dataManager.AddPermission(permissionA);
            dataManager.AddPermission(permissionB);
            
            this.company.AddPermission(dataManager.GetPermissions()[0]);
            
                

            ArrayList tags  = new ArrayList();
            tags.Add("tag1");
            tags.Add("tag");              
            DateTime publicationDate = new DateTime(2008, 3, 1, 7, 0, 0);
            DateTime deliverydate = new DateTime();
            MaterialType materialType  =  new MaterialType("Tela", "Recortes de tela de 1x1");
            this.material =  new Material("Tela",materialType,"200",100,"Berro 1231");
            this.offer = new Offer("Promoción de verano",this.material,"Berro1231",200.00,true,tags,deliverydate,publicationDate,this.company);
            Singleton<OfferManager>.Instance.SaveOffer(this.offer);

            Permission permissionC = new Permission("Materiales inflamables");
            this.entrepreneur = new Entrepreneur("Empre2","091234567","Galicia 1234","Construcción","Trabajo en altura");
            Singleton<OfferManager>.Instance.BuyOffer(this.entrepreneur,0);
        }

        /// <summary>
        /// Prueba de filtrado por ubicación
        /// </summary>
        [Test]
        public void FilterByLocation()
        {
            Assert.That(this.searcher.GetOfferByLocation("Berro1231"),Contains.Substring("Berro1231"));
            
            // Assert.AreEqual(Singleton<OfferManager>.Instance.catalog[0].Entrepreneur,this.entrepreneur);
        }


        /// <summary>
        /// Prueba de filtrado por palabra (tag)
        /// </summary>
        [Test]
        public void FilterByWord()
        {
            Assert.That(this.searcher.GetOfferByWord("tag1"),Contains.Substring("Promoción de verano"));
            
            // Assert.AreEqual(Singleton<OfferManager>.Instance.catalog[0].Entrepreneur,this.entrepreneur);
        }

    }
} 