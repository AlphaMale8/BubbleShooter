using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BallDataSO;
using Random = UnityEngine.Random;

public class BallManager : MonoBehaviour
{
    // Player
    private Player player;
    public Player Player { get => player; }
    float maxHp = 100;

    [SerializeField] private GameObject prefab;

    // data
    public BallDataSO data;
    [DoNotSerialize] public static int maxPool = 1;

    public List<GameObject> ballList = new List<GameObject>();
    public List<GameObject> ballPool = new List<GameObject>();

    // count
    private int destoryCount = 0;
    public int DestroyCount
    {
        get => destoryCount;
        set
        {
            destoryCount = value;
            OnStageScoreChange?.Invoke(destoryCount);
        }

    }
    public int currentStageScore = 0;

    // action
    public Action StageClear;
    public Action<int> OnStageScoreChange;


    // panel
    public GameObject Panel;
    public Button NextStageButton; // 패널 버튼

    private bool isGameEnd = false;

    Dictionary<string, int> A  = new Dictionary<string, int>() { 
        { "Stage1", 1 },
        { "Stage2", 2 },
        { "Stage3", 3 },
    };

    private string TargetSceneName;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }

    private void Start()
    {
        isGameEnd = false;
        Panel.SetActive(false);

        print(SceneManager.GetActiveScene().name);
        switch (A[SceneManager.GetActiveScene().name])
        {
            case 1:
                TargetSceneName = "Stage2";
                break;
            case 2:
                TargetSceneName = "Stage3";
                break;
            case 3:
                TargetSceneName = "Title";
                break;
            default:
                break;
        }

        if(NextStageButton != null)
            NextStageButton.onClick.AddListener(() => { LoadScene(); player.animator.SetBool("IsVictory", false); });

        player.Hp = maxHp;
    }

    private float maxTimer = 3f;
    private void Update()
    {
        if (!isGameEnd && player.Hp <= 0)
        {
            isGameEnd = true;
            player.Hp = 0;

            player.animator.SetTrigger("IsDead");

            StopAllCoroutines();
            StartCoroutine(deadProcess());
        }

        if(!isGameEnd && ballList.Count <= 0 && player.Hp > 0f) // 몬스터 개수 0, 플레이어가 생존일때
        {
            isGameEnd = true;
            // 승리
            Panel.SetActive(true);

            player.animator.SetBool("IsVictory", true);

            // UI 표시
            PanelUI ui = Panel.GetComponentInChildren<PanelUI>();
            ui.SetTitleText("Victory", Color.green);
            ui.SetStageScoreText(DestroyCount);

            int currentTotalScore = PlayerPrefs.GetInt("Score");
            PlayerPrefs.SetInt("Score", currentTotalScore + DestroyCount);

            if(A[SceneManager.GetActiveScene().name] == 3) // 최종 스테이지 일 때
            {
                ui.SetTotalScoreText(PlayerPrefs.GetInt("Score"));
            }
            else
            {
                ui.SetTotalScoreText();
            }
        }
    }

    private IEnumerator deadProcess()
    {
        float timeElapsed = 0;
        while(timeElapsed < maxTimer)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 패배
        Panel.SetActive(true);

        PanelUI ui = Panel.GetComponentInChildren<PanelUI>();
        ui.SetTitleText("Defeat . . .", Color.red);
        ui.SetStageScoreText(DestroyCount);
        ui.SetTotalScoreText(PlayerPrefs.GetInt("Score"));

        if (NextStageButton != null)
            NextStageButton.onClick.AddListener(() => { SceneManager.LoadScene("Title"); Time.timeScale = 1f; player.animator.SetBool("IsDead", false); });
    }

    private void OnEnable()
    {
        CreateBallPool();
        StartCoroutine(CreateBall());
        StartCoroutine(AllBallWasted());
    }

    private void CreateBallPool()
    {
        for (int i = 0; i < maxPool; i++)
        {
            GameObject go = Instantiate(prefab);
            Ball newBall = go.GetComponent<Ball>();
            newBall.InitializeProperty(data.feature[Random.Range(0, data.feature.Count)], data.SpawnPosition[Random.Range(0, data.SpawnPosition.Count)]);


            newBall.name = $"Ball_{i:00}";
            go.SetActive(false);
            ballPool.Add(go);
            ballList.Add(go);

            print(ballList.Count);
        }
    }

    IEnumerator CreateBall()
    {
        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            foreach (var ball in ballList)
            {
                if (ball.activeSelf == false)
                {
                    ball.SetActive(true);
                    break;
                }
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator AllBallWasted()
    {
        while (true)
        {
            if (ballList.Count == 0)
            {
                StageClear?.Invoke(); // x
                break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    // 버튼 이벤트 함수 ===============================================

    void LoadScene()
    {
        SceneManager.LoadScene(TargetSceneName);
    }
}