using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class MarketCalculation : Calculation<HashSet<Space>>
    {
        [SerializeField] HashSet<Map> maps = new();
        [SerializeField] HashSet<Region> regions = new();
        [SerializeField] HashSet<Resource> resources = new();
        [SerializeField] HashSet<Status> status = new(); 

        enum Status { Protected, Unprotected, Isolated, }

        protected override HashSet<Space> Calc(Player player)
        {
            IEnumerable<Market> spaces = Game.Spaces.OfType<Market>();

            if (maps.Count > 0)
                spaces = spaces.Where(space => maps.Contains(space.map));
            if (regions.Count > 0)
                spaces = spaces.Where(space => regions.Contains(space.data.Region));
            if (resources != null && resources.Count > 0)
                spaces = spaces.Where(space => resources.Contains(space.Resource));
            if (status != null && status.Contains(Status.Isolated))
                spaces = spaces.Where(space => space.Isolated(null)); // TODO Fix this. Should Calculations require a PlayerAction context?
            if (status != null && status.Contains(Status.Protected))
                spaces = spaces.Where(space => space.Protected);
            if (status != null && status.Contains(Status.Unprotected))
                spaces = spaces.Where(space => !space.Protected);

            return new HashSet<Space>(spaces); 
        }
    }
}
