using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class scoreboardUi : MonoBehaviour
{
    [SerializeField] private RectTransform prefab;
    [SerializeField] private RectTransform container;

    void Start()
    {
        StartCoroutine(scorebaord.instance.DoRetrieveScores());

        scorebaord.instance.callback.AddListener(UpdateDisplay);
    }

    public void Redo()
    {
        StartCoroutine(scorebaord.instance.DoRetrieveScores());
    }

    public async void UpdateDisplay(string scores)
    {

        Debug.Log("updating display");

        if (scores == null)
        {
            Debug.Log("Error retrieving scores");
            return;
        }

        string[] scoreArray = scores.Split(';');

        for (int i = 0; i < scoreArray.Length; i++)
        {
            string[] scoreData = scoreArray[i].Split(',');

            if (scoreData.Length != 3)
            {
                Debug.Log("Error parsing score");
                continue;
            }

            string name = scoreData[0];
            string scoreString = scoreData[1];
            string hoursString = scoreData[2];

            RectTransform scoreTransform = Instantiate(prefab, transform);

            scoreTransform.SetParent(container);

            scoreTransform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();
            scoreTransform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = name;
            scoreTransform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = scoreString;
            scoreTransform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = hoursString;

        }



    }
}
