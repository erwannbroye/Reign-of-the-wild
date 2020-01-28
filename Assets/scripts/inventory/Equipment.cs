using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
	public EquipmentSlot equipSlot;

	public override void Use()
	{
		base.Use();
		if (EquipmentManager.instance.isEquiped(this))
		{
			EquipmentManager.instance.Unequip(this);
			if (!Inventory.instance.AddItem(this))
				EquipmentManager.instance.Equip(this);
		}
		else
		{
			EquipmentManager.instance.Equip(this);
			Inventory.instance.RemoveItem(this);
		}
	}
}

public enum EquipmentSlot { Head, InnerBody, OuterBody, Legs, Hands, Feet }