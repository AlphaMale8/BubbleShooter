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
        SceneManager.LoadScene(1);
    }

    private void CreateBallPool()
    {
        for (int i = 0; i < maxPool; i++)
        {
            GameObject go = Instantiate(prefab);
            Ball newBall = go.GetComponent<Ball>();
            BallFeature _feature = data.feature[Random.Range(0, data.feature.Count)];

            newBall.Speed = _feature.Speed;
            newBall.ScaleMod = _feature.ScaleMod;
            newBall.CameraToBallDestroyDistance = _feature.CameraToBallDestroyDistance;
            newBall.MinVector = _feature.MinVector;
            newBall.MaxVector = _feature.MaxVector;
            newBall.InitializeProperty();

            newBall.name = $"Ball_{i:00}";
            go.SetActive(false);
            ballPool.Add(go);
            ballList.Add(go);
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

//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using Random = UnityEngine.Random;

//public class BallManager : MonoBehaviour
//{
//    [SerializeField] private GameObject[] prefabs;

//    public BallManager Instance;

//    public Button CreateButton;

//    public List<GameObject> BallList { get; } = new List<GameObject>();

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(Instance);
//        }
//        else if (Instance != this)
//        {
//            Destroy(this);
//        }
//    }

//    void Start()
//    {
//        if (CreateButton != null)
//        {
//            Button btn = CreateButton.GetComponent<Button>();
//            btn.onClick.AddListener(CreateBall);
//        }
//    }

//    void CreateBall()
//    {
//        BallList.Add(Instantiate(prefabs[Random.Range(0, prefabs.Length)]));
//    }
//}
