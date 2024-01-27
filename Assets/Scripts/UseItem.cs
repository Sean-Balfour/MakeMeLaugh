using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : Interactables
{

    [SerializeField] private Item itemRequired;

    private void Awake()
    {
        isInteractable = false;
        PlayerController.instance.InventoryChanged.AddListener(CheckPlz);
    }

    void CheckPlz()
    {
        isInteractable = PlayerController.instance.CheckItem(itemRequired);
    }

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

    public override string GetName()
    {
        string name = "Place" + itemRequired.itemName;

        if (!isInteractable) name += "(Missing Item)";

        return name;

    }

}

