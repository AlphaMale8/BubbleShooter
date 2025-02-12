using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BallDataSO;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
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
        GameObject go = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);

        ballList.Add(go);
        Ball newBall = go.GetComponent<Ball>();
        newBall.onDisable += () => { RemoveBall(go); };
        itemFeature _feature = data.feature[Random.Range(0, data.feature.Count)];
        newBall.Speed = _feature.Speed;
        newBall.ScaleMod = _feature.ScaleMod;
        newBall.CameraToBallDestroyDistance = _feature.CameraToBallDestroyDistance;
        newBall.MinVector = _feature.MinVector;
        newBall.MaxVector = _feature.MaxVector; 
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
