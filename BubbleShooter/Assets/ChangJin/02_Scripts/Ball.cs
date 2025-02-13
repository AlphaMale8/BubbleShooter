using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField]
    public GameObject Character;
    // Ball�� �ʱⰪ
    [SerializeField]
    public float Speed = 5.0f;
    [SerializeField]
    public float ScaleMod = 1.0f;
    [SerializeField]
    public float CameraToBallDestroyDistance = 3.0f;

    public Vector3 MinVector = new Vector3(-5.0f, 0.3f, 5.0f);
    public Vector3 MaxVector = new Vector3(5.0f, 0.3f, 1.0f);

    public int health = 10;

    private BallManager ballManager;

    // Ball�� �ʱ� �� ����
    void Start()
    {
        Character = GameObject.FindGameObjectWithTag($"Player");
        ballManager = FindAnyObjectByType<BallManager>();
        transform.localScale = Vector3.one * ScaleMod;
        transform.localPosition = new Vector3(
                    Random.Range(MinVector.x, MaxVector.x),
                    Random.Range(MinVector.y, MaxVector.y),
                    Random.Range(MinVector.z, MaxVector.z));
    }

    void Update()
    {
        // Vector3 dir = (Camera.main.transform.position - transform.position).normalized;
        Vector3 dir = (Character.transform.position - transform.position).normalized;
        transform.Translate(dir* Time.deltaTime * Speed);
        // if (Vector3.Distance(Camera.main.transform.position, transform.position) <= CameraToBallDestroyDistance)
        // if (Vector3.Distance(Character.transform.position, transform.position) <= CameraToBallDestroyDistance)
        // {
        //     ballManager.BallList.Remove(gameObject);
        //     Destroy(gameObject);
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
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
        transform.position = new Vector3(
            Random.Range(MinVector.x, MaxVector.x),
            Random.Range(MinVector.y, MaxVector.y),
            Random.Range(MinVector.z, MaxVector.z));
    }
} 
