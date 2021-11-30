using System.Collections.Generic;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary
{   
    /// <summary>
    /// Representa una categoria de una empresa
    /// EXPERT :  Esta clase es una clase base del programa que contiene toda la información y metodos de los  area de trabajo de las compañias por lo que se justifica con el principio expert.
    /// </summary>
    public  class AreaOfWork : IJsonConvertible
    {

        [JsonConstructor]
        public AreaOfWork()
        {

        }
        public string Name{get;set;}
        /// <summary>
        /// Crea instancias de este tipo
        /// </summary>
        public AreaOfWork(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(name);
            }
            this.Name = name;
        }
    
        public string ConvertToJsonCompany()
        {
            return null;
        }
        public string ConvertToJsonEntrepreneur()
        {return null;}
        public string ConvertToJsonOffer()
        {return null;}
        public string ConvertToJsonPermissions()
        {return null;}
        public string ConvertToJsonMaterialTypes()
        {return null;}

        public string ConvertToJsonAreaOfWork()
        {
            return JsonSerializer.Serialize(this);
        }
        
    }
}
