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

    public GameObject upNeighbour;
    public GameObject downNeighbour;

    public GameObject rightNeighbour;
    public GameObject leftNeighbour;

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
        if (eventData.pointerDrag != null) // see if an item is dragged
        {
            bool canDrop = true;
            ItemSlot itemSlotBeingLookedAt = gameObject.GetComponent<ItemSlot>(); 

            if (!isFull)                   // see if the mouseOver slot is full
            {
                for (int i = 1; i < eventData.pointerDrag.GetComponent<ItemBehaviour>().itemDimensions.x; i++) // iterate through x Size
                {
                    if (itemSlotBeingLookedAt.rightNeighbour == null)
                    {
                        canDrop = false;
                        return;
                    }
                    
                    itemSlotBeingLookedAt = itemSlotBeingLookedAt.rightNeighbour.GetComponent<ItemSlot>();
                    if (itemSlotBeingLookedAt.isFull || itemSlotBeingLookedAt == null)
                    {
                        // don't drop
                        canDrop = false;
                        return;
                    }
                    
                    itemSlotBeingLookedAt = gameObject.GetComponent<ItemSlot>();    // Return looker to original position


                    for (int j = 1; j < eventData.pointerDrag.GetComponent<ItemBehaviour>().itemDimensions.y; j++) // iterate through y size for additional width
                    {
                        if (itemSlotBeingLookedAt.downNeighbour == null)
                        {
                            canDrop = false;
                            return;
                        }
                        itemSlotBeingLookedAt = itemSlotBeingLookedAt.downNeighbour.GetComponent<ItemSlot>();

                        if (itemSlotBeingLookedAt.isFull)
                        {
                            // don't drop
                            canDrop = false;
                            return;
                        }
                    }
                }
                itemSlotBeingLookedAt = gameObject.GetComponent<ItemSlot>();    // Return looker to original position

                for (int i = 1; i < eventData.pointerDrag.GetComponent<ItemBehaviour>().itemDimensions.y; i++) // iterate through y size
                {
                    if (itemSlotBeingLookedAt.downNeighbour== null)
                    {
                        canDrop = false;
                        return;
                    }

                    itemSlotBeingLookedAt = itemSlotBeingLookedAt.downNeighbour.GetComponent<ItemSlot>();
                    if (itemSlotBeingLookedAt.isFull)
                    {
                        canDrop = false;
                        return;
                    }
                }
                itemSlotBeingLookedAt = gameObject.GetComponent<ItemSlot>();    // Return looker to original position


                if (canDrop)
                {
                    // set filled slots to full
                    isFull = true;

                    for (int i = 1; i < eventData.pointerDrag.GetComponent<ItemBehaviour>().itemDimensions.x; i++) // iterate through x Size
                    {
                        rightNeighbour.GetComponent<ItemSlot>().isFull = true;

                        for (int j = 1; j < eventData.pointerDrag.GetComponent<ItemBehaviour>().itemDimensions.y; j++) // iterate through y size for additional width
                        {
                            rightNeighbour.GetComponent<ItemSlot>().downNeighbour.GetComponent<ItemSlot>().isFull = true;
                        }
                    }
                    for (int i = 1; i < eventData.pointerDrag.GetComponent<ItemBehaviour>().itemDimensions.y; i++) // iterate through y size
                    {
                        downNeighbour.GetComponent<ItemSlot>().isFull = true;
                    }


                    eventData.pointerDrag.GetComponent<ItemBehaviour>().droppedOnSlot = true;
                    eventData.pointerDrag.GetComponent<ItemBehaviour>().topLeftSlotOccupied = gameObject.GetComponent<ItemSlot>();
                    eventData.pointerDrag.GetComponent<RectTransform>().position = new Vector3(gameObject.GetComponent<RectTransform>().position.x, gameObject.GetComponent<RectTransform>().position.y, eventData.pointerDrag.GetComponent<RectTransform>().position.z);

                    eventData.pointerDrag.gameObject.transform.SetParent(gameObject.transform.parent.parent);
                }
                else
                {
                    Debug.Log("----------- CANNOT DROP HERE ---------------");
                }
            }
        }
    }

    public void ItemRemoved(PointerEventData eventData, bool filled)
    {
        isFull = filled;

        for (int i = 1; i < eventData.pointerDrag.GetComponent<ItemBehaviour>().itemDimensions.x; i++) // iterate through x Size
        {
            rightNeighbour.GetComponent<ItemSlot>().isFull = filled;

            for (int j = 1; j < eventData.pointerDrag.GetComponent<ItemBehaviour>().itemDimensions.y; j++) // iterate through y size for additional width
            {
                rightNeighbour.GetComponent<ItemSlot>().downNeighbour.GetComponent<ItemSlot>().isFull = filled;
            }
        }
        for (int i = 1; i < eventData.pointerDrag.GetComponent<ItemBehaviour>().itemDimensions.y; i++) // iterate through y size
        {
            downNeighbour.GetComponent<ItemSlot>().isFull = filled;
        }
    }

}
