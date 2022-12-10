using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
    public class ShiftSpaceAction : PlayerAction
    {
        public ActionPoints actionPointBonusCost; // This is essentially a Modifier cost, since our actual cost and cost type is determined by the space. TODO: Improve this system

        protected override void Do()
        {
        }
    }
}