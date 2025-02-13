using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;
    [HideInInspector] private BallManager ballManager;

    private static int totalScore = 0;
    private static int stageScore = 0;
    private Button exitButton;

    private static int stageCount = 0;

    private GameObject panel;

    public TMP_Text stageScoreText; // 중간 집계 점수 화면을 위한 텍스트
    public TMP_Text totalScoreText; // 최종 점수 화면을 위한 텍스트

    public Action Init;


    public int StageScore
    {
        get { return stageScore; }
        set
        {
            stageScore += value;
        }
    }

    public int TotalScore
    {
        get { return totalScore; }
        set
        {
            totalScore += value;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else 
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        //stageScoreText = GameObject.FindGameObjectWithTag("StageScoreText").GetComponent<TMP_Text>();
        //totalScoreText = GameObject.FindGameObjectWithTag("TotalScoreText").GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        ballManager = GameObject.FindGameObjectWithTag("BallManager").GetComponent<BallManager>();
        panel = GameObject.FindGameObjectWithTag("Panel");
        exitButton = panel.GetComponentInChildren<Button>();

        panel.SetActive(false); // x

        exitButton.onClick.AddListener(() => OnExitButtonClick());

        ballManager.StageClear += () =>
        {
            panel.SetActive(true); // x
            // 패배
        };
    }

    public void OnExitButtonClick()
    {
        panel.SetActive(false);

        Debug.Log(stageCount);
        Debug.Log(SceneManager.sceneCount);
        
        if (stageCount < SceneManager.sceneCount)
        {
            ++stageCount;
            BallManager.maxPool = stageCount == 1 ? 10 : 15;
            SceneManager.LoadScene($"Stage{stageCount +  1}");
        }
        else
        {
            stageCount = 0;
            BallManager.maxPool = 5;
            SceneManager.LoadScene("Title");
        }
    }
}
