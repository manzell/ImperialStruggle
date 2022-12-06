using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector; 

public class UI_GameBoard : SerializedMonoBehaviour, IDragHandler, IBeginDragHandler, IPointerClickHandler
{
    public static UnityEvent<PointerEventData> clickEvent = new UnityEvent<PointerEventData>();
    [SerializeField] GameObject mapContainer, lineContainer, linePrefab;
    [SerializeField] float speed = 0.1f; 
    Vector2 previousPosition;

    [SerializeField] public Dictionary<(Space, Space), LineRenderer> connections = new Dictionary<(Space, Space), LineRenderer>(); 

    public void OnPointerClick(PointerEventData eventData) => clickEvent.Invoke(eventData);
    public void OnBeginDrag(PointerEventData eventData) => previousPosition = Mouse.current.position.ReadValue(); 

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 frameSize = mapContainer.GetComponent<RectTransform>().sizeDelta;
        Vector2 boardSize = GetComponent<RectTransform>().sizeDelta;
        Vector3 drag = (Mouse.current.position.ReadValue() - previousPosition) * speed;

        Vector3 newPosition = transform.position + drag;

        bool maxXrespected = (newPosition.x + boardSize.x / 2) > (mapContainer.transform.position.x + frameSize.x / 2);
        bool maxYrespected = (newPosition.y + boardSize.y / 2) > (mapContainer.transform.position.y + frameSize.y / 2);
        bool minXrespected = (newPosition.x - boardSize.x / 2) < (mapContainer.transform.position.x - frameSize.x / 2);
        bool minYrespected = (newPosition.y - boardSize.y / 2) < (mapContainer.transform.position.y - frameSize.y / 2);

        if (maxXrespected && maxYrespected && minXrespected && minYrespected)
        {
            transform.position += drag;
            previousPosition = Mouse.current.position.ReadValue();
        }
    }

    [Button] 
    void DrawLines()
    {
        foreach(Space source in FindObjectsOfType<Space>())
        {
            foreach(Space target in source.adjacentSpaces)
            {
                if (!connections.ContainsKey((source, target)) && !connections.ContainsKey((target, source)))
                {
                    GameObject go = Instantiate(linePrefab, lineContainer.transform); 
                    go.name = source.name + "/" + target.name; 

                    LineRenderer line = go.GetComponent<LineRenderer>(); 
                    line.SetPositions(new Vector3[] { source.gameObject.transform.position, target.gameObject.transform.position});

                    connections.Add((source, target), line);
                }
            }
        }
    }
}
