using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

	#region Singleton

	public static Inventory instance;

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

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public List<Item> items = new List<Item>();
	public int size = 16;

	public Equipment[] currentEquipment;
	public GameObject selectedSlot = null;


	void Start()
	{
		int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];
	}

	public bool AddItem(Item item)
	{
		if (items.Count >= size)
		{
			size += 4;
		}
		items.Add(item);

		if (onItemChangedCallback != null)
		{
			onItemChangedCallback.Invoke();
		}

		return (true);
	}

	public void RemoveItem(Item item)
	{
		items.Remove(item);
		if (size > 16 && items.Count % 4 == 0)
		{
			size -= 4;
		}
		if (onItemChangedCallback != null)
		{
			onItemChangedCallback.Invoke();
		}
	}

	public int getItemTypeNumber(ItemType itemType)
	{
		int size = 0;

		for (int i = 0; i < items.Count; i++)
		{
			if (itemType == ItemType.General || items[i].type == itemType)
			{
				size += 1;
			}
		}
		return (size);
	}

	public void Equip(Equipment newItem)
	{
		int slotIndex = (int)newItem.equipSlot;
		currentEquipment[slotIndex] = newItem;
	}

	public void Unequip(Equipment newItem)
	{
		int slotIndex = (int)newItem.equipSlot;
		currentEquipment[slotIndex] = null;
	}

	public bool isEquiped(Equipment item)
	{
		int slotIndex = (int)item.equipSlot;
		if (currentEquipment[slotIndex] == item)
			return (true);
		return (false);
	}

	public bool isSlotAvailable(EquipmentSlot slot)
	{
		int slotIndex = (int)slot;
		if (currentEquipment[slotIndex])
			return (false);
		return (true);
	}

	public void SelectItem(GameObject selectedItemSlot)
	{
		if (selectedSlot)
			UnselectItem();
		selectedSlot = selectedItemSlot;
		if (onItemChangedCallback != null)
		{
			onItemChangedCallback.Invoke();
		}
	}

	public void UnselectItem()
	{
		if (selectedSlot)
			selectedSlot.GetComponent<InventorySlot>().UnselectItem();
		selectedSlot = null;
		if (onItemChangedCallback != null)
		{
			onItemChangedCallback.Invoke();
		}
	}
}
