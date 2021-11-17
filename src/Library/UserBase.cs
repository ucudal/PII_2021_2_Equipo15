using System.Collections.Generic;
using System;
namespace ClassLibrary
{   /// <summary>
    /// Esta clase define las propiedas y comportamiento que comparten los diferentes usuarios de la empresa
    /// </summary>
    public abstract class UserBase
    { 
        /// <summary>
        /// Propiedad Nombre del usuario
        /// </summary>
        public string Name { get;  set; }

        /// <summary>
        /// Propiedad ubicación de la empresa
        /// </summary>
        public Location Location{get; set;}

        /// <summary>
        /// Propiedad telefono del usuario
        /// </summary>
        public string Phone { get; set;}  

        /// <summary>
        /// Instancia de clase AreaOfWork que representa una categoria de la empresa
        /// </summary>
        public AreaOfWork AreaOfWork {get; set;}
        
        /// <summary>
        /// Lista de habilitaciones que posee un usario
        /// </summary>
        /// <typeparam list="Permission"></typeparam>
        /// <returns></returns>
        private List<Permission> permissions = new List<Permission>();


        /// <summary>
        /// Permite crear instancias de los usuarios del programa
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="location"></param>
        /// <param name="area"></param>

        protected UserBase(long id,string name, string phone, Location location, string area)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(name);
            }

            if (String.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentNullException(phone);
            }
            if (String.IsNullOrWhiteSpace(area))
            {
                throw new ArgumentNullException(area);
            }
            this.Name = name;
            
            this.Phone = phone;

            this.Location = location;

            this.AreaOfWork = new AreaOfWork(area);
        }

        /// <summary>
        ///Permite agregar permisios a un usuario
        /// </summary>
        public void AddPermission(Permission permission)
        {
            permissions.Add(permission);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>

        /// <summary>
        /// Permite eliminar un permiso de un usuario
        /// </summary>
        public void RemovePermission(int index)
        {
            this.permissions.RemoveAt(index);
        }

        /// <summary>
        /// permite obtener la lista de permisos de un usuario
        /// </summary>
        public string GetPermissions()
        {
            string data = $"La lista de Permisos del usuario son: \n";

            foreach (var permission in permissions)
            {
                data = data + $"Name: {permission.Name} \n";
                
            }
            return data;
        }
    }
}