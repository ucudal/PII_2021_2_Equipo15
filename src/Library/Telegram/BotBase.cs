using System.Collections.Generic;
namespace ClassLibrary
{
    /*
    Singleton: Utilizamos el patrón Singleton ya que de esta manera esta instancia sería única, 
    por lo que los usuarios no usarían distintas instancias de este objeto,
    y también esto nos posibilita acceder a todo lo relacionado con esta clase desde 
    cualquier otra clase.
    */

    /// <summary>
    /// Esta clase se encarga de aplicar la lógica correspondiente a la inicialización 
    /// del programa y la administración de los Handlers.
    /// De igual manera, se encarga de crear los mismos y corresponderlos con la función
    /// SetNext(), también se encarga de crear una instancia para el Sender
    /// (recibidor de mensajes) y para el Sender(enviador de mensajes).
    /// 
    /// Esta clase posee una unica instancia creada mediante un Singleton, por lo que es accesible desde todo el programa.
    /// </summary>
    public class BotBase
    {
       // #region Singleton
        
        private BotBase(){}
        private static BotBase instance;
        public static BotBase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BotBase();
                }

                return instance;
            }
        }
      //  #endregion

        private List<UserRequest> requestList = new List<UserRequest>();
        public void Start()
        {
            IRecive reciever = new TelegramReciv();
            reciever.StartRecive();
            while (reciever.IsRecive())
            {
                
            }
        }
        /// <summary>
        /// Recibe input desde el Receiver y de esta manera y en conjunción con el Sender, 
        /// procesa los mensajes recibidos y brinda una respuesta.
        /// Crea instancias de los handlers, ya que de esta manera es posible que muchos usuarios 
        /// utilicen el bot a la vez, ya que se creara una cadena de responsabilidades para los mensajes.
        /// </summary>
        /// <param name="id">Id del usuario que ingresa la información.</param>
        /// <param name="message">Mensaje del usuario.</param>
        public void GetInput(long id, string message)
        {
            AbstractHandler<UserRequest> welcomeHandler = new WelcomeHandler();
            AbstractHandler<UserRequest> invitationCodeHandler = new InvitationCodeHandler();
            AbstractHandler<UserRequest> initialHandler = new InitialHandler();
            AbstractHandler<UserRequest> userChoiceCreation = new UserChoiceCreationHandler();
            AbstractHandler<UserRequest> CompanyRegistrationHandler = new CompanyRegistrationHandler();
            AbstractHandler<UserRequest> EntrepreneurRegistrationHandler = new EntrepreneurRegistrationHandler();
            
            welcomeHandler.SetNext(invitationCodeHandler/*.SetNext(initialHandler.SetNext(userChoiceCreation))*/);
    
            // AbstractHandler<UserRequest> companyHandler = new CompanyHandler();
            // initialHandler.SetNext(companyHandler.SetNext());
            
            ISend telegramSender = new TelegramSend();
            UserRequest userRequest = GetRequestById(id, message);
            UserRequest response = welcomeHandler.Handle(userRequest);
            UserRequest response0 = invitationCodeHandler.Handle(userRequest);
            UserRequest response1 = initialHandler.Handle(userRequest);
            UserRequest response2 = userChoiceCreation.Handle(userRequest);
            telegramSender.SendMessage(response.Id, response.OutgoingMsg);
            //telegramSender.SendMessage(response2.Id, response2.OutgoingMsg);
        }
        public UserRequest GetRequestById(long id, string message) 
        {
            foreach (UserRequest request in requestList)
            {
                if (request.Id == id)
                {
                    
                    request.ArrivedMsg = message;
                    return request;
                   

                }
            }
            UserRequest newRequest = new UserRequest(id, message);
            requestList.Add(newRequest);
            return newRequest;
        }
    }
}