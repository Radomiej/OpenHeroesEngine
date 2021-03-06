using System.Diagnostics;
using Newtonsoft.Json;
using OpenHeroesEngine.Utils;
using OpenHeroesServer.Server;
using OpenHeroesServer.Server.Events;
using OpenHeroesServer.Server.Models;
using Radomiej.JavityBus;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace OpenHeroesServer.WebSocket
{
    public class PlayerWsService : WebSocketBehavior
    {
        private JPlayer _player;
        
        protected override void OnClose (CloseEventArgs e)
        {
            Debug.WriteLine("WS player service close");
            JEventBus.GetDefault().Unregister(this);
            _player.Connected = false;
        }

        protected override void OnMessage (MessageEventArgs e)
        {
            object incomingRealObject = JsonDeserializer.DeserializeMessage(e);
            QueueEvents.Instance.Add(incomingRealObject);
        }

        protected override void OnOpen ()
        {
            Debug.WriteLine("WS Player Connected");
            SendPersistentMessages();
            JEventBus.GetDefault().Register(this);
            _player = PlayerManager.Instance.CreatePlayer();
            _player.Connected = true;
            _player.PlayerWsService = this;
            
            var yourConnectionId = new YourConnectionIdEvent(_player);
            Send(WsMessageBuilder.CreateWsText("private", yourConnectionId));
        }

        private void SendPersistentMessages()
        {
            foreach (var wsMessage in JavityWebSocketServer.GetInstance().GetPersistentMessages())
            {
                SendMessageListener(wsMessage);
            }
        }

        [Subscribe]
        public void SendMessageListener(WsMessage wsMessageEvent)
        {
            string message = JsonConvert.SerializeObject(wsMessageEvent);
            Send(message);
        }

        public void SendPrivate(object privateEventToSend)
        {
            Send(WsMessageBuilder.CreateWsText("private", privateEventToSend));
        }
    }
}