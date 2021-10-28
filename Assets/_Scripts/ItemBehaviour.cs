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
        //transform.GetChild(1).GetComponent<Image>().sprite = itemScriptableObject.icon;
        gameObject.GetComponent<Image>().sprite = itemScriptableObject.icon;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("--Pointer down--");
        originalPos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      //  Debug.Log("Begin Drag...");
        canvasGroup.blocksRaycasts = false;
        eventData.pointerDrag.GetComponent<ItemBehaviour>().droppedOnSlot = false;
        if (topLeftSlotOccupied != null)
        {
            topLeftSlotOccupied.ItemRemoved(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
     //   Debug.Log("...Dragging...");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
     //   Debug.Log("...End Drag.");
        canvasGroup.blocksRaycasts = true;

        if (!droppedOnSlot)
        {
            transform.position = originalPos;
        }


    }

    // private bool isHeldByMouse = false;


    void Update()
    {
        //if (isHeldByMouse)
        //{
        //    // CITATION: Reading this thread for ideas on how to convert mouse position to canvas objects  https://forum.unity.com/threads/mouse-position-for-screen-space-camera.294458/

        //    var screenPointxy = Camera.main.WorldToScreenPoint(Input.mousePosition);

        //    Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //    gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPointxy);
            

        //    //gameObject.transform.position = Input.mousePosition;

        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        isHeldByMouse = false;
        //    }
        //}
    }


    public void ClickedOnItem()
    {
        //Debug.Log("Clicked on an Item");
        //isHeldByMouse = true;
    }
}
