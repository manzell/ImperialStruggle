using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using System.Linq; 

public class UI_ActionPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image apIcon;
    [SerializeField] TextMeshProUGUI apPoints;
    [SerializeField] Image background, highlight; 

    IEnumerator popupTimer;
    GameObject popupGameObject;
    GraphicSettings graphicSettings;

    ActionPoint.ActionPointKey key; 

    void Awake()
    {
        graphicSettings = FindObjectOfType<Game>().graphicSettings;
    }

    public void SetDisplay(ActionPoint.ActionPointKey key, int points)
    {
        this.key = key; 
        setAPValue(key, points);
        SetAPIcon(key);         
    }

    public void SetAPIcon(ActionPoint.ActionPointKey key)
    {
        apIcon.sprite = graphicSettings.actionIcons[key.actionType];
        apIcon.color = key.actionTier == ActionPoint.ActionTier.Major ? Color.black : Color.gray;
    }

    public void setAPValue(ActionPoint.ActionPointKey key, int val)
    {
        apPoints.text = val.ToString();
        apPoints.color = key.actionTier == ActionPoint.ActionTier.Major ? Color.black : Color.gray;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(key.condition != string.Empty)
        {
            popupTimer = OpenPopup(eventData);
            StartCoroutine(popupTimer);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(popupTimer != null)
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
        tm.text = key.condition; 
    }
}
