using System;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using OpenHeroesServer.Server.Events;
using OpenHeroesServer.WebSocket;
using Radomiej.JavityBus;

namespace OpenHeroesServer.Server.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class EventsRedirectSystem : EventBasedSystem
    {
        public override void LoadContent()
        {
            base.LoadContent();
        }

        [Subscribe]
        public void MoveInListener(MoveInEvent moveInEvent)
        {
            var wrappedEvent = new
            {
                Current = moveInEvent.Current,
                Previous = moveInEvent.Previous,
                Army = moveInEvent.MoveToNextEvent.Owner.GetComponent<Army>()
            };
            RedirectLike(moveInEvent, wrappedEvent);
        }
        
        [Subscribe]
        public void EndGameListener(EndGameEvent endGameEvent)
        {
            var wrappedEvent = new
            {
                Fraction = endGameEvent.Winner.GetComponent<Army>().Fraction,
            };
            RedirectLike(endGameEvent, wrappedEvent);
        }
        
        [Subscribe]
        public void TurnEndListener(TurnEndEvent turnEndEvent)
        {
            RedirectLike(turnEndEvent, turnEndEvent);
        }

        private void RedirectLike(object realEvent, object wrappedEvent, string channel = "public")
        {
            var message = WsMessageBuilder.CreateWsMessage(channel, wrappedEvent);
            message.type = realEvent.GetType().FullName;
            _eventBus.Post(message);
        }
    }
}