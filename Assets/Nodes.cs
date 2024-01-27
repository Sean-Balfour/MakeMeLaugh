using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    bool occupied;

    // Start is called before the first frame update
    void Start()
    {
        occupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setOccupation(bool tru)
    {
        occupied = tru;
    }

    public bool getOccupation()
    {
        return occupied;
    }
}
