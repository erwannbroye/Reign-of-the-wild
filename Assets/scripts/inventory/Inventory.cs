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

	public Equipment[] currentEquipment;
	public GameObject selectedSlot = null;

	public bool isCrafting = false;
	public Crafting craftingObject;

	public InventorySlot lastSlotEntered;
	public InventorySlot draggedSlot;
	public GameObject dragImage;


	void Start()
	{
		int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];
		lastSlotEntered = null;
		craftingObject = null;
	}

	void Update()
	{
		if (draggedSlot)
		{
			dragImage.transform.position = Input.mousePosition;
		}
	}

	public bool AddItem(Item item)
	{
		if (items.Count >= size)
		{
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
			size -= 4;
		}
		if (onItemChangedCallback != null)
		{
			onItemChangedCallback.Invoke();
		}
	}

	public int getItemTypeNumber(ItemType itemType)
	{
		int size = 0;

		for (int i = 0; i < items.Count; i++)
		{
			if (itemType == ItemType.General || items[i].type == itemType)
			{
				size += 1;
			}
		}
		return (size);
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

	public void SelectItem(GameObject selectedItemSlot)
	{
		if (selectedSlot && selectedSlot != selectedItemSlot)
			UnselectItem();
		selectedSlot = selectedItemSlot;
		if (onItemChangedCallback != null)
		{
			onItemChangedCallback.Invoke();
		}
	}

	public void UnselectItem()
	{
		if (selectedSlot)
			selectedSlot.GetComponent<InventorySlot>().UnselectItem();
		selectedSlot = null;
		if (onItemChangedCallback != null)
		{
			onItemChangedCallback.Invoke();
		}
	}

	public void StartDragSlot(InventorySlot slot)
	{
		if (slot.item && slot.isDraggable)
		{
			UnselectItem();
			draggedSlot = slot;
			lastSlotEntered = slot;
			slot.icon.enabled = false;
			dragImage.GetComponent<Image>().sprite = slot.icon.sprite;
			dragImage.SetActive(true);
		}
	}

	public void EndDragSlot(InventorySlot slot)
	{
		if (slot.item && slot.isDraggable)
		{
			draggedSlot = null;
			dragImage.SetActive(false);
			if (lastSlotEntered && lastSlotEntered.isDraggable && lastSlotEntered.type != SlotType.CraftOutput)
			{
				if (lastSlotEntered.item)
				{
					SwapSlots(slot, lastSlotEntered);
				}
				else
				{
					lastSlotEntered.AddItem(slot.item);

					if (slot.type == SlotType.Inventory && lastSlotEntered.type != SlotType.Inventory)
						RemoveItem(slot.item);
					else if (slot.type != SlotType.Inventory && lastSlotEntered.type == SlotType.Inventory)
						AddItem(slot.item);
					else if (slot.type == SlotType.CraftOutput && lastSlotEntered && lastSlotEntered.type == SlotType.CraftInput)
					{
						GetComponent<CraftingUI>().ClearAllInputSlots();
						lastSlotEntered.AddItem(slot.item);
						slot.ClearSlot();
					}
					else if (slot.type == SlotType.Inventory && lastSlotEntered.type == SlotType.Inventory)
					{
						for (int i = slot.transform.GetSiblingIndex(); i < items.Count; i++)
						{
							if (i + 1 < items.Count)
								items[i] = items[i + 1];
							else
								items[i] = slot.item;
						}
					}
					slot.ClearSlot();
				}
				if (slot.type == SlotType.CraftOutput && !slot.item && lastSlotEntered.type == SlotType.Inventory)
					GetComponent<CraftingUI>().ClearAllInputSlots();
				if (onItemChangedCallback != null)
				{
					onItemChangedCallback.Invoke();
				}
			} else
			{
				slot.icon.enabled = true;
			}
		}
	}

	void SwapSlots(InventorySlot slot1, InventorySlot slot2)
	{
		if (slot1.type == SlotType.Inventory && slot2.type == SlotType.Inventory)
		{
			items[slot1.transform.GetSiblingIndex()] = slot2.item;
			items[slot2.transform.GetSiblingIndex()] = slot1.item;
		} else if (slot1.type == SlotType.Inventory && slot2.type != SlotType.Inventory)
		{
			items[slot1.transform.GetSiblingIndex()] = slot2.item;
			slot2.AddItem(slot1.item);
		} else if (slot2.type == SlotType.Inventory && slot1.type != SlotType.Inventory)
		{
			items[slot2.transform.GetSiblingIndex()] = slot1.item;
			slot2.AddItem(slot2.item);
		} else
		{
			Item tmp = slot1.item;
			slot1.AddItem(slot2.item);
			slot2.AddItem(tmp);
		}
	}
}
