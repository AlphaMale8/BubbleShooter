using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    public void setBulletSpeed(float bulletSpeed)
    {
        speed = bulletSpeed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        // 정해진 방향으로 날아가기만 함
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            print("fff");
            GameObject ballObject = other.gameObject;
            Ball ball = other.gameObject.GetComponent<Ball>();
            ball.health -= 3;

            Destroy(gameObject);
        }
    }
}
