using System;
using System.Collections.Generic;
using System.Diagnostics;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using OpenHeroesEngine.Artemis;
using OpenHeroesEngine.WorldMap.Components;
using OpenHeroesEngine.WorldMap.Events;
using OpenHeroesEngine.WorldMap.Models;
using OpenHeroesEngine.WorldMap.Models.Actions;
using Radomiej.JavityBus;
using Action = OpenHeroesEngine.WorldMap.Models.Actions.Action;

namespace OpenHeroesEngine.WorldMap.Systems.Actions
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update)]
    public class ChestActionSystem : EventBasedSystem
    {
        private Grid _grid;
        private Dictionary<long, Entity> _nodeToEntityLinker = new Dictionary<long, Entity>(500);
        private HashSet<long> _chests = new HashSet<long>(200);
        private ActionDefinition _actionDefinition;
        private List<ActionAnswer> _actionAnswers;
        private Random _random = new Random(123);
        
        public override void LoadContent()
        {
            base.LoadContent();
            _grid = BlackBoard.GetEntry<Grid>("Grid");
            _actionDefinition = new ActionDefinition("ChestAction");
            _actionAnswers = new List<ActionAnswer>
            {
                new ActionAnswer("TakeGold"),
                new ActionAnswer("TakeExperience")
            };
        }
        
        [Subscribe]
        public void AddResourceListener(AddResourceOnWorldMapEvent addResourceOnWorldMapEvent)
        {
            if (!addResourceOnWorldMapEvent.Resource.Definition.Name.Equals("Chest")) return;

            long index = _grid.GetNodeIndex(addResourceOnWorldMapEvent.Position);
            _chests.Add(index);
            
          
        }

        [Subscribe]
        public void MoveInListener(MoveInEvent moveInEvent)
        {
            long index = _grid.GetNodeIndex(moveInEvent.Current);
            if(!_chests.Contains(index)) return;
            _chests.Remove(index);
            
            Action action = new Action(_actionDefinition);
            action.AddParam("gold", 1500);
            
            DynamicActionEvent dynamicActionEvent = new DynamicActionEvent(action, _actionAnswers, moveInEvent.MoveToNextEvent.Owner);
            _eventBus.Post(dynamicActionEvent);
            Debug.WriteLine("Chest Taken");
        }
        
        [Subscribe]
        private void AnswerToActionListener(ActionResponseEvent actionResponseEvent)
        {
          if(!actionResponseEvent.ActionEvent.Action.Definition.Name.Equals("ChestAction")) return;

          Entity owner = actionResponseEvent.ActionEvent.Target;
          Fraction fraction = owner.GetComponent<Army>().Fraction;
          int amount = (int) actionResponseEvent.ActionEvent.Action.Params["gold"];
          Resource resource = new Resource(new ResourceDefinition("Gold"), amount);
          AddResourceToFractionEvent addResourceToFractionEvent = new AddResourceToFractionEvent(resource, fraction);
          _eventBus.Post(addResourceToFractionEvent);
        }
    }
}