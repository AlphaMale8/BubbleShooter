using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField]
    public GameObject Character;
    // Ball의 특성
    [SerializeField]
    public float Speed = 5.0f;
    [SerializeField]
    public float ScaleMod = 1.0f;
    [SerializeField]
    public float CameraToBallDestroyDistance = 3.0f;
    public int health = 10;

    public Vector3 SpawnPoint = Vector3.zero;

    private BallManager ballManager;

    // Ball울 초기화하기
    void Start()
    {
        Character = GameObject.FindGameObjectWithTag($"Player");
        ballManager = FindAnyObjectByType<BallManager>();
    }

    void Update()
    {
        // Vector3 dir = (Camera.main.transform.position - transform.position).normalized;
        Vector3 dir = (Character.transform.position - transform.position).normalized;
        transform.Translate(dir* Time.deltaTime * Speed, Space.World);
        transform.rotation = Quaternion.LookRotation(dir) ;
        // if (Vector3.Distance(Camera.main.transform.position, transform.position) <= CameraToBallDestroyDistance)
        // if (Vector3.Distance(Character.transform.position, transform.position) <= CameraToBallDestroyDistance)
        // {
        //     ballManager.BallList.Remove(gameObject);
        //     Destroy(gameObject);
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bullet"))
        {
            if (other.CompareTag("Bullet"))
            {
                ballManager.Instance.DestroyCount++;
            }

            ballManager.Instance.ballList.Remove(gameObject);
            
            this.gameObject.SetActive(false);
        }

        if (other.CompareTag("Bullet"))
        {
            if (health <= 0)
            {
                print(1234);
                ballManager.Instance.ballList.Remove(gameObject);

                this.gameObject.SetActive(false);

            }
        }
    }

    public void InitializeProperty()
    {
        transform.localScale = Vector3.one * ScaleMod;
        transform.position = SpawnPoint;
    }
} 
