using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackmail : Interactables
{

    int chance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void blackmail()
    {
        chance = UnityEngine.Random.Range(0, 11);
        if (chance > 5)
        {
            //successful
        } else
        {
            //not successful 
        }
    }

    public override void Interact()
    {
        
    }
}
