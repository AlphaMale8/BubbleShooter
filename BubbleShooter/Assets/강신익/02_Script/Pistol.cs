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

        // �߻�
        // Bullet �����ϸ鼭 ��ġ, ȸ��, ���׸��� �־���
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject newBullet = Instantiate(bullet);

            newBullet.transform.position = transform.position;
            newBullet.transform.rotation = transform.rotation;
            Bullet bulletCom = newBullet.GetComponent<Bullet>();
            bulletCom.setBulletSpeed(bulletSpeed);
        }
    }
}
