using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
	new public string name = "New Item";
	public string description = "This is a description";
	public Sprite icon = null;
	public float useDelay = 0f;
	public ItemType type;

	public virtual void Use()
	{
		Inventory.instance.UnselectItem();
		Inventory.instance.RemoveItem(this);
	}

	public virtual void Destroy()
	{
		Inventory.instance.RemoveItem(this);
	}

	public virtual void Unequip()
	{
		Inventory.instance.UnselectItem();
	}
}

public enum ItemType { General, Heal, Equipment, Food, Tools, Craft }