using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingUI : MonoBehaviour
{
	public InventorySlot inputSlot;
	public InventorySlot outputSlot;
	public Slider cookingBar;
	public GameObject cookingUI;
	public bool menuOpened;

	bool isCooking = false;
	float cookingTime;
	Item cookingOutput;

	void Start()
	{
		isCooking = false;
		cookingTime = 0f;
		menuOpened = false;
	}

	void Update()
	{
		if (isCooking)
		{
			if (cookingBar.value >= 1f)
			{
				cookingBar.value = 0f;
				isCooking = false;
				inputSlot.ClearSlot();
				outputSlot.AddItem(cookingOutput);
				cookingTime = 0f;
				cookingOutput = null;
				return;
			}
			cookingBar.value += (Time.deltaTime / cookingTime);
		}
		if (menuOpened)
		{
			if (Input.GetButtonDown("Interact"))
			{
				Cursor.lockState = CursorLockMode.Locked;
				menuOpened = false;
				cookingUI.SetActive(false);
				InventoryUI.instance.LeaveCraftingMenu();
				InventoryUI.instance.ToggleOpenInventoryAvailable();
				InventoryUI.instance.ToggleInfoPanelButtons();
			}
		}
	}

	public void StartCooking()
	{
		Item cookingItem = inputSlot.item;
		if (cookingItem && cookingItem.cookable)
		{
			cookingTime = cookingItem.cookTime;
			cookingOutput = cookingItem.cookOutput;
			isCooking = true;
		}
	}

	public void Close()
	{

	}
}
