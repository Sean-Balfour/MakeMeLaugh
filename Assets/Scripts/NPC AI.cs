using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCAI : MonoBehaviour
{
    float timer;

    [SerializeField]
    GameObject[] nodeObjects;

    List<Nodes> nodes = new List<Nodes>();

    Vector3 directionBetweenPoints;
    RaycastHit2D[] hits;
    [SerializeField]
    GameObject nodeToGoTo;
    GameObject currentNode;

    [SerializeField]
    AudioClip[] audioClips = new AudioClip[7];
    AudioClip meow;
    AudioSource audioSource;

    //For detecting player
    Slider detectionBar;

    // Start is called before the first frame update
    void Start()
    {
        currentNode = null;

        //Set a timer for each NPC so theres some realism to their movement
        timer = Random.Range(5.0f, 10.0f);

        //Finding all the nodes
        nodeObjects = GameObject.FindGameObjectsWithTag("Node");

        for (int i = 0; i < nodeObjects.Length; i++)
        {
            nodes.Add(nodeObjects[i].GetComponent<Nodes>());
        }

        audioSource = GetComponent<AudioSource>();
        meow = audioClips[Random.Range(0, 6)];
        audioSource.clip = meow;

        //detectionBar = transform.GetChild(1).GetChild(0).GetComponent<Slider>();
        detectionBar.value = 0;
        detectionBar.gameObject.SetActive(false);
    }

    void Awake()
    {
        detectionBar = this.gameObject.GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        //Debug.Log(timer.ToString());

        if (timer <= 0.0f)
        {
            MoveToNewNode();
            timer = Random.Range(5.0f, 10.0f);

            if (currentNode != null)
            {
                currentNode.GetComponent<Nodes>().setOccupation(false);
            }

            currentNode = nodeToGoTo;
            audioSource.Play();
        }

        if (nodeToGoTo != null)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, nodeToGoTo.transform.position.x, Time.deltaTime * 5.0f), Mathf.Lerp(transform.position.y, nodeToGoTo.transform.position.y, Time.deltaTime));
        }
    }

    void MoveToNewNode()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                //If node is "unoccupied", change it to occupied and move the obejct to it

                if (nodes[i].getOccupation() == false)
                {
                    directionBetweenPoints = (nodes[i].gameObject.transform.position - transform.position).normalized;

                    hits = Physics2D.RaycastAll(transform.position, directionBetweenPoints * 30.0f);
                    Debug.DrawRay(transform.position, directionBetweenPoints * 30.0f, Color.cyan, 5.0f);

                    for (int j = 0; j < hits.Length; j++)
                    {
                        if (hits[j].collider.gameObject.tag == "Node")
                        {
                            nodeToGoTo = nodes[i].gameObject;
                            nodes[i].setOccupation(true);

                            return;
                        }
                    }

                    //Find the difference between current node and planned node
                    //Do the raycast
                    //If colliding with anything other than node, do not go to it

                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PF_Player")
        {
            detectionBar.value = 0.0f;
            detectionBar.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PF_Player")
        {
            if (PlayerController.instance.isInteracting)
            {
                detectionBar.gameObject.SetActive(true);
                detectionBar.value += 0.005f;

                if (detectionBar.value > 1.0f)
                {
                    float rand = Random.value;

                    if (rand < 0.75)
                    {
                        Company.company.DemotePlayer();
                    }
                    else
                    {
                        detectionBar.value = 0.0f;
                        PlayerController.instance.StopInteracting();
                    }
                }
            }
            else
            {
                detectionBar.gameObject.SetActive(false);
            }
        }
    }
}
