using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BallDataSO;
using Random = UnityEngine.Random;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public BallDataSO data;

    [HideInInspector] public BallManager Instance;

    public Button CreateButton;

    public List<GameObject> ballList = new List<GameObject>();
    public List<GameObject> ballPool = new List<GameObject>();

    public int maxPool = 10;
    public int DestroyCount { get; set; } = 0;

    public bool isClear = false;    

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

    void Start()
    {
        CreateBallPool();
        StartCoroutine(CreateBall());
    }

    private void CreateBallPool()
    {
        for (int i = 0; i < maxPool; i++)
        {
            GameObject go = Instantiate(prefab);
            Ball newBall = go.GetComponent<Ball>();
            BallFeature _feature = data.feature[Random.Range(0, data.feature.Count)];
            Vector3 spawnPoint = data.SpawnPosition[Random.Range(0, data.SpawnPosition.Count)];

            newBall.Speed = _feature.Speed;
            newBall.ScaleMod = _feature.ScaleMod;
            newBall.CameraToBallDestroyDistance = _feature.CameraToBallDestroyDistance;
            newBall.SpawnPoint = spawnPoint;
            newBall.health = _feature.Health;
            GameObject gg = Instantiate(_feature.NormalMesh);
            gg.transform.SetParent(go.transform,false);
            newBall.InitializeProperty();
            

            newBall.name = $"Ball_{i:00}";
            go.SetActive(false);
            ballPool.Add(go);
            ballList.Add(go);
        }

        if (!isClear && ballList.Count == 0)
        {
            isClear = true;
        }
    }

    IEnumerator CreateBall()
    {
        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            // 난수 발생
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


}