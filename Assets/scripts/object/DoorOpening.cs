using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : Interactable
{
    // Start is called before the first frame update
    public Animator anim;
    bool isOpen;
    private void Start()
    {
        isOpen = false;
    }
	public override void Interact()
    {
        isOpen = !isOpen;
        anim.SetBool("isOpen", isOpen);
    }
}
