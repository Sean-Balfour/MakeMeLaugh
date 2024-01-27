using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    float timer;

    List<GameObject> nodeObjects;
    List<Nodes> nodes;

    Vector3 directionBetweenPoints;
    RaycastHit hit;
    bool nodeToGoToFound;
    GameObject nodeToGoTo;

    // Start is called before the first frame update
    void Start()
    {
        //Set a timer for each NPC so theres some realism to their movement
        timer = Random.Range(5.0f, 10.0f);

        //Finding all the nodes
        GameObject.FindGameObjectsWithTag("Node", nodeObjects);

        for(int i = 0; i < nodeObjects.Count; i++)
        {
            nodes[i] = nodeObjects[i].GetComponent<Nodes>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;

        if(timer  <= 0.0f)
        {
            if(nodeToGoTo == null)
            {
                MoveToNewNode();
            }
            else
            {
                Vector3.MoveTowards(transform.position, nodeToGoTo.transform.position, Time.deltaTime * 10.0f);
                timer = Random.Range(5.0f, 10.0f);
            }
        }
    }

    void MoveToNewNode()
    {
        for(int i = 0; i < nodes.Count; i++)
        {
            //If node is "unoccupied", change it to occupied and move the obejct to it

            if (nodes[i].getOccupation() == false)
            {
                directionBetweenPoints = (nodes[i].gameObject.transform.position - transform.position).normalized;

                Physics.Raycast(transform.position, directionBetweenPoints, out hit);

                if(hit.collider.gameObject.tag == "Node")
                {
                    nodeToGoTo = nodes[i].gameObject;
                }

                //Find the difference between current node and planned node
                //Do the raycast
                //If colliding with anything other than node, do not go to it
                
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Nodes>().setOccupation(false);
    }
}
