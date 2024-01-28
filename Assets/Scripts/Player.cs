
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

        if (Input.GetKeyUp(KeyCode.H))
            Level = StaffLevel.CEO;
        else if (Input.GetKeyUp (KeyCode.L))
            Level = StaffLevel.LaidOff;

        if (Level == StaffLevel.CEO)
        {
            gameRunning = false;

            TimeSystem timeSystem = FindFirstObjectByType<TimeSystem>();

            if (timeSystem)
            {
                int companyValue = Company.company.Employees * 50000;
                int hours = (timeSystem.CurrentDay * 12) + timeSystem.CurrentHour;
                Company.company.Score = companyValue / hours;
                Company.company.Hours = hours;
            }

            SceneManager.LoadScene(2);
        }
        else if (Level == StaffLevel.LaidOff)
        {
            gameRunning = false;

            SceneManager.LoadScene(2);
        }
    }
}
