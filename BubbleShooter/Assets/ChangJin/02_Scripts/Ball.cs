using System;
using System.Linq;
using UnityEngine;
using static BallDataSO;
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

    public BallFeature ballFeature;

    [SerializeField]private GameObject normalMesh;
    [SerializeField]private GameObject damagedMesh;

    // Ball울 초기화하기
    void Start()
    {
        Character = GameObject.FindGameObjectWithTag($"Player");
        ballManager = FindAnyObjectByType<BallManager>();
    }

    void Update()
    {
        // Vector3 dir = (Camera.main.transform.position - transform.position).normalized;
        Vector3 dir = ((Character.transform.position + new Vector3(0,4,0)) - transform.position).normalized;
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
        if (other.CompareTag("Player"))
        {
            ballManager.ballList.Remove(gameObject);            
            this.gameObject.SetActive(false);
            ballManager.Player.Hp -= 100; // 임시 사망
        }

        if (other.CompareTag("Bullet"))
        {
            if (health <= 0)
            {
                ballManager.DestroyCount++;
                ballManager.ballList.Remove(gameObject);

                this.gameObject.SetActive(false);
            }

            if (normalMesh)
            {
                damagedMesh.SetActive(true);
                normalMesh.SetActive(false);
            }
        }
    }

    public void InitializeProperty(BallFeature _feature, Vector3 SpawnPoint)
    {
        // 구조체 할당
        ballFeature = _feature;

        // transform 초기화
        transform.localScale = Vector3.one * _feature.ScaleMod;
        transform.position = SpawnPoint;

        // 특성값 초기화
        Speed = _feature.Speed;
        CameraToBallDestroyDistance = _feature.CameraToBallDestroyDistance;
        health = _feature.Health;

        // 최초 프리팹 추가
        normalMesh = Instantiate(_feature.NormalMesh);
        normalMesh.transform.SetParent(transform, false);
        normalMesh.SetActive(true);

        damagedMesh = Instantiate(_feature.DamagedMesh);
        damagedMesh.transform.SetParent(transform, false);
        damagedMesh.SetActive(false);
    }
} 
