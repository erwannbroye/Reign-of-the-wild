using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : Interactable
{

	InventoryUI inventoryUI;
	Inventory inventory;
	public GameObject craftingUI;
	public Transform recipesParent;
	public Transform craftParent;
	public GameObject singleRecipePrefab;
	public List<Recipe> recipes = new List<Recipe>();

	void Start()
	{
		inventoryUI = InventoryUI.instance;
		inventory = Inventory.instance;
	}

	public override void Interact()
	{
		if (inventoryUI.GetComponent<CraftingUI>().IsReadyToOpen())
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			inventoryUI.EnterCraftingMenu();
			inventoryUI.ToggleOpenInventoryAvailable();
			inventoryUI.ToggleInfoPanelButtons();
			inventory.isCrafting = true;
			inventory.craftingObject = this;
			buildCraftingUI();
			craftingUI.SetActive(true);
			inventoryUI.GetComponent<CraftingUI>().menuOpened = true;
		}
	}

	void buildCraftingUI()
	{
		foreach (Transform child in recipesParent)
		{
			Destroy(child.gameObject);
		}
		foreach (Recipe recipe in recipes)
		{
			GameObject recipePrefab = Instantiate(singleRecipePrefab, recipesParent);
			recipePrefab.transform.GetChild(0).GetComponent<InventorySlot>().AddItem(recipe.itemOutput);
			recipePrefab.GetComponent<CraftingRecipe>().recipe = recipe;
			int i = 1;
			foreach (Item item in recipe.itemList)
			{
				recipePrefab.transform.GetChild(i).GetComponent<InventorySlot>().AddItem(item);
				i += 1;
			}
		}
	}

	public void recipeClicked(GameObject singleRecipe)
	{
	}
}
