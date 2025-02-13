using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public GameManager Instance;

    [SerializeField] private BallManager ballManager;

    private static int totalScore = 0;
    private static int stageScore = 0;

    private int stageCount = 0;
    private bool scoreSceneLoaded = false; // 중간 집계 점수 씬을 로드했는지 확인

    public TMP_Text stageScoreText; // 중간 집계 점수 화면을 위한 텍스트
    public TMP_Text totalScoreText; // 최종 점수 화면을 위한 텍스트

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
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        stageScoreText = GameObject.FindGameObjectWithTag("StageScoreText").GetComponent<TMP_Text>();
        totalScoreText = GameObject.FindGameObjectWithTag("TotalScoreText").GetComponent<TMP_Text>();


            stageScoreText.gameObject.SetActive(true);
            totalScoreText.gameObject.SetActive(true);
            stageScoreText.text = $"STAGESCORE : {stageScore:0000000}";
            totalScoreText.text = $"TOTALSCORE : {totalScore:0000000}";

    }

    void Update()
    {
        if (ballManager.Instance.isClear == true)
        {
            if (SceneManager.GetActiveScene().name != "ClearScene")
            {
                ballManager.Instance.isClear = false;

                StageScore = ballManager.Instance.DestroyCount; // 현재 스테이지 점수를 설정
                stageScoreText.text = StageScore.ToString();
                SceneManager.LoadScene("ClearScene");
            }
            else if (stageCount > 2)
            {
                stageCount = 0;

                // 마지막 스테이지일 경우 최종 점수 씬 로드
                SceneManager.LoadScene("FinalScene");
            }
            else
            {
                // 그 외에는 다음 스테이지로 이동
                stageCount++;
                SceneManager.LoadScene("Stage" + stageCount);
            }
        }
    }
}
