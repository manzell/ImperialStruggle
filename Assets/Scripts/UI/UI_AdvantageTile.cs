using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using Sirenix.OdinInspector;

namespace ImperialStruggle
{
    public class UI_AdvantageTile : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Image trim, background, exhaustion;
        [SerializeField] TextMeshProUGUI tileName;
        [SerializeField] AdvantageTile advantageTile;

        private void Awake()
        {
            //throw new System.NotImplementedException(); 
            /*
            foreach(Space space in advantageTile.adjacentSpaces)
                space.updateSpaceEvent += Style; 
            */
        }

        [Button]
        public void Style()
        {
            GraphicSettings settings = FindObjectOfType<Game>().graphicSettings;

            tileName.text = advantageTile.Name;
            background.color = settings.factionColors[advantageTile.faction];
            exhaustion.gameObject.SetActive(advantageTile.tileState == AdvantageTile.AdvantageTileState.Exhaused);
        }

        public async void OnPointerClick(PointerEventData eventData)
        {
            if(Phase.CurrentPhase is ActionRound actionRound)
            {
                await advantageTile.Execute(); 
            }
        }
    }
}