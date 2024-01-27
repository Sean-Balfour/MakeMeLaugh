using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public bool isInteractable = true;

    public virtual void Interact()
    {
        
    }

    public virtual void EndInteract()
    {
        PlayerController.instance.StopInteracting();
    }

    public virtual string GetName()
    {
        return gameObject.name;
    }


}
