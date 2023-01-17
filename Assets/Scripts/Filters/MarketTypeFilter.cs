using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ImperialStruggle
{
    public class MarketTypeFilter : Conditional<Space>
    {
        enum Status { Protected, Unprotected, Isolated, }
        [SerializeField] HashSet<Resource> marketTypes = new();
        [SerializeField] HashSet<Status> requiredStatus = new();

        protected override bool Test(Space context)
        {
            if(context is Market market && (marketTypes.Count() == 0 || marketTypes.Contains(market.Resource)))
            {
                bool retVal = true;

                if (requiredStatus.Contains(Status.Protected))
                    retVal &= market.Protected;
                if (requiredStatus.Contains(Status.Unprotected))
                    retVal &= !market.Protected;
                if (requiredStatus.Contains(Status.Isolated))
                    retVal &= market.Isolated();

                return retVal; 
            }

            return false; 
        }
    }
}