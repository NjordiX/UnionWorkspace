using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWClient
{
    sealed internal class MessageHandler
    {
        public enum MessageType
        {
            System,
            Text,
            Debug
        }
        public static void Send(string message, MessageType type)
        {
            
        }
        
    }
}
