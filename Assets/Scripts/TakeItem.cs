using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : Interactables
{

    [SerializeField] private Item itemToGive;

    public override void Interact()
    {
        PlayerController.instance.AddItem(itemToGive);
        isInteractable = false;
        this.EndInteract();
    }

    public override string GetName()
    {
        string name = "Take " + itemToGive.itemName;
        if (!isInteractable) name += " (Taken)";
        return name;
    }

}

