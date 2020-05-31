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
    public class RecruitmentCampaignSystem : EventBasedSystem
    {
        private Grid _grid;
        private Dictionary<long, Entity> _nodeToEntityLinker = new Dictionary<long, Entity>(500);
        private HashSet<long> _habitats = new HashSet<long>();
        private ActionDefinition _actionDefinition;
        private List<ActionAnswer> _actionAnswers;
        private Random _random = new Random(123);

        public override void LoadContent()
        {
            base.LoadContent();
            _grid = BlackBoard.GetEntry<Grid>("Grid");
            _actionDefinition = new ActionDefinition("RecruitmentAction");
            _actionAnswers = new List<ActionAnswer>
            {
                new ActionAnswer("TakeMax"),
                new ActionAnswer("Cancel")
            };
        }

        [Subscribe]
        public void AddHabitatsListener(PlaceObjectOnMapEvent placeObjectOnMapEvent)
        {
            Entity entity = placeObjectOnMapEvent.Entity;
            if (!entity.HasComponent<Structure>()) return;
            Structure structure = entity.GetComponent<Structure>();
            if (!structure.Definition.Name.EndsWith("Habitat")) return;

            long index = _grid.GetNodeIndex(placeObjectOnMapEvent.Position);
            _habitats.Add(index);
            _nodeToEntityLinker.Add(index, entity);
        }

        [Subscribe]
        public void MoveInListener(MoveInEvent moveInEvent)
        {
            long index = _grid.GetNodeIndex(moveInEvent.Current);
            if (!_habitats.Contains(index)) return;

            Habitat habitat = _nodeToEntityLinker[index].GetComponent<Habitat>();
            Action action = new Action(_actionDefinition);
            action.AddParam("population", habitat.Current);
            action.AddParam("habitat", habitat);

            DynamicActionEvent dynamicActionEvent =
                new DynamicActionEvent(action, _actionAnswers, moveInEvent.MoveToNextEvent.Owner);
            _eventBus.Post(dynamicActionEvent);
            Debug.WriteLine("Recruitment Start");
        }

        [Subscribe]
        private void AnswerToActionListener(ActionResponseEvent actionResponseEvent)
        {
            if (!actionResponseEvent.ActionEvent.Action.Definition.Name.Equals("RecruitmentAction")) return;
            if (actionResponseEvent.SelectedAnswer.Name.Equals("Cancel")) return;

            Habitat habitat = actionResponseEvent.ActionEvent.Action.Params["habitat"] as Habitat;

            Entity owner = actionResponseEvent.ActionEvent.Target;
            Army army = owner.GetComponent<Army>();
            Fraction fraction = army.Fraction;
            int maxHire = (int) actionResponseEvent.ActionEvent.Action.Params["population"];
            int amount = maxHire * 17;
            Resource resource = new Resource(new ResourceDefinition("Gold"), amount);

            RemoveResourceFromFractionEvent removeResource = new RemoveResourceFromFractionEvent(resource, fraction);
            _eventBus.Post(removeResource);
            if (!removeResource.Success && removeResource.CountOfDividend == 0) return;

            int hireAmount = removeResource.Success ? maxHire : removeResource.CountOfDividend;
            army.Creatures[0].Count += hireAmount;
            habitat.Current -= hireAmount;
        }
    }
}