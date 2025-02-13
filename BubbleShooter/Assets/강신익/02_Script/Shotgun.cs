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

        // �߻�
        // Bullet �����ϸ鼭 ��ġ, ȸ��, ���׸��� �־���
        if (Input.GetKeyUp(KeyCode.Space))
        {
            for (int i = 0; i < bulletNum; ++i)
            {
                GameObject newBullet = Instantiate(bullet);

                newBullet.transform.position = transform.position;

                Vector3 angles = transform.rotation.eulerAngles;

                float axisX = Random.Range(-10.0f, 10.0f);
                float axisY = Random.Range(-10.0f, 10.0f);

                angles.x += axisX;
                angles.y += axisY;

                newBullet.transform.rotation = Quaternion.Euler(angles);


                Bullet bulletCom = newBullet.GetComponent<Bullet>();
                bulletCom.setBulletSpeed(bulletSpeed);
            }
        }
    }
}
