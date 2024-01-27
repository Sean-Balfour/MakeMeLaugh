using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sendEmail : Interactables
{

    int emailLen = 0;
    float duration = 0;
    float timePassed;
    bool working;


    // Start is called before the first frame update
    void Start()
    {
        //emailLen = UnityEngine.Random.Range(0,3);
        //emailWrite();
    }

    // Update is called once per frame
    void Update()
    {
        if (working)
        {
            timePassed += Time.deltaTime;
            if (timePassed > duration)
            {
                // success in sending email
                Debug.Log("success");
                working = false;
            }

        }
    }


   void emailWrite()
    {
       
        switch(emailLen)
        {
            case 0:
                working = true;
                Debug.Log("short"); //4
                duration = 4f;
                break;

            case 1:
                working = true;
                Debug.Log("med"); //8
                duration = 8f;
                break;

            case 2:
                working = true;
                Debug.Log("long"); //12
                duration = 12f;
                break;
        }
    }

    public override void Interact()
    {
        Debug.Log("meow");
        //emailWrite();
    }
}
