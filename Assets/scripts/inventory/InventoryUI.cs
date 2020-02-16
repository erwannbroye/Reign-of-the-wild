using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

	#region Singleton

	public static InventoryUI instance;

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of Inventory found");
			return;
		}
		instance = this;
	}

	#endregion Singleton

	Inventory inventory;
	public GameObject itemsParent;
	public GameObject inventoryUI;

	public Hunger hunger;
	public Thirst thirst;
	public Sleep sleep;

	InventorySlot[] slots;

	public int slotsSize;
	public GameObject slotPrefab;

	public GameObject equipmentsItemsParent;
	InventorySlot[] equipmentSlots;
	ItemType inventoryType;

	public GameObject infoPanel;
	public Text selectedItemName;
	public Text selectedItemDescription;
	public Image selectedItemSprite;
	public Button useButton;
	public bool openAvailable = true;

	public List<Sprite> unselectedIconTab = new List<Sprite>();
	public List<Sprite> selectedIconTab = new List<Sprite>();

	void Start()
	{
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;
		slotsSize = 16;
		inventoryType = ItemType.General;

		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
		equipmentSlots = equipmentsItemsParent.GetComponentsInChildren<InventorySlot>();
	}

	void Update()
	{
		if (Input.GetButtonDown("Inventory") && openAvailable)
		{
			Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.None : CursorLockMode.Locked;
			inventoryUI.SetActive(!inventoryUI.activeSelf);
			hunger.switchPos();
			thirst.switchPos();
			sleep.switchPos();
			inventory.UnselectItem();
		}
	}

	void UpdateUI()
	{
		int j = 0;
		fixSlotNumber();
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count && (inventoryType == ItemType.General || inventory.items[i].type == inventoryType))
			{
				slots[j].AddItem(inventory.items[i]);
				j += 1;
			}
		}
		for (; j < slots.Length; j++)
		{
			slots[j].ClearSlot();
		}
		int numSlots = inventory.currentEquipment.Length;
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if (inventory.currentEquipment[i] != null)
			{
				equipmentSlots[i].AddItem(inventory.currentEquipment[i]);
			} else {
				equipmentSlots[i].ClearSlot();
			}
		}
		UpdateSelectedSlot();
		if (inventory.isCrafting)
			GetComponent<CraftingUI>().UpdateUI(inventory.craftingObject.recipes);
	}

	void UpdateSelectedSlot()
	{
		if (inventory.selectedSlot)
		{
			infoPanel.SetActive(true);
			Item selectedItem = inventory.selectedSlot.GetComponent<InventorySlot>().item;
			selectedItemName.text = selectedItem.name;
			selectedItemDescription.text = selectedItem.description;
			selectedItemSprite.sprite = selectedItem.icon;
			useButton.transform.GetChild(0).GetComponent<Text>().text = (inventory.selectedSlot.name.Substring(0, 4) == "Equi") ? "Unequip" : (selectedItem.type == ItemType.Equipment) ? "Equip" : "Use";
			useButton.onClick.RemoveAllListeners();
			if (inventory.selectedSlot.name.Substring(0, 4) == "Equi")
				useButton.onClick.AddListener(inventory.selectedSlot.GetComponent<InventorySlot>().UnequipItem);
			else
				useButton.onClick.AddListener(inventory.selectedSlot.GetComponent<InventorySlot>().UseItem);
		}
		else
		{
			infoPanel.SetActive(false);
		}
	}

	public void changeInventoryType(int futureType)
	{
		Debug.Log((ItemType)futureType);
		inventoryType = (ItemType)futureType;
		fixSlotNumber();
		inventory.onItemChangedCallback.Invoke();
		inventory.UnselectItem();
	}

	public void unselectIconTab(Image icon)
	{
		icon.sprite = unselectedIconTab[icon.name[0] - 48];
	}

	public void selectIconTab(Image icon)
	{
		icon.sprite = selectedIconTab[icon.name[0] - 48];
	}

	void fixSlotNumber()
	{
		int size = inventory.getItemTypeNumber(inventoryType);

		if (size > slotsSize)
		{
			size += (4 - size % 4);
			for (int i = slotsSize; i < size; i++)
			{
				GameObject newSlot = Instantiate(slotPrefab, itemsParent.transform);
				newSlot.GetComponent<InventorySlot>().LoadBar = GetComponent<ItemLoadingBar>();
				newSlot.GetComponent<InventorySlot>().type = SlotType.Inventory;
			}
			slotsSize = size;
		}
		else if (size < slotsSize)
		{
			size += (size % 4 == 0) ? 0 : (4 - size % 4);
			if (size < 16)
				size = 16;
			if (size != slotsSize)
			{
				for (int i = slotsSize; i > size; i--)
				{
					Destroy(itemsParent.transform.GetChild(i - 1).gameObject);
				}
			}
			slotsSize = size;
		}
	}

	public void EnterCraftingMenu()
	{
		for (int i = 0; i < inventoryUI.transform.childCount; i++)
		{
			if (inventoryUI.transform.GetChild(i).name == "CaseGrid" || inventoryUI.transform.GetChild(i).name == "ItemTypes")
				inventoryUI.transform.GetChild(i).gameObject.SetActive(true);
			else
				inventoryUI.transform.GetChild(i).gameObject.SetActive(false);
		inventoryUI.SetActive(true);
		}
	}

	public void LeaveCraftingMenu()
	{
		for (int i = 0; i < inventoryUI.transform.childCount; i++)
		{
			if (inventoryUI.transform.GetChild(i).name == "CaseGrid" || inventoryUI.transform.GetChild(i).name == "ItemTypes")
				inventoryUI.transform.GetChild(i).gameObject.SetActive(true);
			else
				inventoryUI.transform.GetChild(i).gameObject.SetActive(true);
		}
		inventoryUI.SetActive(false);
	}

	public void ToggleOpenInventoryAvailable()
	{
		openAvailable = !openAvailable;
	}

	public void ToggleInfoPanelButtons()
	{
		for (int i = 0; i < infoPanel.transform.childCount; i++)
		{
			if (infoPanel.transform.GetChild(i).GetComponent<Button>())
				infoPanel.transform.GetChild(i).gameObject.SetActive(!infoPanel.transform.GetChild(i).gameObject.activeSelf);
		}
	}
}
