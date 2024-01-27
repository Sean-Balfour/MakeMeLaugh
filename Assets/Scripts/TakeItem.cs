using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : Interactables
{

    [SerializeField] private Item itemToGive;

    public override void Interact()
    {
        PlayerController.instance.AddItem(itemToGive);
    }

}

