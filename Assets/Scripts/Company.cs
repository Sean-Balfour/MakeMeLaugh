using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Company : MonoBehaviour
{
    public static Company company { get; private set; }

    public Staff StaffPrefab { get => staffPrefab; }

    [SerializeField]
    private int employees;
    private int companyValue;

    [SerializeField]
    private int RandomSeed = -1;

    [SerializeField]
    private Staff CEO;

    [SerializeField]
    private Staff staffPrefab;

    [SerializeField]
    private bool showRandomChances;

    [SerializeField]
    private float directorChance;
    [SerializeField]
    private float principalChance;
    [SerializeField]
    private float leadChance;
    [SerializeField]
    private float seniorChance;
    [SerializeField]
    private float midChance;
    [SerializeField]
    private float juniorChance;

#if UNITY_EDITOR
    [CustomEditor(typeof(Company))]
    class MyClassEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Company self = (Company)target;
            serializedObject.Update();
            if (self.showRandomChances)
                DrawDefaultInspector();
            else
            {
                string[] varsToHide = { "directorChance", "principalChance", "leadChance", "seniorChance", "midChance", "juniorChance" };
                DrawPropertiesExcluding(serializedObject, varsToHide);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif

    void Awake()
    {
        if (company == null)
        {
            company = this;
            DontDestroyOnLoad(company);
        }
        else
        {
            Destroy(company);
        }
    }

    private void Start()
    {
        if (RandomSeed != -1)
        {
            Random.InitState(RandomSeed);
        }

        CEO = Instantiate(staffPrefab);
        CEO.gameObject.name = "CEO";
        CEO.Init();

        Dictionary<float, StaffLevel> roles = new Dictionary<float, StaffLevel>()
        {
            { directorChance, StaffLevel.Director },
            { principalChance, StaffLevel.Principal },
            { leadChance, StaffLevel.Lead },
            { seniorChance, StaffLevel.Senior},
            { midChance, StaffLevel.Mid },
            { juniorChance, StaffLevel.Junior},
        };

        int addedEmployees = 0;
        while (addedEmployees < employees)
        {
            float roll = Random.value;

            StaffLevel newStaffLevel = StaffLevel.LaidOff;
            foreach (var keyValuePair in roles)
            {
                if (roll < keyValuePair.Key)
                {
                    newStaffLevel = keyValuePair.Value;
                    break;
                }
            }

            Debug.Log(newStaffLevel.ToString());

            addedEmployees++;
        }
    }
}
