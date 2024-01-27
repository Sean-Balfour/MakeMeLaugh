using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUiButton : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI m_Text;

    public bool isDisabled { get; private set; } = false;

    public Interactables m_Interactable { get; private set; }


    // Start is called before the first frame update
    public void Setup(Collider2D interactable)
    {

        m_Interactable = interactable.GetComponent<Interactables>();

        m_Text.text = m_Interactable.GetName();

        isDisabled = !m_Interactable.isInteractable;

        if (isDisabled)
        {
            m_Text.color = Color.gray;
        }

    }
}
