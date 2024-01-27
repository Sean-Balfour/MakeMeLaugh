using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    float horizontal, vertical;
    float movementSpeed = 2f;
    //float diagonal = 0.1f;
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private float radius;
    [SerializeField] private float force;

    private Animator m_Animator;

    private List<Item> inventory = new List<Item>();


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
    }

    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(horizontal, vertical, 0) * 1000 * movementSpeed * Time.fixedDeltaTime);

        if (vertical != 0) m_Animator.SetFloat("X", vertical);
        if (horizontal != 0) m_Animator.SetFloat("Y", horizontal);

        m_Animator.SetBool("IsWalk", horizontal != 0 || vertical != 0);
    }
    void interact()
    {
        if (Input.GetKeyDown("e"))
        {
            m_Animator.SetBool("IsInteract", true);
            search();
        }
    }

    void search()
    {
        Collider2D[] interactableList = Physics2D.OverlapCircleAll(transform.position, 2, interactableLayerMask);

        //* TODO
        //* Show UI to select interactable

        //* TEMP
        //* Select first interactable
        if (interactableList.Length > 0)
        {
            Interactables interactable = interactableList[0].GetComponent<Interactables>();
            interactable.Interact();
        }
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);

        string inventoryString = "INVENTORY: ";

        foreach (Item i in inventory)
        {
            inventoryString += i.name + ", ";
        }

        Debug.Log(inventoryString);
    }

    public bool CheckItem(Item item)
    {
        return inventory.Contains(item);
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
    }

}