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
    private bool scoreSceneLoaded = false; // �߰� ���� ���� ���� �ε��ߴ��� Ȯ��

    public TMP_Text stageScoreText; // �߰� ���� ���� ȭ���� ���� �ؽ�Ʈ
    public TMP_Text totalScoreText; // ���� ���� ȭ���� ���� �ؽ�Ʈ

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

                StageScore = ballManager.Instance.DestroyCount; // ���� �������� ������ ����
                stageScoreText.text = StageScore.ToString();
                SceneManager.LoadScene("ClearScene");
            }
            else if (stageCount > 2)
            {
                stageCount = 0;

                // ������ ���������� ��� ���� ���� �� �ε�
                SceneManager.LoadScene("FinalScene");
            }
            else
            {
                // �� �ܿ��� ���� ���������� �̵�
                stageCount++;
                SceneManager.LoadScene("Stage" + stageCount);
            }
        }
    }
}
