using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;

namespace ClassLibrary
{
    public class TelegramReciv : IRecive
    {
         private TelegramBot TelegramBot{set; get; }
        private ITelegramBotClient Bot{get; set; }
        private bool receiving;
      public TelegramReciv()
     
      {
        this.TelegramBot = TelegramBot.Instance;
        this.Bot = TelegramBot.Client;
        
      }
        public void StartRecive()
        {
            Bot.StartReceiving();
            this.receiving = true;
            Console.WriteLine("Se empiezan a recibir los mensajes");
        }
        public void StopRecive()
        {
            Bot.StopReceiving();
            this.receiving = false;
            Console.WriteLine("Se dejan de recibir los mensajes");
        }
        public bool IsRecive()
        {
            return receiving;
        }
        
    }
}