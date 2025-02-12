using UnityEngine;

public class Ball : MonoBehaviour
{
    // Ball의 초기값
    public Camera MainCamera;
    [SerializeField]
    public float Speed = 5.0f;
    [SerializeField]
    public float ScaleMod = 1.0f;
    [SerializeField]
    public float CameraToBallDestroyDistance = 3.0f;

    [SerializeField] private Vector3 MinVector = new Vector3(-16.0f, 0.3f, 5.0f);
    [SerializeField] private Vector3 MaxVector = new Vector3(16.0f, 0.3f, 1.0f);
    // Ball의 초기 값 설정
    void Start()
    {
        MainCamera = FindAnyObjectByType<Camera>();
        transform.localScale = Vector3.one * ScaleMod;
        transform.localPosition = new Vector3(
                    Random.Range(MinVector.x, MaxVector.x),
                    Random.Range(MinVector.y, MaxVector.y),
                    Random.Range(MinVector.z, MaxVector.z));
    }

    void Update()
    {
        Vector3 dir = (Camera.main.transform.position - transform.position).normalized;
        transform.Translate(dir* Time.deltaTime * Speed);
        if (Vector3.Distance(Camera.main.transform.position, transform.position) <= CameraToBallDestroyDistance)
        {
            Destroy(gameObject);
        }
    }
} 
