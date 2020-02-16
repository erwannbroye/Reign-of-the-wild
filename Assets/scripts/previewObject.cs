using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class previewObject : MonoBehaviour
{

    public List<Collider> col = new List<Collider>();
    public Material green;
    public Material red;
    public bool isBuildable;

    void OnTriggerEnter(Collider other)
    {
        isBuildable = false;
    }
    void OnTriggerExit(Collider other) 
    {
        isBuildable = true;
    }

    void Update()
    {
        changeColor();
    }

    public void changeColor()
    {
        // if (col.Count == 0)
        //     isBuildable = true;
        // else
        //     isBuildable = false;
        if (isBuildable) {
            foreach (Transform child in transform)
                child.GetComponent<Renderer>().material = green;
        } else {
            foreach (Transform child in transform)
                child.GetComponent<Renderer>().material = red;
        }
    }
}
