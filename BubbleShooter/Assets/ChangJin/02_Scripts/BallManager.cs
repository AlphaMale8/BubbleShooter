using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BallDataSO;
using Random = UnityEngine.Random;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public BallDataSO data;

    [DoNotSerialize] public static int maxPool = 1;

    public List<GameObject> ballList = new List<GameObject>();
    public List<GameObject> ballPool = new List<GameObject>();

    public int DestroyCount { get; set; } = 0;

    public Action StageClear;

    public GameObject Panel;
    public Button NextStageButton;

    public int currentStageScore = 0;


    Dictionary<string, int> A  = new Dictionary<string, int>() { 
        { "Stage1", 1 },
        { "Stage2", 2 },
        { "Stage3", 3 },
    };

    private string TargetSceneName;

    private void Awake()
    {

    }

    private void Start()
    {

        Panel = GameObject.Find("Panel");
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
                TargetSceneName = "End";
                break;
            default:
                break;
        }
        if(NextStageButton != null)
            NextStageButton.onClick.AddListener(LoadScene);
    }
    private void Update()
    {
        if(ballList.Count <= 0)
        {
            Panel.SetActive(true);
        }
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

    void LoadScene()
    {

        SceneManager.LoadScene(TargetSceneName);
    }
}