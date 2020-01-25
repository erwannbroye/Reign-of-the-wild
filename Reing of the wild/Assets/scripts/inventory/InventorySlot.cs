using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	public Image icon;
	public ItemLoadingBar LoadBar;

	Item item;

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

	public void UseItem()
	{
		if (item)
		{
			if (item.useDelay > 0)
				LoadBar.StartCoroutine("FillLoadingBar" , item.useDelay);
			StartCoroutine("UseItemAfterDelay");
		}
	}
}
