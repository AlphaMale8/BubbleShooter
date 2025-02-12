using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BallDataSO;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public BallDataSO data;
    
    public List<GameObject> ballList = new List<GameObject>();
    public BallManager Instance = null;

    public Button CreateButton;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        if (CreateButton != null)
        {
            Button btn = CreateButton.GetComponent<Button>();
            btn.onClick.AddListener(CreateBall);
        }
        
    }

    void CreateBall()
    {
        // Ball Prefab을 생성하고 값 할당하기. Ball에 새로운 프로퍼티를 추가하려면
        // BallDataSO의 BallFeature 구조체에 프로퍼티 추가해주면 됨.
        if (prefab != null)
        {
            GameObject go = Instantiate(prefab);

            ballList.Add(go);
            Ball newBall = go.GetComponent<Ball>();
        
            // Assign newBall Data
            BallFeature _feature = data.feature[Random.Range(0, data.feature.Count)];
            newBall.Speed = _feature.Speed;
            newBall.ScaleMod = _feature.ScaleMod;
            newBall.CameraToBallDestroyDistance = _feature.CameraToBallDestroyDistance;
            newBall.MinVector = _feature.MinVector;
            newBall.MaxVector = _feature.MaxVector;
            newBall.InitializeProperty();

            newBall.onDisable += () => { RemoveBall(go); };
        }
    }

    public void RemoveBall(GameObject go)
    {
        Destroy(go);
        ballList.Remove(go);
    }

    public GameObject GetFirstBall()
    {
        if (ballList.Count == 0) return null;
        else return ballList[0];
    }

    void Update()
    {

    }
}
