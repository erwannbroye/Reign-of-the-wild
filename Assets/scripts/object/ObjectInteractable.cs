using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractable : Interactable
{
	public override void Interact()
	{
		// base.Interact();
		Debug.Log("OUI");
		Use();
	}

	public virtual void Use()
	{
		Debug.Log("Use interactable object");
	}
}
