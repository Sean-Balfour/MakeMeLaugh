using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Interactables : MonoBehaviour
{
    public bool isInteractable = true;
    public bool isInRange = false;

    [SerializeField] private Sprite outline;
    [SerializeField] private Sprite baseSkin;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public virtual void Interact()
    {
        
    }
    //public void Awake()
    //{
        
    //}

    public virtual void EndInteract()
    {
        PlayerController.instance.StopInteracting();
    }

    public virtual string GetName()
    {
        return gameObject.name;
    }
    
    public void SkinChange()
    {
        if (this.isInRange)
        {
            Debug.Log("a");
            spriteRenderer.sprite = outline;
        }
        else
        {
            Debug.Log("b");
            spriteRenderer.sprite = baseSkin;
        }
    }

    public void setInRange(bool inRange)
    {
        isInRange = inRange;
        this.SkinChange();
    }

    protected virtual void Update()
    {
        Player player = FindFirstObjectByType<Player>();

        Debug.Log((player.transform.position - transform.position).magnitude);

        if ((player.transform.position - transform.position).magnitude > 6)
        {

            setInRange(false);
        }
    }

}
