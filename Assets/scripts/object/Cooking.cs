using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : Interactable
{

	public GameObject cookingUI;
	InventoryUI inventoryUI;

	void Start()
	{
		inventoryUI = InventoryUI.instance;
	}

	public override void Interact()
	{
		Debug.Log("oui");
		if (inventoryUI.GetComponent<CookingUI>().IsReadyToOpen())
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			inventoryUI.EnterCraftingMenu();
			inventoryUI.ToggleOpenInventoryAvailable();
			inventoryUI.ToggleInfoPanelButtons();
			cookingUI.SetActive(true);
			inventoryUI.GetComponent<CookingUI>().menuOpened = true;
		}
	}
}
