using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public class Company : MonoBehaviour
{
    public static Company company { get; private set; }

    public Staff StaffPrefab { get => staffPrefab; }
    public List<string> StaffFirstNames { get => staffFirstNames; }
    public List<string> StaffLastNames { get => staffLastNames; }
    public Player PlayerPrefab { get => playerPrefab; }
    public Transform StaffSpawn { get => staffSpawn; }
    public List<Staff> AllStaff { get => allStaff; set => allStaff = value; }
    public Player Player { get => player; }
    public int Score { get => score; set => score = value; }
    public int Employees { get => employees; }

    private List<string> staffFirstNames = new List<string>() { "Parker", "Morgan", "Rowan", "Amy", "Jack", "Sean", "Gareth", "John", "William", "James", "Charles", "George", "Frank", "Joseph", "Thomas", "Henry", "Robert", "Edward", "Harry", "Walter", "Arthur", "Fred", "Albert", "Samuel", "David", "Louis", "Joe", "Charlie", "Clarence", "Richard", "Andrew", "Daniel", "Ernest", "Will", "Jesse", "Oscar", "Lewis", "Peter", "Benjamin", "Frederick", "Willie", "Alfred", "Sam", "Roy", "Herbert", "Jacob", "Tom", "Elmer", "Carl", "Lee", "Howard", "Martin", "Michael", "Bert", "Herman", "Jim", "Francis", "Harvey", "Earl", "Eugene", "Ralph", "Ed", "Claude", "Edwin", "Ben", "Charley", "Paul", "Edgar", "Isaac", "Otto", "Luther", "Lawrence", "Ira", "Patrick", "Guy", "Oliver", "Theodore", "Hugh", "Clyde", "Alexander", "August", "Floyd", "Homer", "Jack", "Leonard", "Horace", "Marion", "Philip", "Allen", "Archie", "Stephen", "Chester", "Willis", "Raymond", "Rufus", "Warren", "Jessie", "Milton", "Alex", "Leo", "Julius", "Ray", "Sidney", "Bernard", "Dan", "Jerry", "Calvin", "Stella", "Sallie", "Nettie", "Etta", "Harriet", "Sadie", "Katie", "Lydia", "Kate", "Mollie", "Lulu", "Nannie", "Lottie", "Belle", "Charlotte", "Amelia", "Hannah", "Jane", "Emily", "Matilda", "Henrietta", "Sara", "Estella", "Theresa", "Josie", "Sophia", "Anne", "Delia", "Louisa", "Mayme", "Estelle", "Nina", "Bettie", "Luella", "Inez", "Lela", "Rosie", "Millie", "Janie", "Cornelia", "Victoria", "Celia", "Christine", "Birdie", "Harriett", "Mable", "Myra", "Sophie", "Tillie", "Isabel", "Sylvia", "Isabelle", "Leila", "Sally", "Ina", "Nell", "Alberta", "Katharine", "Rena", "Mina", "Mathilda", "Dollie", "Hettie", "Fanny", "Lenora", "Adelaide", "Lelia", "Nelle", "Sue", "Johanna", "Lilly", "Lucinda", "Minerva", "Lettie", "Roxie", "Helena", "Hilda", "Hulda", "Genevieve", "Cordelia", "Jeanette", "Adeline", "Leah", "Lura", "Mittie", "Isabella", "Olga", "Phoebe", "Teresa", "Lida", "Lina", "Marguerite", "Claudia", "Cecelia", "Bess", "Emilie", "Rosetta", "Myrtie", "Cecilia", "Olivia", "Ophelia" };
    private List<string> staffLastNames = new List<string>() { "Campbell", "Finney", "Ruthven", "Calderbank", "Aitken" ,"Balfour", "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson", "Martinez", "Anderson", "Taylor", "Thomas", "Hernandez", "Moore", "Martin", "Jackson", "Thompson", "White", "Lopez", "Lee", "Gonzalez", "Harris", "Clark", "Lewis", "Robinson", "Walker", "Perez", "Hall", "Young", "Allen", "Sanchez", "Wright", "King", "Scott", "Green", "Baker", "Adams", "Nelson", "Hill", "Ramirez", "Mitchell", "Roberts", "Carter", "Phillips", "Evans", "Turner", "Torres", "Parker", "Collins", "Edwards", "Stewart", "Flores", "Morris", "Nguyen", "Murphy", "Rivera", "Cook", "Rogers", "Morgan", "Peterson", "Cooper", "Reed", "Bailey", "Bell", "Gomez", "Kelly", "Howard", "Ward", "Cox", "Diaz", "Richardson", "Wood", "Watson", "Brooks", "Bennett", "Gray", "James", "Reyes", "Cruz", "Hughes", "Price", "Myers", "Long", "Foster", "Sanders", "Ross", "Morales", "Powell", "Sullivan", "Russell", "Ortiz", "Jenkins", "Gutierrez", "Perry", "Butler", "Barnes", "Fisher", "Henderson", "Coleman", "Simmons", "Patterson", "Jordan", "Reynolds", "Hamilton", "Graham", "Kim", "Gonzales", "Alexander", "Ramos", "Wallace", "Griffin", "West", "Cole", "Hayes", "Chavez", "Gibson", "Bryant", "Ellis", "Stevens", "Murray", "Ford", "Marshall", "Owens", "Mcdonald", "Harrison", "Ruiz", "Kennedy", "Wells", "Alvarez", "Woods", "Mendoza", "Castillo", "Olson", "Webb", "Washington", "Tucker", "Freeman", "Burns", "Henry", "Vasquez", "Snyder", "Simpson", "Crawford", "Jimenez", "Porter", "Mason", "Shaw", "Gordon", "Wagner", "Hunter", "Romero", "Hicks", "Dixon", "Hunt", "Palmer", "Robertson", "Black", "Holmes", "Stone", "Meyer", "Boyd", "Mills", "Warren", "Fox", "Rose", "Rice", "Moreno", "Schmidt", "Patel", "Ferguson", "Nichols", "Herrera", "Medina", "Ryan", "Fernandez", "Weaver", "Daniels", "Stephens", "Gardner", "Payne", "Kelley", "Dunn", "Pierce", "Arnold", "Tran", "Spencer", "Peters", "Hawkins", "Grant", "Hansen", "Castro", "Hoffman", "Hart", "Elliott", "Cunningham", "Knight", "Bradley" };

    [SerializeField]
    private int employees;
    [SerializeField]
    private int score;
    [SerializeField]
    private int RandomSeed = -1;

    [SerializeField]
    private List<Staff> allStaff = new List<Staff>();
    [SerializeField]
    private List<Staff> staffOnLevel = new List<Staff>();

    [SerializeField]
    private Staff CEO;
    [SerializeField]
    private Staff staffPrefab;
    [SerializeField]
    private Player playerPrefab;
    [SerializeField] 
    private Player player;
    [SerializeField]
    private Desk deskPrefab;
    [SerializeField]
    private GameObject deskSpawns;

    [SerializeField]
    private Transform staffSpawn;
    [SerializeField]
    private Transform door;

    [SerializeField]
    private bool showRandomChances;

    [Header("Chances")]
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

    [Header("Amounts")]
    [SerializeField]
    private int directorAmount;
    [SerializeField]
    private int principalAmount;
    [SerializeField]
    private int leadAmount;
    [SerializeField]
    private int seniorAmount;
    [SerializeField]
    private int midAmount;

    [Header("Limits")]
    [SerializeField]
    private int directorLimit;
    [SerializeField]
    private int principalLimit;
    [SerializeField]
    private int leadLimit;
    [SerializeField]
    private int seniorLimit;
    [SerializeField]
    private int midLimit;


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
                string[] varsToHide = { "directorChance", "principalChance", "leadChance", "seniorChance", "midChance", "juniorChance", "directorLimit", "principalLimit", "leadLimit", "seniorLimit", "midLimit" };
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

        CEO = Instantiate(staffPrefab, staffSpawn.position, Quaternion.identity);
        CEO.gameObject.name = "CEO";
        CEO.Init();
        CEO.CreateProfile();
        CEO.transform.SetParent(transform);
        allStaff.Add(CEO);

        GenerateStaff();

        for (int i = 0; i <= deskSpawns.transform.childCount - 1; i++)
        {
            Desk newDesk = Instantiate(deskPrefab, deskSpawns.transform.GetChild(i).position, deskSpawns.transform.GetChild(i).rotation);
        }

        for (int i = 0; i < allStaff.Count; i++)
        {
            if (allStaff[i].Level == StaffLevel.Intern)
                player = allStaff[i].gameObject.GetComponent<Player>();
        }

        StartDay();
    }

    public void NewDay()
    {
        StartDay();
    }

    public void StartDay()
    {
        staffOnLevel.Clear();
        for (int i = 0; i < allStaff.Count; i++)
        {
            allStaff[i].gameObject.transform.position = staffSpawn.position;
            if (allStaff[i].gameObject.GetComponent<NPCAI>())
            {
                allStaff[i].gameObject.GetComponent<NPCAI>().enabled = false;
            }
        }

        Staff currentStaff = player;
        while (currentStaff.Level != StaffLevel.CEO)
        {
            staffOnLevel.Add(currentStaff);
            currentStaff = currentStaff.Boss;
        }

        // current staff is now ceo
        staffOnLevel.Add(currentStaff);

        Staff randomStaff;
        for (int i = staffOnLevel.Count; i < deskSpawns.transform.childCount; i++)
        {
            do
            {
                randomStaff = allStaff[UnityEngine.Random.Range(0, allStaff.Count)];
            } while (staffOnLevel.Contains(randomStaff));

            staffOnLevel.Add(randomStaff);
        }

        for (int i = 0; i < staffOnLevel.Count; i++)
        {
            staffOnLevel[i].transform.position = door.position;
            if (allStaff[i].gameObject.GetComponent<NPCAI>())
            {
                allStaff[i].gameObject.GetComponent<NPCAI>().enabled = true;
            }
        }

        Desk[] desks = FindObjectsByType<Desk>(FindObjectsSortMode.None);
        for (int i = 0; i < desks.Length; i++)
        {
            desks[i].RandomiseDesk();
        }
    }

    private void GenerateStaff()
    {
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
            int rollsMade = 0;
            int rollLimit = 10;
            bool overLimit = true;
            StaffLevel newStaffLevel = StaffLevel.LaidOff;
            while (overLimit && rollsMade < rollLimit)
            {
                roll = Random.value;
                rollsMade++;
                foreach (var keyValuePair in roles)
                {
                    if (roll < keyValuePair.Key)
                    {
                        newStaffLevel = keyValuePair.Value;

                        if (rollsMade >= rollLimit)
                            newStaffLevel = StaffLevel.Junior;

                        switch (newStaffLevel)
                        {
                            case StaffLevel.Director:
                                if (directorAmount < directorLimit)
                                    overLimit = false;
                                break;
                            case StaffLevel.Principal:
                                if (principalAmount < principalLimit)
                                    overLimit = false;
                                break;
                            case StaffLevel.Lead:
                                if (leadAmount < leadLimit)
                                    overLimit = false;
                                break;
                            case StaffLevel.Senior:
                                if (seniorAmount < seniorLimit)
                                    overLimit = false;
                                break;
                            case StaffLevel.Mid:
                                if (midAmount < midLimit)
                                    overLimit = false;
                                break;
                        }

                        break;
                    }
                }
            }

            allStaff.Add(CEO.AddLackey(CEO, newStaffLevel));
            
            switch (newStaffLevel)
            {
                case StaffLevel.Director:
                    directorAmount++;
                    break;
                case StaffLevel.Principal:
                    principalAmount++;
                    break;
                case StaffLevel.Lead:
                    leadAmount++;
                    break;
                case StaffLevel.Senior:
                    seniorAmount++;
                    break;
                case StaffLevel.Mid:
                    midAmount++;
                    break;
            }

            addedEmployees++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            CEO.Lackeys[UnityEngine.Random.Range(0, CEO.Lackeys.Count)].LayOff();
        }
    }
}
