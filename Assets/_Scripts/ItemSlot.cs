using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Display item in the slot, update image, make clickable when there is an item, invisible when there is not
public class ItemSlot : MonoBehaviour, IDropHandler
{
    public Item itemInSlot = null;
    public bool isFull;

    [SerializeField]
    private int itemCount = 0;
    public int ItemCount
    {
        get
        {
            return itemCount;
        }
        set
        {
            itemCount = value;
        }
    }

    [SerializeField]
   // private Image icon;
  //  [SerializeField]
   // private TMPro.TextMeshProUGUI itemCountText;

    void Start()
    {
        RefreshInfo();
    }

    public void UseItemInSlot()
    {
        if(itemInSlot != null)
        {
            itemInSlot.Use();
            if (itemInSlot.isConsumable)
            {
                itemCount--;
                RefreshInfo();
            }
        }
    }

    public void RefreshInfo()
    {
        if(ItemCount < 1)
        {
            itemInSlot = null;
        }

        if(itemInSlot != null) // If an item is present
        {
            //update image and text
            //itemCountText.text = ItemCount.ToString();
            //icon.sprite = itemInSlot.icon;
            //icon.gameObject.SetActive(true);
        } else
        {
            // No item
            //itemCountText.text = "";
            //icon.gameObject.SetActive(false);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("-----------Item dropped in me.-----------");
        if (eventData.pointerDrag != null && !isFull)
        {
            isFull = true;
            Debug.Log("IS FULL SET TO: " + isFull);
            eventData.pointerDrag.GetComponent<ItemBehaviour>().droppedOnSlot = true;
            eventData.pointerDrag.GetComponent<RectTransform>().position = new Vector3 (gameObject.GetComponent<RectTransform>().position.x, gameObject.GetComponent<RectTransform>().position.y, eventData.pointerDrag.GetComponent<RectTransform>().position.z);
            if (eventData.pointerDrag.gameObject.transform.parent.GetComponent<ItemSlot>() != null)
                eventData.pointerDrag.gameObject.transform.parent.GetComponent<ItemSlot>().isFull = false;
            //Debug.Log("Setting the isFull property of " + eventData.pointerDrag.gameObject.transform.parent + "to False");
            eventData.pointerDrag.gameObject.transform.SetParent(gameObject.transform);
        }
    }
}
