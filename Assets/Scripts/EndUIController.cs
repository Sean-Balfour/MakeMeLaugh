using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EndUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject winScreen;

    [SerializeField]
    private GameObject loseScreen;

    PlayerController playerController;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private InputField InputField;

    [SerializeField]
    private Button submitScore;

    private bool hasWon;

    // Start is called before the first frame update
    void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        hasWon = false;

        if (Company.company)
        {
            hasWon = Company.company.Player.Level == StaffLevel.CEO;
        }

        if (PlayerController.instance)
        {
            playerController = PlayerController.instance;
        }

        OnSceneLoaded();
        //SceneManager.sceneLoaded += OnSceneLoaded;

        submitScore.onClick.AddListener(Submit);
        InputField.onValueChanged.AddListener(Check);
    }

    void Submit ()
    {
        scorebaord.instance.DoPostScores(InputField.text, Company.company.Score, Company.company.Hours);
    }

    void Check(string newsting)
    {
        if(newsting.Length >= 3)
        {
            submitScore.interactable = true;
        } else
        {
            submitScore.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSceneLoaded()
    {
        if (hasWon)
        {
            winScreen.SetActive(true);
            scoreText.text = "Score: $" + Company.company.Score.ToString() + "/hr";
        }
        else
        {
            loseScreen.SetActive(true);
            scoreText.text = "Laid off!";
        }
    }

    public void Return()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    
}
