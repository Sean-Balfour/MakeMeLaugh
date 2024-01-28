using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeSystem : MonoBehaviour
{
    [SerializeField]
    private int currentDay;
    [SerializeField]
    private int currentHour;
    [SerializeField]
    private int currentMinutes;

    [SerializeField]
    private float currentSpeed;

    [SerializeField]
    private TextMeshProUGUI dayText;
    [SerializeField]
    private TextMeshProUGUI timeText;

    private void Start()
    {
        currentDay = 1;
        currentHour = 8;
        currentMinutes = 0;
        StartCoroutine(TickUp());
    }

    private IEnumerator TickUp()
    {
        yield return new WaitForSeconds(1.0f * currentSpeed);
        currentMinutes++;

        if (currentMinutes > 59)
        {
            currentHour++;
            currentMinutes = 0;
        }

        if (currentHour >= 20)
        {
            Company.company.NewDay();
            currentHour = 8;
            currentMinutes = 0;
            currentDay++;
        }
        else if (currentHour >= 19)
        {
            timeText.color = Color.red;
        }
        else
        {
            timeText.color = Color.white;
        }

        string currentHourString = ""; 
        string currentMinutesString = "";

        if (currentHour < 10)
            currentHourString = "0" + currentHour.ToString();
        else
        currentHourString = currentHour.ToString();

        if (currentMinutes < 10)
            currentMinutesString = "0" + currentMinutes.ToString();
        else
            currentMinutesString = currentMinutes.ToString();

        dayText.text = "Day " + currentDay.ToString();
        timeText.text = currentHourString + ":" + currentMinutesString; 

        StartCoroutine(TickUp());
    }
}
