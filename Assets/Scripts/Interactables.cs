using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public bool isInteractable = true;

    public virtual void Interact()
    {
        // function called interact . child classes imherit from interact 
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
