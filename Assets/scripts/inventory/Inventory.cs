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

	public RectTransform inventoryParent;
	public GameObject slotPrefab;

	public Equipment[] currentEquipment;

	void Start()
	{
		int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];
	}

	public bool AddItem(Item item)
	{
		if (items.Count >= size)
		{
			for (int i = 0; i < 4; i++)
				Instantiate(slotPrefab, inventoryParent);
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
			Destroy(inventoryParent.GetChild(size - 1).gameObject);
			Destroy(inventoryParent.GetChild(size - 2).gameObject);
			Destroy(inventoryParent.GetChild(size - 3).gameObject);
			Destroy(inventoryParent.GetChild(size - 4).gameObject);
			size -= 4;
		}
		if (onItemChangedCallback != null)
		{
			onItemChangedCallback.Invoke();
		}
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
}
