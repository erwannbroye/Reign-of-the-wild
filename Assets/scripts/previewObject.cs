using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class previewObject : MonoBehaviour
{

    public List<Collider> col = new List<Collider>();
    public Material green;
    public Material red;
    public bool isBuildable;

    private void Start() {
        isBuildable = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 10 && other.gameObject.layer != 11)
            isBuildable = false;
        Debug.Log(other.gameObject.name);
        Debug.Log(other.gameObject.layer);
    }
    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.layer != 10 && other.gameObject.layer != 11)
            isBuildable = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 10 && other.gameObject.layer != 11)
            isBuildable = false;
    }

    void FixedUpdate()
    {
        changeColor();
    }

    public void changeColor()
    {

        if (isBuildable) {
            foreach (Transform child in transform)
                child.GetComponent<Renderer>().material = green;
        } else {
            foreach (Transform child in transform)
                child.GetComponent<Renderer>().material = red;
        }

    }
}
