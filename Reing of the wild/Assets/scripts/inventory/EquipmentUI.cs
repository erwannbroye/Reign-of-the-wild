using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
	Inventory inventory;
	EquipmentManager equipmentManager;

	public GameObject itemsParent;

	InventorySlot[] slots;

	void Start()
	{
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateEquipmentUI;
		equipmentManager = EquipmentManager.instance;

		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}

	void Update()
	{
	}

	void UpdateEquipmentUI()
	{
		// Debug.Log("Update EquipmentUI " + slots.Length + " " + equipmentManager.currentEquipment.Length);
		int numSlots = equipmentManager.currentEquipment.Length;
		for (int i = 0; i < slots.Length; i++)
		{
			if (equipmentManager.currentEquipment[i] != null)
			{
				slots[i].AddItem(equipmentManager.currentEquipment[i]);
			}
			else
			{
				slots[i].ClearSlot();
			}
		}
	}
}
