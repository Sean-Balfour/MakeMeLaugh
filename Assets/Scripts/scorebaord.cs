
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using UnityEngine.Events;

public class scorebaord : MonoBehaviour
{

    public static scorebaord instance;

    public UnityEvent<string> callback;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one scoreboard in scene!");
            GameObject.Destroy(this);
            return;
        }
        instance = this;
    }


    private const string scoreURL = "https://ggj24.pdox.uk/setscore.php";

    public IEnumerator DoPostScores(string name, int score, int hours)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("score", score);
        form.AddField("hours", hours);

        using (UnityWebRequest www = UnityWebRequest.Post(scoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {

            }
            else
            {

            }
        }
    }

    private const string highscoreURL = "http://ggj24.pdox.uk/getscores.php";

    public IEnumerator DoRetrieveScores()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post(highscoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error retrieving scores");
            }
            else
            {

                string _leaderboardCommaSeperated = www.downloadHandler.text;

                callback.Invoke(_leaderboardCommaSeperated);

                yield return null;
            }
        }
    }
}