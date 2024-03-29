using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    float horizontal, vertical;
    [SerializeField]
    float walkSpeed = 2f;
    [SerializeField]
    private float sprintSpeed = 4f;
    private float movingSpeed;
    //float diagonal = 0.1f;
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private float radius;
    [SerializeField] private float force;

    [SerializeField] private InteractionUiController m_InteractionUiController;

    private Animator m_Animator;

    public List<Item> inventory { get; private set; } = new List<Item>();

    public bool isInteracting { get; private set; } = false;

    private bool wasInteractingLastFrame = false;

    public UnityEvent InventoryChanged;

    AudioSource audioSource;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerController found!");
            GameObject.Destroy(this);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(this);

        audioSource = GetComponent<AudioSource>();

    }

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        interact();

    }
    private void FixedUpdate()
    {
        m_Animator.SetBool("IsInteract", false);

        Movement();
        CheckDistance();
    }

    void CheckDistance()
    {
        Interactables[] interactables = FindObjectsByType<Interactables>(FindObjectsSortMode.None);
        for (int i = 0; i < interactables.Length; i++)
        {
            interactables[i].setInRange(false);
        }

        Collider2D[] interactableHighlight = Physics2D.OverlapCircleAll(transform.position, 3, interactableLayerMask);
        if (interactableHighlight.Length > 0)
        {
            foreach (Collider2D item in interactableHighlight)
            {
                item.GetComponent<Interactables>().setInRange(true);
                Debug.Log("Setting in range");
            }
        }
    
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 3);
    }

    void Movement()
    {
        if (isInteracting) return;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        bool sprinting = Input.GetKey(KeyCode.LeftShift);

        if (sprinting)
            movingSpeed = sprintSpeed;
        else
            movingSpeed = walkSpeed;

        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce (new Vector3(horizontal, vertical, 0)*force * movingSpeed * Time.fixedDeltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        if (vertical > 0.1 || vertical < -0.1) m_Animator.SetFloat("X", vertical);
        if (horizontal > 0.1 || horizontal < -0.1) m_Animator.SetFloat("Y", horizontal);

        if (horizontal != 0 && vertical == 0) m_Animator.SetFloat("X", vertical);
        if (vertical != 0 && horizontal == 0) m_Animator.SetFloat("Y", horizontal);

        m_Animator.SetBool("IsWalk", horizontal != 0 || vertical != 0);

        
    }
    void interact()
    {
        if (isInteracting) return;


        if (Input.GetKeyUp("e") && !wasInteractingLastFrame)
        {
            m_Animator.SetBool("IsInteract", true);

            search();
        }

        wasInteractingLastFrame = false;
    }

    void search()
    {
        Collider2D[] interactableList = Physics2D.OverlapCircleAll(transform.position, radius, interactableLayerMask);

        //* TODO
        //* Show UI to select interactable

        m_InteractionUiController.ShowInteractables(interactableList);

        //* TEMP
        //* Select first interactable
        if (interactableList.Length > 0)
        {
            isInteracting = true;
            m_Animator.SetBool("IsWalk", false);
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    public void StopInteracting()
    {
        isInteracting = false;
        m_InteractionUiController.Hide();
        wasInteractingLastFrame = true;
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);

        string inventoryString = "INVENTORY: ";

        foreach (Item i in inventory)
        {
            inventoryString += i.name + ", ";
        }

        InventoryChanged.Invoke();
    }

    public bool CheckItem(Item item)
    {
        return inventory.Contains(item);
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
        InventoryChanged.Invoke();
    }

}