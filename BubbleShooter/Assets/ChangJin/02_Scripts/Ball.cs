using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField]
    public GameObject Character;
    // Ball의 초기값
    public Camera MainCamera;
    public BallManager ballManager;
    [SerializeField]
    public float Speed = 5.0f;
    [SerializeField]
    public float ScaleMod = 1.0f;
    [SerializeField]
    public float CameraToBallDestroyDistance = 3.0f;

    [SerializeField] private Vector3 MinVector = new Vector3(-5.0f, 0.3f, 5.0f);
    [SerializeField] private Vector3 MaxVector = new Vector3(5.0f, 0.3f, 1.0f);

    public bool Istargeted { get; set; } = false;

    public Action onDisable;
    // Ball의 초기 값 설정
    void OnEnable()
    {
        MainCamera = FindAnyObjectByType<Camera>();
        ballManager = FindAnyObjectByType<BallManager>();

        transform.localScale = Vector3.one * ScaleMod;
        transform.localPosition = new Vector3(
                    Random.Range(MinVector.x, MaxVector.x),
                    Random.Range(MinVector.y, MaxVector.y),
                    Random.Range(MinVector.z, MaxVector.z));
    }

    void Update()
    {
        if (Character != null)
        {
            // 플레이어와 공 사이의 벡터 계산 (공 -> 플레이어 방향)
            Vector3 dir = (MainCamera.transform.position - transform.position).normalized;

            // 공이 그 방향으로 이동하도록 설정
            transform.Translate(dir * Time.deltaTime * Speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GunFront"))
        {
            ballManager.Instance.ballList.Remove(gameObject);
        }

        if (Character != null && other.gameObject.CompareTag("Character"))
        {
            ballManager.Instance.ballList.Remove(gameObject);
            Destroy(gameObject);
        }
    }
} 
