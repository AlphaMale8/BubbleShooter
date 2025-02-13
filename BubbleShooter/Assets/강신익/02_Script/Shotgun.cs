using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] private int bulletNum;

    void Update()
    {
        base.Update();

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward);

        float lerpTime = Time.deltaTime / 1.0f;

        lerpTime += lerpTime * rotateSpeed; // ȸ�� �ӵ� ����

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpTime);

        // 발사
        // Bullet 생성하면서 위치, 회전, 머테리얼 넣어줌
        if (Input.GetKeyUp(KeyCode.Space) && currentGauge >= useGauge)
        {
             List<Vector3> positions = GameObject.Find("BallManager").GetComponent<BallManager>().data.SpawnPosition;

            for (int i = 0; i < positions.Count; ++i)
            {
                GameObject newBullet = Instantiate(bullet);

                newBullet.transform.position = transform.position;

                Quaternion bulletDir = Quaternion.LookRotation(positions[i] - transform.position);

                newBullet.transform.rotation = bulletDir;


                Bullet bulletCom = newBullet.GetComponent<Bullet>();
                bulletCom.setBulletSpeed(bulletSpeed);
                bulletCom.setDamage(damage);
                bulletCom.setGunType(GunController.GunType.Shotgun);
            }

            currentGauge -= useGauge;
        }
    }
}
