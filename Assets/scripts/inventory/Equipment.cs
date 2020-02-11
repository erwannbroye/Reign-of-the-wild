using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
	public EquipmentSlot equipSlot;

	void Start()
	{
	}

	public override void Unequip()
	{
		base.Unequip();
		Inventory.instance.Unequip(this);
		if (!Inventory.instance.AddItem(this))
			Inventory.instance.Equip(this);
	}

	public override void Use()
	{
		Inventory.instance.UnselectItem();
		if (Inventory.instance.isSlotAvailable(this.equipSlot))
		{
			Inventory.instance.Equip(this);
			Inventory.instance.RemoveItem(this);
		} else
		{
			Item tmpItem = Inventory.instance.currentEquipment[(int)this.equipSlot];
			Inventory.instance.Equip(this);
			Inventory.instance.RemoveItem(this);
			Inventory.instance.AddItem(tmpItem);
		}
	}
}

public enum EquipmentSlot { Head, InnerBody, OuterBody, Legs, Hands, Feet }