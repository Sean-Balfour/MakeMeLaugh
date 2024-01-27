using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeleteGit : Interactables
{
    float duration = 2;
    float timePassed;
    bool deleting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deleting)
        {
            timePassed += Time.deltaTime;
            if (timePassed > 15)
            { 
                Debug.Log("success");
                deleting = false;
            }

        }
    }

    public override void Interact()
    {

    }
}

