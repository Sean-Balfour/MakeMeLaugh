using System;
using System.Collections.Generic;
using UnityEngine;

public enum StaffLevel
{
    CEO,
    Director,
    Principal,
    Lead,
    Senior,
    Mid,
    Junior,
    Intern,
    LaidOff
}

public class Staff : MonoBehaviour
{
    [SerializeField]
    private List<Staff> lackeys;
    [SerializeField]
    private StaffLevel level;

    public Staff(StaffLevel inLevel)
    {
        level = inLevel;
    }

    public void Init()
    {
        if (level != StaffLevel.Intern)
        {
            Staff newStaff = Instantiate(Company.company.StaffPrefab);
            newStaff.level = (level + 1);

            lackeys.Add(newStaff);
            newStaff.Init();
            newStaff.gameObject.name = (level + 1).ToString();
        }
    }

    public void AddLackey(StaffLevel lackeyLevel)
    {
        if (lackeyLevel == level + 1)
        {
            Staff newStaff = Instantiate(Company.company.StaffPrefab);
            newStaff.level = lackeyLevel;

            lackeys.Add(newStaff);
        }
        else
        {
            lackeys[UnityEngine.Random.Range(0, lackeys.Count - 1)].AddLackey(lackeyLevel);
        }
    }
}
