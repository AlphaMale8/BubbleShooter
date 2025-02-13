using System.Linq;
using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] private int bulletNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        // 발사
        // Bullet 생성하면서 위치, 회전, 머테리얼 넣어줌
        if (Input.GetKeyUp(KeyCode.Space))
        {
            float z = 5.0f;

            float x1 = -16.0f;
            float x2 = 0.0f;
            float x3 = 16.0f;

            float y1 = 0.3f;
            float y2 = 7.5f;
            float y3 = 15.0f;

            Vector3[] dest = 
            {
                new Vector3(x1, y1, z),
                new Vector3(x1, y2, z),
                new Vector3(x1, y3, z),
                new Vector3(x2, y1, z),
                new Vector3(x2, y2, z),
                new Vector3(x2, y3, z),
                new Vector3(x3, y1, z),
                new Vector3(x3, y2, z),
                new Vector3(x3, y3, z)
            };

            for (int i = 0; i < dest.Length; ++i)
            {
                GameObject newBullet = Instantiate(bullet);

                newBullet.transform.position = transform.position;

                Quaternion bulletDir = Quaternion.LookRotation(dest[i] - transform.position);

                newBullet.transform.rotation = bulletDir;


                Bullet bulletCom = newBullet.GetComponent<Bullet>();
                bulletCom.setBulletSpeed(bulletSpeed);
            }
        }

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward);

        float lerpTime = Time.deltaTime / 1.0f;

        lerpTime += lerpTime * rotateSpeed; // ȸ�� �ӵ� ����

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpTime);
    }
}
