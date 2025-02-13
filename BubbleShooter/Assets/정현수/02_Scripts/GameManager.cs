//using TMPro;
//using UnityEditor.SearchService;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.SocialPlatforms.Impl;

//public class GameManager : MonoBehaviour
//{
//    [HideInInspector] public GameManager Instance;

//    [SerializeField] private BallManager ballManager;

//    private int totalScore = 0;

//    private int sceneNum = 1;

//    private bool isLoaded = false;

//    public TMP_Text finalScoreText;

//    public int TotalScore
//    {
//        get
//        {
//            return totalScore;
//        }
//        set
//        {
//            totalScore += value;
//            PlayerPrefs.SetInt("SCORE", totalScore);
//            finalScoreText.text = $"SCORE : {totalScore:0000000}";
//        }
//    }

//    public static int StageScore { get; set; } = 0;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(this.gameObject);
//        }
//        else if (Instance != this)
//        {
//            Destroy(this.gameObject);
//        }
//    }

//    private void Start()
//    {


//    }

//    private void OnEnable()
//    {
//    }

//    void Update()
//    {
            
//    }
//}
