using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //[SerializeField]
    //GameObject containerCanvas;

    [SerializeField]
    ItemTable itemTable;

    private void Start()
    {
        itemTable.AssignItemIDs();
    }

    public void OpenContainer(GameObject containerToOpen)
    {
        containerToOpen.SetActive(true);
    }

    public void CloseContainer(GameObject containerToClose)
    {
        containerToClose.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       // if (collision.gameObject.tag == "Container")
        {
            OpenContainer(collision.gameObject.transform.parent.GetComponent<ContainerBehaviour>().containerCanvas);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
      //  if (collision.gameObject.tag == "Container")
        {
            CloseContainer(collision.gameObject.transform.parent.GetComponent<ContainerBehaviour>().containerCanvas);
        }
    }
}
