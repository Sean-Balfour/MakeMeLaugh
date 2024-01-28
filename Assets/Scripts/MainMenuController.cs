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
    public GameObject leaderboardPanel;
    public GameObject creditsPanel;
    GameObject currentPanel;

    public string gameScene;

    [SerializeField]
    private GameObject[] Controls;

    private int currentControlDisplay;

    // Start is called before the first frame update
    void Start()
    {
        mainPanel.SetActive(true);
        controlsPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        creditsPanel.SetActive(false);
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

    public void Leaderboard()
    {

        mainPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
        leaderboardPanel.GetComponent<scoreboardUi>().Redo();
        currentPanel = leaderboardPanel;
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
