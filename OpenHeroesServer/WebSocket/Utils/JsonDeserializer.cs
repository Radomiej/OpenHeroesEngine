using System;
using System.Diagnostics;
using Newtonsoft.Json;
using OpenHeroesServer.WebSocket;
using WebSocketSharp;

namespace OpenHeroesEngine.Utils
{
    public class JsonDeserializer
    {
        public static object DeserializeMessage(MessageEventArgs e)
        {
            try
            {
                WsMessage receivedMessage = JsonConvert.DeserializeObject<WsMessage>(e.Data);
                object incomingRealObject = WsMessageBuilder.ReadWsMessage(receivedMessage);
                return incomingRealObject;
            }
            catch(Exception exception)
            {
                Debug.WriteLine("Cannot deserialize: " + exception);
            }

            return null;
        }
    }
}