using System.Diagnostics;
using Newtonsoft.Json;
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
//            Debug.Log("Message: " + String.Format ("{0}", e.Data));
            WsMessage receivedMessage = JsonConvert.DeserializeObject<WsMessage>(e.Data);
            object incomingRealObject = WsMessageBuilder.ReadWsMessage(receivedMessage);
            QueueEvents.Instance.Add(incomingRealObject);
        }

        protected override void OnOpen ()
        {
            Debug.WriteLine("WS Player Connected");
            SendPersistentMessages();
            JEventBus.GetDefault().Register(this);
            _player = PlayerManager.Instance.CreatePlayer();
            _player.Connected = true;

            var yourPlayerEvent = new YourPlayerEvent(_player);
            Send(WsMessageBuilder.CreateWsText("private", yourPlayerEvent));
            
          
            
            var createPlayerEvent = new CreatePlayerEvent(_player);
            Send(WsMessageBuilder.CreateWsText("player", createPlayerEvent));
            QueueEvents.Instance.Add(createPlayerEvent); //TODO add logic
            // Observable.NextFrame().ObserveOnMainThread().Subscribe(unit =>
            // {
            //     JEventBus.GetDefault().Post(createPlayerEvent);
            // });
          
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
    }
}