using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject controlsPanel;
    private bool isPaused;

    [SerializeField]
    private GameObject[] Controls;

    private int currentControlDisplay;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        controlsPanel.SetActive(false);
        currentControlDisplay = 0;
        Controls[currentControlDisplay].SetActive(true);
        for (int i = 1; i < Controls.Length; i++)
        {
            Controls[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (controlsPanel.activeSelf)
            {
                Back();
            }
            else
            {
                Time.timeScale = 0;

                isPaused = !isPaused;
                pausePanel.SetActive(isPaused);
                Button button = GameObject.Find("ResumeButton").GetComponent<Button>();
                Debug.Log(button.gameObject.name);
                button.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(button.gameObject);
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
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

    public void ChangeControl(int direction)
    {
        Controls[currentControlDisplay].SetActive(false);
        currentControlDisplay += direction;
        if (currentControlDisplay >= Controls.Length)
        {
            currentControlDisplay = 0;
        }
        else if (currentControlDisplay < 0)
        {
            currentControlDisplay = Controls.Length - 1;
        }
        Controls[currentControlDisplay].SetActive(true);
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
