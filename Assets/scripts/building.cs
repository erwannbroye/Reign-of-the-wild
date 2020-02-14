using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : MonoBehaviour
{
    public List<buildingObject> objects = new List<buildingObject>();
    public buildingObject currentObject;
    private Vector3 currentPos;
    public Transform currentPreview;
    public Transform cam;
    public RaycastHit hit;
    public LayerMask Layer;

    public float offset = 1.0f;
    public float gridSize = 1.0f;
    public bool isBuilding;

    private void Start() {
        currentObject = objects[0];
        changeCurrentBuilding();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void changeCurrentBuilding()
    {
        GameObject curprev = Instantiate(currentObject.preview, currentPos, Quaternion.identity) as GameObject;
        currentPreview = curprev.transform;
    }
    private void Update() {
        if (isBuilding)
            startPreview();
    }

    public void startPreview()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, 10, Layer)) {
            if (hit.transform != this.transform) {
                showPreview(hit);
            }
        }
    }

    public void showPreview(RaycastHit hitPoint) 
    {
        currentPos = hitPoint.point;
        currentPos -= Vector3.one * offset;
        currentPos /= gridSize;
        currentPos = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));
        currentPos *= gridSize;
        currentPos += Vector3.one * offset;
        currentPreview.position = currentPos;
        currentPreview.up = hitPoint.normal;
    }
}

[System.Serializable]
public class buildingObject
{
    public string name;
    public GameObject preview;
}