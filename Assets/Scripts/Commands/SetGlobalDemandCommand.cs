using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq;

namespace ImperialStruggle
{
    public class SetGlobalDemandCommand : Command
    {
        public static System.Action setGlobalDemandEvent;
        [SerializeField] Calculation<IEnumerable<Resource>> inputResources; 

        public override void Do(IAction action)
        {
            if (Phase.CurrentPhase is PeaceTurn peaceTurn)
            {
                foreach(Resource resource in inputResources.Calculate(action))
                {
                    peaceTurn.globalDemandResources.Add(resource);
                    Debug.Log($"{resource.Name} is in Global Demand!");
                }

                setGlobalDemandEvent?.Invoke();
            }
        }
    }

    public class RandomGlobalDemandResources : Calculation<IEnumerable<Resource>>
    {
        [SerializeField] List<Resource> resources;
        [SerializeField] int numResources; 
        protected override IEnumerable<Resource> Calc(IAction context) => resources.OrderBy(x => Random.value).Take(numResources);
    }
}