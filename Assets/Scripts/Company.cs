using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public class Company : MonoBehaviour
{
    public static Company company { get; private set; }

    public Staff StaffPrefab { get => staffPrefab; }
    public List<string> StaffFirstNames { get => staffFirstNames; set => staffFirstNames = value; }
    public List<string> StaffLastNames { get => staffLastNames; set => staffLastNames = value; }

    private List<string> staffFirstNames = new List<string>() { "Parker", "Morgan", "Rowan", "Amy", "Jack", "Sean", "Gareth", "John", "William", "James", "Charles", "George", "Frank", "Joseph", "Thomas", "Henry", "Robert", "Edward", "Harry", "Walter", "Arthur", "Fred", "Albert", "Samuel", "David", "Louis", "Joe", "Charlie", "Clarence", "Richard", "Andrew", "Daniel", "Ernest", "Will", "Jesse", "Oscar", "Lewis", "Peter", "Benjamin", "Frederick", "Willie", "Alfred", "Sam", "Roy", "Herbert", "Jacob", "Tom", "Elmer", "Carl", "Lee", "Howard", "Martin", "Michael", "Bert", "Herman", "Jim", "Francis", "Harvey", "Earl", "Eugene", "Ralph", "Ed", "Claude", "Edwin", "Ben", "Charley", "Paul", "Edgar", "Isaac", "Otto", "Luther", "Lawrence", "Ira", "Patrick", "Guy", "Oliver", "Theodore", "Hugh", "Clyde", "Alexander", "August", "Floyd", "Homer", "Jack", "Leonard", "Horace", "Marion", "Philip", "Allen", "Archie", "Stephen", "Chester", "Willis", "Raymond", "Rufus", "Warren", "Jessie", "Milton", "Alex", "Leo", "Julius", "Ray", "Sidney", "Bernard", "Dan", "Jerry", "Calvin", "Stella", "Sallie", "Nettie", "Etta", "Harriet", "Sadie", "Katie", "Lydia", "Kate", "Mollie", "Lulu", "Nannie", "Lottie", "Belle", "Charlotte", "Amelia", "Hannah", "Jane", "Emily", "Matilda", "Henrietta", "Sara", "Estella", "Theresa", "Josie", "Sophia", "Anne", "Delia", "Louisa", "Mayme", "Estelle", "Nina", "Bettie", "Luella", "Inez", "Lela", "Rosie", "Millie", "Janie", "Cornelia", "Victoria", "Celia", "Christine", "Birdie", "Harriett", "Mable", "Myra", "Sophie", "Tillie", "Isabel", "Sylvia", "Isabelle", "Leila", "Sally", "Ina", "Nell", "Alberta", "Katharine", "Rena", "Mina", "Mathilda", "Dollie", "Hettie", "Fanny", "Lenora", "Adelaide", "Lelia", "Nelle", "Sue", "Johanna", "Lilly", "Lucinda", "Minerva", "Lettie", "Roxie", "Helena", "Hilda", "Hulda", "Genevieve", "Cordelia", "Jeanette", "Adeline", "Leah", "Lura", "Mittie", "Isabella", "Olga", "Phoebe", "Teresa", "Lida", "Lina", "Marguerite", "Claudia", "Cecelia", "Bess", "Emilie", "Rosetta", "Myrtie", "Cecilia", "Olivia", "Ophelia" };
    private List<string> staffLastNames = new List<string>() { "Campbell", "Finney", "Ruthven", "Calderbank", "Aitken" ,"Balfour", "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson", "Martinez", "Anderson", "Taylor", "Thomas", "Hernandez", "Moore", "Martin", "Jackson", "Thompson", "White", "Lopez", "Lee", "Gonzalez", "Harris", "Clark", "Lewis", "Robinson", "Walker", "Perez", "Hall", "Young", "Allen", "Sanchez", "Wright", "King", "Scott", "Green", "Baker", "Adams", "Nelson", "Hill", "Ramirez", "Mitchell", "Roberts", "Carter", "Phillips", "Evans", "Turner", "Torres", "Parker", "Collins", "Edwards", "Stewart", "Flores", "Morris", "Nguyen", "Murphy", "Rivera", "Cook", "Rogers", "Morgan", "Peterson", "Cooper", "Reed", "Bailey", "Bell", "Gomez", "Kelly", "Howard", "Ward", "Cox", "Diaz", "Richardson", "Wood", "Watson", "Brooks", "Bennett", "Gray", "James", "Reyes", "Cruz", "Hughes", "Price", "Myers", "Long", "Foster", "Sanders", "Ross", "Morales", "Powell", "Sullivan", "Russell", "Ortiz", "Jenkins", "Gutierrez", "Perry", "Butler", "Barnes", "Fisher", "Henderson", "Coleman", "Simmons", "Patterson", "Jordan", "Reynolds", "Hamilton", "Graham", "Kim", "Gonzales", "Alexander", "Ramos", "Wallace", "Griffin", "West", "Cole", "Hayes", "Chavez", "Gibson", "Bryant", "Ellis", "Stevens", "Murray", "Ford", "Marshall", "Owens", "Mcdonald", "Harrison", "Ruiz", "Kennedy", "Wells", "Alvarez", "Woods", "Mendoza", "Castillo", "Olson", "Webb", "Washington", "Tucker", "Freeman", "Burns", "Henry", "Vasquez", "Snyder", "Simpson", "Crawford", "Jimenez", "Porter", "Mason", "Shaw", "Gordon", "Wagner", "Hunter", "Romero", "Hicks", "Dixon", "Hunt", "Palmer", "Robertson", "Black", "Holmes", "Stone", "Meyer", "Boyd", "Mills", "Warren", "Fox", "Rose", "Rice", "Moreno", "Schmidt", "Patel", "Ferguson", "Nichols", "Herrera", "Medina", "Ryan", "Fernandez", "Weaver", "Daniels", "Stephens", "Gardner", "Payne", "Kelley", "Dunn", "Pierce", "Arnold", "Tran", "Spencer", "Peters", "Hawkins", "Grant", "Hansen", "Castro", "Hoffman", "Hart", "Elliott", "Cunningham", "Knight", "Bradley" };

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
        CEO.CreateProfile();

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

            CEO.AddLackey(CEO, newStaffLevel);

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
