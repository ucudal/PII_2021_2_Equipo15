using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Este handler implementa el patrón Chain of Responsability y es el encargado de manejar el comando /registrarse
    /// En caso de que el usuario ingrese la opción empresa se le desplegan las opciones para registrarse como empresa.
    /// En cambio si el usuario ingresa la opción emprendedor se le despegan las opciones para registrarse como emprendedor.
    /// Cuando se finaliza de pedir los datos al usuario, se agrega el usuario al sistema.
    /// </summary>
    public class RegistrationHandler : BaseHandler
    {
        /// <summary>
        /// String temporal para concatenar una respuesta al usuario
        /// </summary>
        /// <returns></returns>
        private StringBuilder responsetemp = new StringBuilder();

        /// <summary>
        /// Lista que maneja los permisos que se van agregando al emprendedor
        /// </summary>
        /// <typeparam name="Permission">Campo del tipo Permission</typeparam>
        /// <returns></returns>
        private List<Permission> userpermissions = new List<Permission>();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="RegistrationHandler"/>.
        /// Procesa el mensaje /registrarse
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public RegistrationHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/registrarse" };
        }

        /// <summary>
        /// Este metodo es el encargado de procesar el mensaje que le llega de telegram y enviar una respuesta
        /// </summary>
        /// <param name="message"> El mensaje que llega para procesar</param>
        /// <param name="response">La respuesta del mensaje procesado </param>
        /// <returns></returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            var _myuserdata = Singleton<TelegramUserData>.Instance.userdata;
            var _mypermissions = Singleton<TelegramUserData>.Instance.permissionsDict;
            if (!_myuserdata.ContainsKey(message.UserId))
            {
                _myuserdata.Add(message.UserId, new Collection<string>());
            }

            if (message.Text.ToLower().Equals("/registrarse"))
            {

                if (Singleton<DataManager>.Instance.GetEntrepreneur(message.UserId) != null | Singleton<DataManager>.Instance.GetCompany(message.UserId) != null)
                {

                    response = "Usted ya se encuentra registrad@";
                    return true;
                }
                else
                {
                    _mypermissions.Remove(message.UserId);
                    _mypermissions.Add(message.UserId, new Collection<string>());
                    _myuserdata[message.UserId].Add(message.Text.ToLower());
                    response = "Registrarse como Empresa o como Emprendedor\n/Empresa\n/Emprendedor\no cancela con /cancel ";
                    return true;
                }
            }

            if (_myuserdata[message.UserId].Count >= 1)
            {
                if (_myuserdata[message.UserId][0].ToLower().Contains("/registrarse"))
                {

                    if (_myuserdata[message.UserId].Count >= 1 && message.Text.ToLower().Contains("/empresa"))
                    {
                        _myuserdata[message.UserId].Add(message.Text.ToLower());
                        response = "Ingrese el código de invitación";
                        return true;
                    }
                    if (_myuserdata[message.UserId].Count == 1 && message.Text.ToLower().Equals("/emprendedor"))
                    {
                        _myuserdata[message.UserId].Add(message.Text);
                        response = "Ingrese nombre de su emprendimiento";
                        return true;
                    }

                    if (_myuserdata[message.UserId][1].ToLower().Contains("/empresa"))
                    {
                        if (message.Text.ToLower().Equals("1234") && _myuserdata[message.UserId].Count == 2)
                        {
                            _myuserdata[message.UserId].Add(message.Text.ToLower());
                            response = "Ingrese el nombre de la empresa";
                            return true;
                        }
                        if (!message.Text.ToLower().Equals("1234") && _myuserdata[message.UserId].Count == 2)
                        {

                            response = "Código incorrecto, intente nuevamente";
                            return true;
                        }

                        switch (_myuserdata[message.UserId].Count)
                        {
                            case 3:
                                response = "Ingrese su teléfono";
                                _myuserdata[message.UserId].Add(message.Text);
                                return true;

                            case 4:
                                _myuserdata[message.UserId].Add(message.Text);
                                response = "Ingrese Calle y Numero de puerta";
                                return true;

                            case 5:
                                _myuserdata[message.UserId].Add(message.Text);
                                response = "Ingrese Ciudad";
                                return true;

                            case 6:
                                _myuserdata[message.UserId].Add(message.Text);
                                response = "Ingrese Departamento";
                                return true;

                            case 7:
                                _myuserdata[message.UserId].Add(message.Text);
                                responsetemp.Append("Ingrese el rubro de la empresa\n");
                                responsetemp.Append($"{Singleton<DataManager>.Instance.GetTextToPrintAreaOfWork()}");
                                response = $"{responsetemp}";
                                responsetemp.Clear();
                                return true;

                            case 8:
                                if (Singleton<DataManager>.Instance.CheckAreaOfWork(Int16.Parse(message.Text)))
                                {
                                    _myuserdata[message.UserId].Add(Singleton<DataManager>.Instance.areaofwork[Int32.Parse(message.Text)].Name);
                                    Singleton<DataManager>.Instance.AddCompany(message.UserId, _myuserdata[message.UserId][3], _myuserdata[message.UserId][4], _myuserdata[message.UserId][5], _myuserdata[message.UserId][6], _myuserdata[message.UserId][7], _myuserdata[message.UserId][8]);
                                    response = $"Se creó la Empresa correctamente\n \nPara ver las siguientes acciones posibles ingrese /help";
                                    _myuserdata.Remove(message.UserId);
                                    return true;
                                }
                                else
                                {
                                    response = "Dato Mal ingresado, ingrese un número de la lista";
                                    return true;
                                }
                        }
                    }

                    if (_myuserdata[message.UserId][1].ToLower().Contains("/emprendedor"))
                    {
                        switch (_myuserdata[message.UserId].Count)
                        {
                            case 2:
                                _myuserdata[message.UserId].Add(message.Text);
                                response = "Ingrese su teléfono";
                                return true;

                            case 3:
                                _myuserdata[message.UserId].Add(message.Text);
                                response = "Ingrese calle y número de puerta";
                                return true;

                            case 4:
                                _myuserdata[message.UserId].Add(message.Text);
                                response = "Ingrese ciudad";
                                return true;

                            case 5:
                                _myuserdata[message.UserId].Add(message.Text);
                                response = "Ingrese departamento";
                                return true;

                            case 6:
                                _myuserdata[message.UserId].Add(message.Text);
                                response = $"Ingrese un rubro\n{Singleton<DataManager>.Instance.GetTextToPrintAreaOfWork()}";
                                return true;

                            case 7:
                                if (Singleton<DataManager>.Instance.CheckAreaOfWork(Int16.Parse(message.Text)))
                                {
                                    _myuserdata[message.UserId].Add(Singleton<DataManager>.Instance.areaofwork[Int32.Parse(message.Text)].Name);
                                    response = "Ingrese una especialización de su Emprendimiento";
                                    return true;
                                }
                                else
                                {
                                    response = "Dato mal ingresado, ingrese un número de la lista";
                                    return true;
                                }

                            case 8:
                                _myuserdata[message.UserId].Add(message.Text);
                                response = $"Como emprendedor tiene algún permiso especial? Si/No";
                                return true;

                            case 9:
                                if (message.Text.ToUpper().Equals("SI"))
                                {
                                    _myuserdata[message.UserId].Add($"permiso = {message.Text}");
                                    responsetemp.Clear();
                                    responsetemp.Append("Ingrese el permiso \n");
                                    responsetemp.Append($"{Singleton<DataManager>.Instance.GetTextToPrintPermission()}\n");
                                    response = $"{responsetemp}";
                                    return true;
                                }
                                else if (message.Text.ToUpper().Equals("NO"))
                                {
                                    _myuserdata[message.UserId].Add($"{message.Text}");
                                    if (_mypermissions[message.UserId].Count > 0)
                                    {
                                        responsetemp.Clear();
                                        responsetemp.Append("Se agregan los siguientes permisos \n");
                                        for (int i = 0; i < _mypermissions[message.UserId].Count; i++)
                                        {
                                            this.userpermissions.Add(Singleton<DataManager>.Instance.GetPermissionByIndex(Int32.Parse(_mypermissions[message.UserId][i])));
                                            responsetemp.Append($"- {Singleton<DataManager>.Instance.GetPermissionByIndex(Int32.Parse(_mypermissions[message.UserId][i])).Name}\n");

                                        }
                                        responsetemp.Append($"Presione /continuar");
                                        response = $"{responsetemp}";
                                        responsetemp.Clear();
                                    }
                                    else
                                    {
                                        response = "No se agregan permisos especiales /continuar";

                                    }
                                    return true;
                                }
                                else
                                {
                                    response = "Dato mal ingresado debe ingresar Si o No";
                                    return true;
                                }

                            case 10:
                                if (_myuserdata[message.UserId][9].ToUpper().Contains("SI"))
                                {
                                    if (!_mypermissions[message.UserId].Contains(message.Text))
                                    {
                                        _mypermissions[message.UserId].Add(message.Text);
                                        _myuserdata[message.UserId].RemoveAt(9);
                                        response = "Desea Agregar otro Permiso? Si/No";
                                        return true;
                                    }
                                    else
                                    {
                                        _myuserdata[message.UserId].RemoveAt(9);
                                        response = "Ya tiene este permiso asignado \nDesea Agregar otro Permiso? Si/No";
                                        return true;
                                    }
                                }
                                else
                                {
                                    _mypermissions[message.UserId].Add(message.Text);
                                    _myuserdata[message.UserId].Add(message.Text);
                                    response = "Para finalizar presione /ok";
                                    return true;
                                }

                            case 11:

                                Singleton<DataManager>.Instance.AddEntrepreneur(message.UserId, _myuserdata[message.UserId][2], _myuserdata[message.UserId][3], _myuserdata[message.UserId][4], _myuserdata[message.UserId][5], _myuserdata[message.UserId][6], _myuserdata[message.UserId][7], _myuserdata[message.UserId][8], this.userpermissions);
                                response = "Se creó el Emprendedor correctamente\n Para ver las siguientes acciones posibles ingrese /help";
                                _myuserdata.Remove(message.UserId);
                                _mypermissions.Remove(message.UserId);
                                return true;

                        }
                    }
                }
            }
            response = String.Empty;
            return false;
        }
    }
}


