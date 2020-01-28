using UnityEngine;

public class objectExample : Interactable
{
    public override void Interact()
    {
        // base.Interact();

        Pickup();
    }

    void Pickup()
    {
        Debug.Log("Picking up item");
    }
}