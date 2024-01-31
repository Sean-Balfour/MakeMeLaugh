using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button[] m_BackButtons;
    [SerializeField] private Button m_PlayButton, m_HowToPlayButton, m_LeaderboardButton, m_CreditsButton, m_QuitButton;

    [Header("Panels")]
    [SerializeField] private GameObject[] Controls;
    [SerializeField] private GameObject mainPanel, controlsPanel, leaderboardPanel, creditsPanel;

    [Header("Game Scene")]
    [SerializeField] private string gameScene;

    [Header("Private Variables")]
    private GameObject currentPanel;
    private int currentControlDisplay;

    void Awake()
    {
        m_PlayButton.onClick.AddListener(StartGame);
        m_HowToPlayButton.onClick.AddListener(HowToPlay);
        m_LeaderboardButton.onClick.AddListener(Leaderboard);
        m_CreditsButton.onClick.AddListener(Credits);
        m_QuitButton.onClick.AddListener(CloseGame);
        foreach (Button button in m_BackButtons)
        {
            button.onClick.AddListener(Back);
        }
    }

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

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !mainPanel.activeSelf) Back();
    }

    private void OnDestroy()
    {
        m_PlayButton.onClick.RemoveListener(StartGame);
        m_HowToPlayButton.onClick.RemoveListener(HowToPlay);
        m_LeaderboardButton.onClick.RemoveListener(Leaderboard);
        m_CreditsButton.onClick.RemoveListener(Credits);
        m_QuitButton.onClick.RemoveListener(CloseGame);
        foreach (Button button in m_BackButtons)
        {
            button.onClick.RemoveListener(Back);
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
        button.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }

    public void Back()
    {
        currentPanel.SetActive(false);
        mainPanel.SetActive(true);
        Button button = GameObject.Find("PlayButton").GetComponent<Button>();
        button.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}