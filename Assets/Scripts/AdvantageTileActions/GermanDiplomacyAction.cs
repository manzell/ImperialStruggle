using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ImperialStruggle
{
    public class GermanDiplomacyAction : PlayerAction
    {
        [SerializeField] public ActionPoints awardAP;

        protected override Task Do()
        {
            return Task.CompletedTask; 
        }
    }
}