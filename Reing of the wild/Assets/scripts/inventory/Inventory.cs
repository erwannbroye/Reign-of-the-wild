using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public int size = 18;
 
	public bool AddItem(Item item)
	{
		if (items.Count >= size)
		{
			Debug.Log("Not enough space");
			return (false);
		}
		items.Add(item);

		if (onItemChangedCallback != null)
		{
			Debug.Log("oui");
			onItemChangedCallback.Invoke();
		}

		return (true);
	}

	public void RemoveItem(Item item)
	{
		items.Remove(item);
	}
}
