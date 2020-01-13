﻿using UnityEngine;

public class objectExample : Interactable
{

	public Item item;

	public override void Interact()
    {
        // base.Interact();

        Pickup();
    }

    void Pickup()
    {
        Debug.Log("Picking up item");
		Inventory.instance.AddItem(item);
		Destroy(gameObject);
    }
}