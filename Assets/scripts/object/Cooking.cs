using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : ObjectInteractable
{

	public GameObject cookingUI;
	InventoryUI inventoryUI;

	void Start()
	{
		inventoryUI = InventoryUI.instance;
	}

	public override void Use()
	{
		Cursor.lockState = CursorLockMode.None;
		inventoryUI.EnterCraftingMenu();
		inventoryUI.ToggleOpenInventoryAvailable();
		inventoryUI.ToggleInfoPanelButtons();
		cookingUI.SetActive(true);
		inventoryUI.GetComponent<CookingUI>().menuOpened = true;
	}
}
