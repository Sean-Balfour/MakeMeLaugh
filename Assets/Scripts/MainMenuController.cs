using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public GameObject mainPanel;
    public GameObject controlsPanel;
    public GameObject creditsPanel;
    GameObject currentPanel;

    public string gameScene;

    // Start is called before the first frame update
    void Start()
    {
        mainPanel.SetActive(true);
        controlsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!mainPanel.activeSelf)
            {
                Back();
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void HowToPlay()
    {
        mainPanel.SetActive(false);
        controlsPanel.SetActive(true);
        currentPanel = controlsPanel;
        Button button = GameObject.Find("BackButton").GetComponent<Button>();
        Debug.Log(button.gameObject.name);
        button.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }

    public void Credits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
        currentPanel = creditsPanel;
        Button button = GameObject.Find("BackButton").GetComponent<Button>();
        Debug.Log(button.gameObject.name);
        button.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }

    public void Back()
    {
        currentPanel.SetActive(false);
        mainPanel.SetActive(true);
        Button button = GameObject.Find("PlayButton").GetComponent<Button>();
        Debug.Log(button.gameObject.name);
        button.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }

    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("Closing Game");
    }
}
