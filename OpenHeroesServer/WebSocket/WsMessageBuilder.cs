using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using OpenHeroesServer.Server.Events;
using OpenHeroesServer.WebSocket.Models;

namespace OpenHeroesServer.WebSocket
{
    public class WsMessageBuilder
    {
        private static Dictionary<string, Type> _typeBindings = new Dictionary<string, Type>()
        {
            {typeof(YourPlayerEvent).FullName, typeof(YourPlayerEvent)}
        };

        public static void AddBinding(Type realType)
        {
            System.Diagnostics.Debug.Assert(realType.FullName != null, "realType.FullName != null");
            _typeBindings.Add(realType.FullName, realType);
        }

        public static void AddBinding(string typeText, Type realType)
        {
            _typeBindings.Add(typeText, realType);
        }

        public static string CreateWsText(string channelName, object eventToSend)
        {
            WsMessage wsMessage = CreateWsMessage(channelName, eventToSend);
            return CreateWsText(wsMessage);
        }

        public static string CreateWsText(WsMessage wsMessage)
        {
            string textMessage = JsonConvert.SerializeObject(wsMessage);
            return textMessage;
        }

        public static WsMessage CreateWsMessage(string channelName, object realMessageToSend,
            NetworkPersistenceType networkPersistenceType = NetworkPersistenceType.None)
        {
            string type = realMessageToSend.GetType().FullName;
            var message = new WsMessage
            {
                channel = channelName, 
                message = JsonConvert.SerializeObject(realMessageToSend), 
                type = type,
                persistenceType = networkPersistenceType
            };
            return message;
        }

        public static object ReadWsText(string rawMessage)
        {
            WsMessage message = JsonConvert.DeserializeObject<WsMessage>(rawMessage);
            return ReadWsMessage(message);
        }

        public static object ReadWsMessage(WsMessage message)
        {
            if (!_typeBindings.ContainsKey(message.type))
            {
                String exceptionMessage = String.Format("Given type: {0} don`t have binding in WsMessageBuilder.",
                    message.type);
                Debug.WriteLine(exceptionMessage);
                return new OperationCanceledException();
//                throw new UnassignedReferenceException(exceptionMessage);
            }

            Type jsonType = _typeBindings[message.type];
            return JsonConvert.DeserializeObject(message.message, jsonType);
        }
    }
}