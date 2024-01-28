using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject winScreen;

    [SerializeField]
    private GameObject loseScreen;

    PlayerController playerController;

    private bool hasWon;

    // Start is called before the first frame update
    void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        hasWon = false;

        playerController = PlayerController.instance;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (hasWon)
        {
            winScreen.SetActive(true);
        }
        else
        {
            loseScreen.SetActive(true);
        }

        // ADD SCORE
    }

    public void Return()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
