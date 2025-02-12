using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        // 정해진 방향으로 날아가기만 함
        transform.Translate(Vector3.forward *speed * Time.deltaTime);
    }
}
