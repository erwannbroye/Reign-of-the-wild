using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

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
		if (Input.GetButtonDown("Inventory"))
		{
			Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.None : CursorLockMode.Locked;
			inventoryUI.SetActive(!inventoryUI.activeSelf);
			hunger.switchPos();
			thirst.switchPos();
			sleep.switchPos();
		}
	}

	void UpdateUI()
	{
		// Debug.Log("Update UI " + slots.Length + " " + inventory.items.Count);
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
	}

	public void changeInventoryType(int futureType)
	{
		Debug.Log((ItemType)futureType);
		inventoryType = (ItemType)futureType;
		fixSlotNumber();
		inventory.onItemChangedCallback.Invoke();
	}

	public void decreaseIconAlpha(Image icon)
	{
		icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0f);
	}

	public void increaseIconAlpha(Image icon)
	{
		icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 1f);
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
			}
			slotsSize = size;
		}
		else if (size >= 16 && size < slotsSize)
		{
			size += (size % 4 == 0) ? 0 : (4 - size % 4);
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
}
