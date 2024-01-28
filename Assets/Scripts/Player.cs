
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Staff
{
    private bool gameRunning = false;

    private void Start()
    {
        gameRunning = true;
    }

    private void Update()
    {
        if (!gameRunning) return;

        if (Level == StaffLevel.CEO)
        {
            gameRunning = false;

            TimeSystem timeSystem = FindFirstObjectByType<TimeSystem>();

            if (timeSystem)
            {
                int companyValue = Company.company.Employees * 50000;
                int hours = (timeSystem.CurrentDay * 12) + timeSystem.CurrentHour;
                Company.company.Score = companyValue / hours;
            }

            SceneManager.LoadScene(0);
        }
        else if (Level == StaffLevel.LaidOff)
        {
            gameRunning = false;

            SceneManager.LoadScene(0);
        }
    }
}
