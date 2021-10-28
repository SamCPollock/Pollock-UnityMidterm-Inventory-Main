using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // CITATION: Event systems for PointerDown and PointerUp from here: https://www.youtube.com/watch?v=BGr-7GZJNXg
using UnityEngine.UI;

public class ItemBehaviour : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPos;

    public bool droppedOnSlot;

    public Vector2 itemDimensions;

    [SerializeField]
    private Item itemScriptableObject;


    public ItemSlot topLeftSlotOccupied;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        gameObject.GetComponent<Image>().sprite = itemScriptableObject.icon;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        originalPos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        eventData.pointerDrag.GetComponent<ItemBehaviour>().droppedOnSlot = false;
        if (topLeftSlotOccupied != null)
        {
            topLeftSlotOccupied.ItemRemoved(eventData, false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (!droppedOnSlot)
        {
            transform.position = originalPos;
            if (topLeftSlotOccupied != null)
            {
                Debug.Log("FILLIND UP THE SLOTS");
                topLeftSlotOccupied.ItemRemoved(eventData, true);
            }
        }


    }


}
