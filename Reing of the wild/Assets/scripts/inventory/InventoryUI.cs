using UnityEngine;

public class InventoryUI : MonoBehaviour
{

	Inventory inventory;
	public GameObject itemsParent;

	InventorySlot[] slots;

	void Start()
	{
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;

		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}

	void Update()
	{
		if (Input.GetButtonDown("Inventory"))
		{
			Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.None : CursorLockMode.Locked;
			itemsParent.SetActive(!itemsParent.activeSelf);
		}
	}

	void UpdateUI()
	{
		// Debug.Log("Update UI " + slots.Length + " " + inventory.items.Count);
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count)
			{
				slots[i].AddItem(inventory.items[i]);
			} else {
				slots[i].ClearSlot();
			}
		}
	}
}
