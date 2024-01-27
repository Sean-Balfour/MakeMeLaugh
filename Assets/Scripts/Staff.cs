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
    protected Staff boss;
    [SerializeField]
    private List<Staff> lackeys;
    [SerializeField]
    protected StaffLevel level;

    [SerializeField]
    private string firstName;
    [SerializeField]
    private string lastName;

    private int pay;

    public List<Staff> Lackeys { get => lackeys; }

    public void CreateProfile()
    {
        firstName = Company.company.StaffFirstNames[UnityEngine.Random.Range(0, Company.company.StaffFirstNames.Count - 1)];
        lastName = Company.company.StaffLastNames[UnityEngine.Random.Range(0, Company.company.StaffLastNames.Count - 1)];
    }

    public void Init()
    {
        if (level != StaffLevel.Intern)
        {
            Staff newStaff = Instantiate(Company.company.StaffPrefab);
            newStaff.level = (level + 1);
            newStaff.boss = this;

            lackeys.Add(newStaff);
            newStaff.Init();
            newStaff.gameObject.name = (level + 1).ToString();
        }
    }

    public void AddLackey(Staff boss, StaffLevel lackeyLevel)
    {
        if (lackeyLevel == level + 1)
        {
            Staff newStaff = Instantiate(Company.company.StaffPrefab);
            newStaff.level = lackeyLevel;
            newStaff.gameObject.name = lackeyLevel.ToString();
            newStaff.boss = this;

            lackeys.Add(newStaff);

            if (lackeyLevel == StaffLevel.Intern)
            {
                // do player stuff
            }
        }
        else
        {
            if (lackeys.Count == 0)
            {
                AddLackey(this, level + 1);
            }

            lackeys[UnityEngine.Random.Range(0, lackeys.Count - 1)].AddLackey(this, lackeyLevel);
        }
    }

    public void Promote()
    {
        level -= 1;
        gameObject.name = level.ToString();
    }

    public void LayOff()
    {
        if (lackeys.Count == 0)
        {
            AddLackey(this, level + 1);
        }

        Staff lackeyToPromote = lackeys[UnityEngine.Random.Range(0, lackeys.Count - 1)];
        lackeyToPromote.Promote();
        lackeyToPromote.boss = boss;
        boss.lackeys.Add(lackeyToPromote);

        for (int i = 0; i < lackeyToPromote.lackeys.Count; i++)
        {
            lackeyToPromote.lackeys[i].Promote();
        }

        boss.lackeys.Remove(this);

        Destroy(this.gameObject);
    }
}
