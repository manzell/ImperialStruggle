using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialStruggle
{
    public class JohnLawAction : PlayerAction
    {
        [SerializeField] List<Space> scotlandSpaces = new List<Space>();

        protected override Task Do()
        {

            return Task.CompletedTask; 
        }
    }
}