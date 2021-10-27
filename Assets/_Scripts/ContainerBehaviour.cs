using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerBehaviour : MonoBehaviour
{

    public GameObject containerCanvas;

    public GameObject containerCanvasPrefab;
    public GameObject canvasInScene;

    // Start is called before the first frame update
    void Start()
    {
        canvasInScene = GameObject.Find("Canvas");
        containerCanvas = Instantiate(containerCanvasPrefab);
        containerCanvas.transform.parent = canvasInScene.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
