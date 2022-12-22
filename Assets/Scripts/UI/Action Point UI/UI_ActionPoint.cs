using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using System.Linq;

namespace ImperialStruggle
{
    public class UI_ActionPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image apIcon;
        [SerializeField] TextMeshProUGUI apPoints;
        [SerializeField] Image background, highlight;

        IEnumerator popupTimer;
        GameObject popupGameObject;

        public ActionPoint actionPoint;

        public void SetTile(ActionPoint ap)
        {
            actionPoint = ap;
            SetAPIcon(actionPoint);
            setAPValue(actionPoint);
        }

        public void SetAPIcon(ActionPoint ap)
        {
            apIcon.sprite = FindObjectOfType<Game>().graphicSettings.actionIcons[ap.type];
            apIcon.color = ap.tier == ActionPoint.ActionTier.Major ? Color.black : Color.gray;
        }

        public void setAPValue(ActionPoint ap)
        {
            apPoints.text = $"{ap.Value(null)}{(ap.conditionals.Count > 0 ? "*" : string.Empty)}";
            apPoints.color = ap.tier == ActionPoint.ActionTier.Major ? Color.black : Color.gray;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (actionPoint.conditionText != string.Empty)
            {
                popupTimer = OpenPopup(eventData);
                StartCoroutine(popupTimer);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (popupTimer != null)
            {
                StopCoroutine(popupTimer);
                ClosePopup();
            }
        }

        IEnumerator ClosePopup()
        {
            yield return new WaitForSeconds(1f);
            popupGameObject.transform.DOScale(0f, 0.3f);

        }

        IEnumerator OpenPopup(PointerEventData eventData)
        {
            yield return new WaitForSeconds(1f);
            popupGameObject = Instantiate(new GameObject(), transform); // make this a prefab from Graphic Settings? 
            popupGameObject.transform.position = eventData.position;
            TextMeshProUGUI tm = popupGameObject.AddComponent<TextMeshProUGUI>();
            tm.text = actionPoint.conditionText;
        }
    }
}