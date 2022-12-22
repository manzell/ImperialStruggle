using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
    public class UI_ActionPoints : SerializedMonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] GameObject actionPointPrefab;
        [SerializeField] Dictionary<ActionPoint.ActionTier, GameObject> actionTiers = new ();

        Dictionary<string, UI_ActionPoint> APtiles = new ();

        private void Start()
        {
            player.ActionPoints.AdjustAPEvent += UpdateTiles;
        }

        void AddTile(ActionPoint ap)
        {
            string name = $"{ap.type}-{ap.tier}-{ap.conditionText}";

            if (!APtiles.ContainsKey(name))
                APtiles.Add(name, Instantiate(actionPointPrefab, actionTiers[ap.tier].transform).GetComponent<UI_ActionPoint>());

            APtiles[name].SetTile(ap);
        }

        void RemoveTile(ActionPoint ap)
        {
            string name = $"{ap.type}-{ap.tier}-{ap.conditionText}";
            Destroy(APtiles[name].gameObject);
            APtiles.Remove(name);
        }

        public void UpdateTiles()
        {
            // First cycle through our existing APTile keys and remove any uncessary ones
            List<string> keysToRemove = new ();
            foreach (string key in APtiles.Keys)
                if (player.ActionPoints.All(ap => ap.name != key))
                    keysToRemove.Add(key);

            foreach (string key in keysToRemove)
                RemoveTile(APtiles[key].actionPoint);

            // Then cycle through our AP's and add tiles that we need
            foreach (ActionPoint actionPoint in player.ActionPoints)
                if (!APtiles.Keys.Contains(actionPoint.name))
                    AddTile(actionPoint);
        }
    }
}