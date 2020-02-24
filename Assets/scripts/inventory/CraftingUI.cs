using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
	public Transform craftParent;
	List<Recipe> recipeList = new List<Recipe>();
	List<Item> inputItems = new List<Item>();
	public GameObject craftingUI;

	public bool menuOpened;
	public float openCooldownTime = 0f;
	float openCooldown;
	bool isCrafting;

	void Start()
	{
		menuOpened = false;
		isCrafting = false;
	}

	void Update()
	{
		if (openCooldown > 0)
			openCooldown -= Time.deltaTime;
		if (menuOpened)
		{
			if (Input.GetButtonDown("Interact"))
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				craftingUI.SetActive(false);
				InventoryUI.instance.LeaveCraftingMenu();
				InventoryUI.instance.ToggleOpenInventoryAvailable();
				InventoryUI.instance.ToggleInfoPanelButtons();
				openCooldown = openCooldownTime;
				menuOpened = false;
			}
		}
	}

	public void UpdateUI(List<Recipe> recipes)
	{
		recipeList = recipes;
		inputItems.Clear();
		for (int i = 1; i < craftParent.childCount; i++)
		{
			if (craftParent.GetChild(i).GetComponent<InventorySlot>().item)
				inputItems.Add(craftParent.GetChild(i).GetComponent<InventorySlot>().item);
		}
		if (inputItems.Count > 0)
		{
			Item outputResult = IsItARecipe();
			if (outputResult)
				craftParent.GetChild(0).GetComponent<InventorySlot>().AddItem(outputResult);
			else
				craftParent.GetChild(0).GetComponent<InventorySlot>().ClearSlot();
		}
	}

	public void ClearAllInputSlots()
	{
		for (int i = 1; i < craftParent.childCount; i++)
		{
			if (craftParent.GetChild(i).GetComponent<InventorySlot>().item)
				craftParent.GetChild(i).GetComponent<InventorySlot>().ClearSlot();
		}
	}

	Item IsItARecipe()
	{
		foreach (Recipe recipe in recipeList)
		{
			recipe.itemList.Sort(SortByName);
			inputItems.Sort(SortByName);
			if (CompareRecipeWithInput(recipe))
			{
				return (recipe.itemOutput);
			}
		}
		return (null);
	}

	bool CompareRecipeWithInput(Recipe recipe)
	{
		if (recipe.itemList.Count != inputItems.Count)
			return (false);
		for (int i = 0; i < recipe.itemList.Count; i++)
		{
			if (recipe.itemList[i].name != inputItems[i].name)
				return (false);
		}
		return (true);
	}

	static int SortByName(Item p1, Item p2)
	{
		return p1.name.CompareTo(p2.name);
	}

	public bool IsReadyToOpen()
	{
		return ((openCooldown <= 0) ? true : false);
	}
}
