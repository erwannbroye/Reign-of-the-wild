using UnityEngine;

public class InventoryUI : MonoBehaviour
{

	Inventory inventory;
	public GameObject itemsParent;
	public GameObject inventoryUI;

	public Hunger hunger;
	public Thirst thirst;
	public Sleep sleep;

	InventorySlot[] slots;

	public GameObject equipmentsItemsParent;
	InventorySlot[] equipmentSlots;

	void Start()
	{
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;

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
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count)
			{
				slots[i].AddItem(inventory.items[i]);
			} else {
				slots[i].ClearSlot();
			}
		}
		int numSlots = inventory.currentEquipment.Length;
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if (inventory.currentEquipment[i] != null)
			{
				equipmentSlots[i].AddItem(inventory.currentEquipment[i]);
			}
			else
			{
				equipmentSlots[i].ClearSlot();
			}
		}
	}
}
