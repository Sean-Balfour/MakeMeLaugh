using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject controlsPanel;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pausePanel.activeSelf)
            {
                isPaused = !isPaused;
                pausePanel.SetActive(false);
                Button button = GameObject.Find("ResumeButton").GetComponent<Button>();
                Debug.Log(button.gameObject.name);
                button.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(button.gameObject);
            }
            else
            {
                Back();
            }

            // FREEZE GAME
        }
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);

        // UNFREEZE GAME
    }

    public void HowToPlay()
    {
        pausePanel.SetActive(false);
        controlsPanel.SetActive(true);
        Button button = GameObject.Find("BackButton").GetComponent<Button>();
        Debug.Log(button.gameObject.name);
        button.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }

    public void Back()
    {
        controlsPanel.SetActive(false);
        pausePanel.SetActive(true);
        Button button = GameObject.Find("ResumeButton").GetComponent<Button>();
        Debug.Log(button.gameObject.name);
        button.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");

        // RESET ANY VARIABLES
    }
}
