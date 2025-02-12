using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum BulletColor
{
    Red, Blue, Black, Green
}

public class Gun : MonoBehaviour
{
    public BallManager ballManager;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float rotateSpeed = 2.0f;

    [SerializeField] private Material red;
    [SerializeField] private Material blue;
    [SerializeField] private Material black;
    [SerializeField] private Material green;

    [SerializeField] private GameObject monster;

    private BulletColor color = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        ballManager = FindAnyObjectByType<BallManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �ٲٱ�
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            int num = (int)color;

            if (num == (int)BulletColor.Green)
            {
                color = BulletColor.Red;
            }
            else
            {
                ++num;
                color = (BulletColor)num;
            }

            // �ӽ�
            // ���� �÷� Ȯ�ο�
            switch (color)
            {
                case BulletColor.Red:
                    GetComponentsInChildren<MeshRenderer>()[0].sharedMaterial = red;
                    break;
                case BulletColor.Blue:
                    GetComponentsInChildren<MeshRenderer>()[0].sharedMaterial = blue;
                    break;
                case BulletColor.Black:
                    GetComponentsInChildren<MeshRenderer>()[0].sharedMaterial = black;
                    break;
                case BulletColor.Green:
                    GetComponentsInChildren<MeshRenderer>()[0].sharedMaterial = green;
                    break;
            }
        }

        // �߻�
        // Bullet �����ϸ鼭 ��ġ, ȸ��, ���׸��� �־���
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject newBullet = Instantiate(bullet);

            newBullet.transform.position = transform.position;
            newBullet.transform.rotation = transform.rotation;

            MeshRenderer meshRenderer = newBullet.GetComponentsInChildren<MeshRenderer>()[0];

            switch(color)
            {
                case BulletColor.Red:
                    meshRenderer.sharedMaterial = red;
                    break;
                case BulletColor.Blue:
                    meshRenderer.sharedMaterial = blue;
                    break;
                case BulletColor.Black:
                    meshRenderer.sharedMaterial = black;
                    break;
                case BulletColor.Green:
                    meshRenderer.sharedMaterial = green;
                    break;
            }
        }

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Rotate(new Vector3(0.0f, 90.0f * rotateSpeed * Time.deltaTime, 0.0f));
        //}

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Rotate(new Vector3(0.0f, -90.0f * rotateSpeed * Time.deltaTime, 0.0f));
        //}

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.Rotate(new Vector3(-90.0f * rotateSpeed * Time.deltaTime, 0.0f, 0.0f));
        //}

        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.Rotate(new Vector3(90.0f * rotateSpeed * Time.deltaTime, 0.0f, 0.0f));
        //}


        // �ӽ�
        // ���� ���� ����
        DistanceComparer distanceComparer = new DistanceComparer();
        distanceComparer.setGun(gameObject);
        ballManager.Instance.ballList.Sort(distanceComparer);

        if (ballManager.Instance.ballList.Count > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(ballManager.Instance.ballList.First().transform.position - transform.position);

            float lerpTime = Time.deltaTime / 1.0f;

            lerpTime += lerpTime * rotateSpeed; // ȸ�� �ӵ� ����

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpTime);
        }

        // Quaternion targetRotation = Quaternion.LookRotation(monster.transform.position - transform.position);

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
