using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private int damage;
    private GunController.GunType gunType;
    private GameObject monster;
    private float destroyTime = 5f;

    public void setBulletSpeed(float bulletSpeed)
    {
        speed = bulletSpeed;
    }

    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    public void setGunType(GunController.GunType gunType)
    {
        this.gunType = gunType;
    }

    public void setMonster(GameObject monster)
    {
        this.monster = monster;
    }

    public void setDestroyTime(float destroyTime)
    {
        this.destroyTime = destroyTime;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        // 정해진 방향으로 날아가기만 함

        if (monster != null && monster.GetComponent<Ball>().health > 0)
        {
            transform.rotation = Quaternion.LookRotation(monster.transform.position - transform.position);
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            print("fff");
            GameObject ballObject = other.gameObject;
            Ball ball = other.gameObject.GetComponent<Ball>();
            ball.health -= damage;

            if (gunType != GunController.GunType.SniperRifle)
            {
                Destroy(gameObject);
            }
        }
    }
}
