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
    private Staff boss;
    [SerializeField]
    private List<Staff> lackeys;
    [SerializeField]
    private StaffLevel level;

    [SerializeField]
    private string firstName;
    [SerializeField]
    private string lastName;

    private int pay;

    public List<Staff> Lackeys { get => lackeys; }
    public StaffLevel Level { get => level; }
    public Staff Boss { get => boss; }

    public void CreateProfile()
    {
        firstName = Company.company.StaffFirstNames[UnityEngine.Random.Range(0, Company.company.StaffFirstNames.Count - 1)];
        lastName = Company.company.StaffLastNames[UnityEngine.Random.Range(0, Company.company.StaffLastNames.Count - 1)];
    }

    public void Init()
    {
        if (level != StaffLevel.Intern)
        {
            Staff newStaff;
            
            if ((level + 1) != StaffLevel.Intern)
                newStaff = Instantiate(Company.company.StaffPrefab, Company.company.StaffSpawn.position, Quaternion.identity);
            else
                newStaff = Instantiate(Company.company.PlayerPrefab, Company.company.StaffSpawn.position, Quaternion.identity);
              
            newStaff.level = (level + 1);
            newStaff.boss = this;
            newStaff.CreateProfile();
            newStaff.transform.SetParent(Company.company.transform);

            lackeys.Add(newStaff);
            newStaff.Init();
            newStaff.gameObject.name = (level + 1).ToString();

            Company.company.AllStaff.Add(newStaff);
        }
    }

    public Staff AddLackey(Staff boss, StaffLevel lackeyLevel)
    {
        if (lackeyLevel == level + 1)
        {
            Staff newStaff = Instantiate(Company.company.StaffPrefab, Company.company.StaffSpawn.position, Quaternion.identity);
            newStaff.level = lackeyLevel;
            newStaff.gameObject.name = lackeyLevel.ToString();
            newStaff.boss = this;
            newStaff.CreateProfile();
            newStaff.transform.SetParent(Company.company.transform);

            lackeys.Add(newStaff);

            return newStaff;
        }
        else
        {
            if (lackeys.Count == 0)
            {
                AddLackey(this, level + 1);
            }

            return lackeys[UnityEngine.Random.Range(0, lackeys.Count - 1)].AddLackey(this, lackeyLevel);
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
