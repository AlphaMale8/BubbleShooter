using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float rotateSpeed = 2.0f;

    [SerializeField] private GameObject monster;
    [SerializeField] private List<GameObject> monstersList;

    // private int bulletCount = 30;
    
    public List<GameObject> MonstersList
    {
        get => monstersList;
        private set => monstersList = value;
    }

    // Update is called once per frame
    protected void Update()
    {
        // �߻�
        // Bullet �����ϸ鼭 ��ġ, ȸ��, ���׸��� �־���
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // if (bulletCount > 0)
            // {
                GameObject newBullet = Instantiate(bullet);

                newBullet.transform.position = transform.position;
                newBullet.transform.rotation = transform.rotation;
            // }
        }
        // ���� ���� ����

        MonstersList = GameObject.FindGameObjectsWithTag("Monster").ToList<GameObject>();

        DistanceComparer distanceComparer = new DistanceComparer();
        distanceComparer.setGun(gameObject);
        MonstersList.Sort(distanceComparer);

        if (MonstersList.Count > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(MonstersList.First().transform.position - transform.position);

            float lerpTime = Time.deltaTime / 1.0f;

            lerpTime += lerpTime * rotateSpeed; // ȸ�� �ӵ� ����

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
