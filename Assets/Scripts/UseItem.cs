using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : Interactables
{

    [SerializeField] private Item itemRequired;

    public override void Interact()
    {
        if (PlayerController.instance.CheckItem(itemRequired))
        {
            PlayerController.instance.RemoveItem(itemRequired);
            Debug.Log("Item used");
        }
        else
        {
            Debug.Log("Item not found");
        }
    }

}

