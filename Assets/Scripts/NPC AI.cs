using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    float timer;

    [SerializeField]
    List<GameObject> nodeObjects;
    
    List<Nodes> nodes = new List<Nodes>();

    Vector3 directionBetweenPoints;
    RaycastHit2D hit;
    [SerializeField]
    GameObject nodeToGoTo;
    GameObject currentNode;

    [SerializeField]
    AudioClip[] audioClips = new AudioClip[7];
    AudioClip meow;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        currentNode = null;

        //Set a timer for each NPC so theres some realism to their movement
        timer = Random.Range(5.0f, 10.0f);

        //Finding all the nodes
        GameObject.FindGameObjectsWithTag("Node", nodeObjects);

        for(int i = 0; i < nodeObjects.Count; i++)
        {
            nodes.Add(nodeObjects[i].GetComponent<Nodes>());
        }

        audioSource = GetComponent<AudioSource>();
        meow = audioClips[Random.Range(0, 6)];
        audioSource.clip = meow;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        //Debug.Log(timer.ToString());

        if(timer  <= 0.0f)
        {
            MoveToNewNode();
            timer = Random.Range(5.0f, 10.0f);

            if(currentNode != null)
            {
                currentNode.GetComponent<Nodes>().setOccupation(false); 
            }

            currentNode = nodeToGoTo;
            audioSource.Play();
        }

        if (nodeToGoTo != null)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, nodeToGoTo.transform.position.x, Time.deltaTime * 5.0f), Mathf.Lerp(transform.position.y, nodeToGoTo.transform.position.y, Time.deltaTime * 5.0f));
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

                hit = Physics2D.Raycast(transform.position, directionBetweenPoints * 30.0f);
                Debug.DrawRay(transform.position, directionBetweenPoints * 30.0f, Color.cyan, 5.0f);

                if(hit.collider.gameObject.tag == "Node")
                {
                    nodeToGoTo = nodes[i].gameObject;
                    nodes[i].setOccupation(true);

                    return;
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
