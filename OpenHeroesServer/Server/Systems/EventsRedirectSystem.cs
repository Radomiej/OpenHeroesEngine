using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesServer.WebSocket;
using OpenHeroesServer.WebSocket.Models;
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
        
        [Subscribe]
        public void PlaceObjectOnMapListener(PlaceObjectOnMapEvent placeObjectOnMapEvent)
        {
            var wrappedEvent = new
            {
                Position = placeObjectOnMapEvent.Position,
                Size = placeObjectOnMapEvent.Size,
                Structure = placeObjectOnMapEvent.Entity.GetComponent<Structure>(),
                Obstacle = placeObjectOnMapEvent.Entity.GetComponent<Obstacle>(),
                Resource = placeObjectOnMapEvent.Entity.GetComponent<Resource>()
            };
            RedirectLike(placeObjectOnMapEvent, wrappedEvent, default, NetworkPersistenceType.Permanent);
        }

        [Subscribe]
        public void AddArmyListener(AddArmyEvent addArmyEvent)
        {
            RedirectLike(addArmyEvent, addArmyEvent);
        }
        
        [Subscribe]
        public void FindPathRequestListener(FindPathRequestEvent findPathRequestEvent)
        {
            FindPathEvent realEvent = new FindPathEvent(findPathRequestEvent.Start, findPathRequestEvent.End);
            _eventBus.Post(realEvent);
            findPathRequestEvent.CalculatedPath = realEvent.CalculatedPath;
            RedirectLike(findPathRequestEvent, findPathRequestEvent);
        }

        
        private void RedirectLike(object realEvent, object wrappedEvent, string channel = "public", NetworkPersistenceType persistenceType = NetworkPersistenceType.None)
        {
            var message = WsMessageBuilder.CreateWsMessage(channel, wrappedEvent);
            message.type = realEvent.GetType().FullName;
            message.persistenceType = persistenceType;
            _eventBus.Post(message);
        }
    }
}