using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    public Button CreateButton;
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
        Instantiate(prefabs[Random.Range(0, prefabs.Length)]);

    }

    void Update()
    {

    }
}
