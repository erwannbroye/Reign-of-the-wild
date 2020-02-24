using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class quickMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public List<MenuButton> buttons = new List<MenuButton>();
    private Vector2 mousePosition;
    private Vector2 fromVector2M = new Vector2(0.5f, 1.0f);
    private Vector2 centerCircle = new Vector2(0.5f,0.5f);
    private Vector2 toVector2M;
    public building buildSys;
    public GameObject quickMenuRef;

    public int menuItems;
    public int CurrentItem;
    public int oldMenuItem;
    public equipementSwitch equipement;

    void Start()
    {
        menuItems = buttons.Count;
        foreach (MenuButton button in buttons)
        {
            button.sceneImage.color = button.NormalColor;
        }
        CurrentItem = 0;
        oldMenuItem = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if (quickMenuRef.activeSelf)
			GetCurrentMenuItem();
		if (Input.GetButtonDown("Fire1") && quickMenuRef.gameObject.activeSelf)
			ButtonAction();
		if (Input.GetButtonDown("quickMenu"))
		{
			quickMenuRef.gameObject.SetActive(true);
			Cursor.lockState = CursorLockMode.None;
			buildSys.isBuilding = false;
			buildSys.currentGameObject.SetActive(false);

		}
		else if (Input.GetButtonUp("quickMenu"))
		{
			quickMenuRef.gameObject.SetActive(false);
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

    public void GetCurrentMenuItem()
    {
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        toVector2M = new Vector2 (mousePosition.x / Screen.width, mousePosition.y / Screen.height);
        float angle = (Mathf.Atan2(fromVector2M.y - centerCircle.y, fromVector2M.x-centerCircle.x) - Mathf.Atan2(toVector2M.y - centerCircle.y, toVector2M.x-centerCircle.x)) * Mathf.Rad2Deg;

        if (angle < 0)
            angle += 360;


        CurrentItem = (int)(angle / (360 / menuItems));

        if (oldMenuItem > 9 || oldMenuItem < 0)
            oldMenuItem = 0;
        if (CurrentItem > 9 || oldMenuItem < 0)
            oldMenuItem = 0;
        if (CurrentItem != oldMenuItem)
        {
            buttons[oldMenuItem].sceneImage.color = buttons[oldMenuItem].NormalColor;
            oldMenuItem = CurrentItem;
            buttons[CurrentItem].sceneImage.color = buttons[CurrentItem].HighlightedColor;
        }
    }

    public void ButtonAction()
    {
        buttons[CurrentItem].sceneImage.color = buttons[CurrentItem].PressedColor;
        if (CurrentItem == 0 || CurrentItem == 1) {
            buildSys.changeCurrentBuilding(CurrentItem);
            buildSys.isBuilding = true;
        }
        if (CurrentItem == 2)
        {
            if (equipement.selected == 1)
                equipement.selected = 0 ;
            else
                equipement.selected = 1;
        }
        if (CurrentItem == 3)
        {
            if (equipement.selected == 2)
                equipement.selected = 0 ;
            else
            equipement.selected = 2;
        }
        quickMenuRef.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; 
    }
}

[System.Serializable]
public class MenuButton 
{
    public string name;
    public Image sceneImage;
    public Color NormalColor = Color.white;
    public Color HighlightedColor = Color.grey;
    public Color PressedColor = Color.grey;
}
