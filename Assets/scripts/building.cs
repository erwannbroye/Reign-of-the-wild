using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : MonoBehaviour
{
    public List<buildingObject> objects = new List<buildingObject>();
    public buildingObject currentObject;
    public GameObject currentGameObject;
    private Vector3 currentPos;
    public Transform currentPreview;
    public Transform cam;
    public RaycastHit hit;
    public LayerMask Layer;

	public GameObject cookingUIRef;
    public float offset = 1.0f;
    public float gridSize = 1.0f;
    public bool isBuilding;

    private void Start() {
        currentObject = objects[0];
        changeCurrentBuilding(0);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void changeCurrentBuilding(int num)
    {
        currentObject = objects[num];
        if (currentPreview.gameObject != null)
            Destroy(currentPreview.gameObject);
        GameObject curprev = Instantiate(currentObject.preview, currentPos, Quaternion.identity) as GameObject;
        currentGameObject = curprev;
        currentPreview = curprev.transform;
        
    }
    private void Update() {
        if (isBuilding)
            startPreview();
        if (Input.GetButtonDown("Fire1") && isBuilding)
            build();
    }

    public void startPreview()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, 10, Layer)) {
            if (hit.transform != this.transform && hit.transform.gameObject.layer == 10 && Vector3.Angle (Vector3.up, hit.normal) < 35.0f) {
                showPreview(hit);
                if (!currentGameObject.activeSelf)
                    currentGameObject.GetComponent<previewObject>().isBuildable = true;
                currentGameObject.SetActive(true);
            }
        } else {
            currentGameObject.SetActive(false);
            currentPreview.GetComponent<previewObject>().isBuildable = false;
        }

    }

    public void showPreview(RaycastHit hitPoint) 
    {
        currentPos = hitPoint.point;
        currentPos -= Vector3.one * offset;
        currentPos /= gridSize;
        currentPos = new Vector3(Mathf.Round(currentPos.x), currentPos.y, Mathf.Round(currentPos.z));
        currentPos *= gridSize;
        currentPos += Vector3.one * offset;
        currentPreview.position = currentPos;
        currentPreview.up = hitPoint.normal;
    }

    public void build()
    {
        previewObject po = currentPreview.GetComponent<previewObject>();
        if (po.isBuildable) {
            GameObject curprev =  Instantiate(currentObject.prefab, currentPos, Quaternion.identity);
			if (currentObject.name == "boneFire")
				curprev.GetComponent<Cooking>().cookingUI = cookingUIRef;
            curprev.transform.up = currentPreview.up;
            curprev.SetActive(true);
        }
        isBuilding = false;
        currentGameObject.SetActive(false);
    }
}

[System.Serializable]
public class buildingObject
{
    public string name;
    public GameObject preview;
    public GameObject prefab;
}