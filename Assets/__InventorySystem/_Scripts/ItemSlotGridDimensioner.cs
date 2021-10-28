using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiates prefabs to fill a grid
[RequireComponent(typeof(GridLayout))]
public class ItemSlotGridDimensioner : MonoBehaviour
{
    [SerializeField]
    GameObject itemSlotPrefab;

    [SerializeField]
    Vector2Int GridDimensions = new Vector2Int(6, 6);



    public List<GameObject> slotsList = new List<GameObject>();

    void Start()
    {
        int numCells = GridDimensions.x * GridDimensions.y;


        // Create ItemSlots
        for (int i = 0; i < numCells; i++)
        {
            GameObject newObject = Instantiate(itemSlotPrefab, this.transform);
            slotsList.Add(newObject);
        }

        AssignSlotNeighbours(numCells);
      
    }

    public void AssignSlotNeighbours(int numberOfCells)
    {
        // assign slots' neighbour references // CITATION: Inspired by a conversation with Chris Tulip about grids.
        for (int i = 0; i < numberOfCells; i++)
        {

            Debug.Log("---INDEX: " + i + "---");
            // Set up neighbours
            if (i - GridDimensions.y >= 0)
            {
                Debug.Log("--SUCCESS: UP--");
                slotsList[i].GetComponent<ItemSlot>().upNeighbour = slotsList[i - GridDimensions.y];
            }
            else // Set topmost row to have no up neighbour 
            {
                Debug.Log("--SUCCESS: NO UP--");
                slotsList[i].GetComponent<ItemSlot>().upNeighbour = null;
            }

            // set down neighbours 
            if (i + GridDimensions.y < GridDimensions.x * GridDimensions.y)
            {

                Debug.Log("--SUCCESS: DOWN--");
                slotsList[i].GetComponent<ItemSlot>().downNeighbour = slotsList[i + GridDimensions.y];
            }
            else
            {
                Debug.Log("--SUCCESS: NO DOWN--");
                slotsList[i].GetComponent<ItemSlot>().downNeighbour = null;
            }



            if ((i + 1) % GridDimensions.y == 0 && i != 0) // Set rightmost column to have no right neighbour
            {
                Debug.Log("--SUCCESS: NO RIGHT--");
                slotsList[i].GetComponent<ItemSlot>().rightNeighbour = null;
            }
            else
            {
                Debug.Log("--SUCCESS: RIGHT--");
                slotsList[i].GetComponent<ItemSlot>().rightNeighbour = slotsList[i + 1];
            }


            if (i % GridDimensions.y == 0) // Set leftmost column to have no left neighbour
            {
                Debug.Log("--SUCCESS: NO LEFT--");
                slotsList[i].GetComponent<ItemSlot>().leftNeighbour = null;
            }
            else
            {
                Debug.Log("--SUCCESS: LEFT--");
                slotsList[i].GetComponent<ItemSlot>().leftNeighbour = slotsList[i - 1];
            }
        }


    }
}
