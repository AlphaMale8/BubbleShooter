using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    public BallManager Instance;

    public Button CreateButton;

    public List<GameObject> BallList { get; } = new List<GameObject>();
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        if(CreateButton != null)
        {
            Button btn = CreateButton.GetComponent<Button>();
            btn.onClick.AddListener(CreateBall);
        }
    }

    void CreateBall()
    {
        BallList.Add(Instantiate(prefabs[Random.Range(0, prefabs.Length)]));
    }
}
