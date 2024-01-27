using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUiController : MonoBehaviour
{
    [SerializeField] private InteractionUiButton m_Prefab;
    [SerializeField] private Transform m_Container;

    private int m_CurrentIndex = 0;

    public void ShowInteractables(Collider2D[] interactables)
    {
        foreach (Transform child in m_Container)
        {
            Destroy(child.gameObject);
        }

        if (interactables.Length == 0)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            return;
        }

        Debug.Log("Showing interactables");

        gameObject.transform.GetChild(0).gameObject.SetActive(true);

        foreach (var interactable in interactables)
        {
            InteractionUiButton button = Instantiate(m_Prefab, transform);
            button.Setup(interactable);
            button.transform.SetParent(m_Container);
        }

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_Container.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_Container.transform.parent.GetComponent<RectTransform>());
    }

    void Update()
    {
        if (!PlayerController.instance.isInteracting) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerController.instance.StopInteracting();
            return;
        }




        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {

            if (m_Container.GetChild(m_CurrentIndex).GetComponent<InteractionUiButton>().isDisabled) { PlayerController.instance.StopInteracting(); return; }

            m_Container.GetChild(m_CurrentIndex).GetComponent<InteractionUiButton>().m_Interactable.Interact();
            return;
        }


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_CurrentIndex--;
            if (m_CurrentIndex < 0)
            {
                m_CurrentIndex = m_Container.childCount - 1;
            }

            int timesLooped = 0;

            while (m_Container.GetChild(m_CurrentIndex).GetComponent<InteractionUiButton>().isDisabled && timesLooped < m_Container.childCount)
            {
                m_CurrentIndex--;
                timesLooped++;
                if (m_CurrentIndex < 0)
                {
                    m_CurrentIndex = m_Container.childCount - 1;
                }
            }

        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_CurrentIndex++;
            if (m_CurrentIndex >= m_Container.childCount)
            {
                m_CurrentIndex = 0;
            }

            int timesLooped = 0;

            while (m_Container.GetChild(m_CurrentIndex).GetComponent<InteractionUiButton>().isDisabled && timesLooped < m_Container.childCount)
            {
                m_CurrentIndex++;
                timesLooped++;
                if (m_CurrentIndex >= m_Container.childCount)
                {
                    m_CurrentIndex = 0;
                }
            }

        }

        UpdateSelected();
    }


    void UpdateSelected()
    {
        for (int i = 0; i < m_Container.childCount; i++)
        {
            m_Container.GetChild(i).GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }

        m_Container.GetChild(m_CurrentIndex).GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
    }

    public void Hide()
    {

        Debug.Log("Hiding interactables");
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}