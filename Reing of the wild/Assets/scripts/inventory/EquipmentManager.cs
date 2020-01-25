using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
	#region Singleton

	public static EquipmentManager instance;

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

	public Equipment[] currentEquipment;

	void Start()
	{
		int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];
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
}
