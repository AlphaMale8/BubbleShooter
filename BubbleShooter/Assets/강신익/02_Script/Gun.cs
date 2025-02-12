using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public BallManager ballManager;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float rotateSpeed = 2.0f;

    [SerializeField] private GameObject monster;
    [SerializeField] private List<GameObject> monstersList;
    public List<GameObject> MonstersList
    {
        get => monstersList;
        private set => monstersList = value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        ballManager = FindAnyObjectByType<BallManager>();
    }

    // Update is called once per frame
    protected void Update()
    {
        // 발사
        // Bullet 생성하면서 위치, 회전, 머테리얼 넣어줌
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject newBullet = Instantiate(bullet);

            newBullet.transform.position = transform.position;
            newBullet.transform.rotation = transform.rotation;
        }
        // 몬스터 방향 조준
        MonstersList = GameObject.FindGameObjectsWithTag("Monster").ToList<GameObject>();

        DistanceComparer distanceComparer = new DistanceComparer();
        distanceComparer.setGun(gameObject);
        MonstersList.Sort(distanceComparer);

        if (MonstersList.Count > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(MonstersList.First().transform.position - transform.position);

            float lerpTime = Time.deltaTime / 1.0f;

            lerpTime += lerpTime * rotateSpeed; // 회전 속도 조절

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpTime);
        }

    }

    class DistanceComparer : Comparer<GameObject>
    {
        GameObject gun;
        public void setGun(GameObject gun)
        {
            this.gun = gun;
        }
        public override int Compare(GameObject x, GameObject y)
        {
            return Vector3.Distance(gun.transform.position, x.transform.position).CompareTo(Vector3.Distance(gun.transform.position, y.transform.position));
        }
    }
}
