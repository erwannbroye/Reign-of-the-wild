using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
	new public string name = "New Item";
	public Sprite icon = null;
	public float useDelay = 0f;
	public ItemType type;

	public virtual void Use()
	{
	}

	public virtual void Unequip()
	{
	}
}

public enum ItemType { General, Heal, Equipment, Food, Tools, Craft }