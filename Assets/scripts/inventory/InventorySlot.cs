using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	public Image icon;
	public ItemLoadingBar LoadBar;

	public Item item;

	public Sprite defaultSlot;
	public Sprite selectedSlot;

	public void AddItem(Item newItem)
	{
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
	}

	public void ClearSlot()
	{
		item = null;

		icon.sprite = null;
		icon.enabled = false;
	}

	IEnumerator UseItemAfterDelay()
	{
		yield return new WaitForSeconds(item.useDelay);
		item.Use();
	}

	IEnumerator UnequipItemAfterDelay()
	{
		yield return new WaitForSeconds(item.useDelay);
		item.Unequip();
	}

	public void UseItem()
	{
		if (item)
		{
			if (item.useDelay > 0)
				LoadBar.FillLoadingBar(item.useDelay);
			StartCoroutine("UseItemAfterDelay");
		}
	}

	public void UnequipItem()
	{
		if (item)
		{
			if (item.useDelay > 0)
				LoadBar.FillLoadingBar(item.useDelay);
			StartCoroutine("UnequipItemAfterDelay");
		}
	}

	public void SelectItem()
	{
		if (item)
		{
			gameObject.GetComponent<Image>().sprite = selectedSlot;
			Inventory.instance.SelectItem(gameObject);
		}
	}

	public void UnselectItem()
	{
		gameObject.GetComponent<Image>().sprite = defaultSlot;
	}
}
