using System.Linq;
using UnityEngine;

public class Pistol : Gun
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        int index = 0;
        while (index < MonstersList.Count)
        {
            if (Vector3.Distance(transform.position, MonstersList[index].transform.position) < minimumDistance)
            {
                MonstersList.RemoveAt(index);
            }
            else
            {
                ++index;
            }
        }

        DistanceComparer distanceComparer = new DistanceComparer();
        distanceComparer.setGun(gameObject);
        MonstersList.Sort(distanceComparer);

        Quaternion targetRotation;

        if (MonstersList.Count > 0)
        {
            targetRotation = Quaternion.LookRotation(MonstersList.First().transform.position - transform.position);

            float lerpTime = Time.deltaTime / 1.0f;

            lerpTime += lerpTime * rotateSpeed; // ȸ�� �ӵ� ����

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpTime);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward);

            float lerpTime = Time.deltaTime / 1.0f;

            lerpTime += lerpTime * rotateSpeed; // ȸ�� �ӵ� ����

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpTime);
        }

        // 발사
        // Bullet 생성하면서 위치, 회전, 머테리얼 넣어줌
        if (Input.GetKeyUp(KeyCode.Space) && reloadTime < currentTime && currentGauge > useGauge)
        {
            GameObject newBullet = Instantiate(bullet);

            newBullet.transform.position = transform.position;
            newBullet.transform.rotation = transform.rotation;
            Bullet bulletCom = newBullet.GetComponent<Bullet>();
            bulletCom.setBulletSpeed(bulletSpeed);
            bulletCom.setDamage(damage);
            bulletCom.setGunType(GunController.GunType.Pistol);

            currentTime = 0.0f;

            currentGauge -= useGauge;
        }
    }
}
