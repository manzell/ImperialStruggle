using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using UnityEngine;

namespace ImperialStruggle
{
    public class MarketCalculation : Calculation<IEnumerable<Space>>
    {
        [SerializeField] HashSet<Map> maps;
        [SerializeField] HashSet<Resource> resources;
        [SerializeField] HashSet<Status> status; 

        enum Status { Protected, Unprotected, Isolated, }

        public override IEnumerable<Space> Calculate()
        {
            IEnumerable<Market> spaces = Game.Spaces.OfType<Market>();

            if (maps != null)
                spaces = spaces.Where(space => maps.Contains(space.map));

            if (resources != null)
                spaces = spaces.Where(space => resources.Contains(space.Resource));

            if (status.Contains(Status.Isolated))
                spaces = spaces.Where(space => space.Isolated(null)); // TODO Fix this. Should Calculations require a PlayerAction context?

            if (status.Contains(Status.Protected))
                spaces = spaces.Where(space => space.Protected);

            if (status.Contains(Status.Unprotected))
                spaces = spaces.Where(space => !space.Protected);

            return spaces; 
        }
    }
}
