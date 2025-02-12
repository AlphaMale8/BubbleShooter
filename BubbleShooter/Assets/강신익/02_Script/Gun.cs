using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum Color
{
    Red, Blue, Black, Green
}

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float rotateSpeed = 2.0f;

    [SerializeField] private Material red;
    [SerializeField] private Material blue;
    [SerializeField] private Material black;
    [SerializeField] private Material green;

    [SerializeField] private GameObject monster;

    private Color color = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 색깔 바꾸기
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            int num = (int)color;

            if (num == (int)Color.Green)
            {
                color = Color.Red;
            }
            else
            {
                ++num;
                color = (Color)num;
            }

            // 임시
            // 현재 컬러 확인용
            switch (color)
            {
                case Color.Red:
                    GetComponentsInChildren<MeshRenderer>()[0].sharedMaterial = red;
                    break;
                case Color.Blue:
                    GetComponentsInChildren<MeshRenderer>()[0].sharedMaterial = blue;
                    break;
                case Color.Black:
                    GetComponentsInChildren<MeshRenderer>()[0].sharedMaterial = black;
                    break;
                case Color.Green:
                    GetComponentsInChildren<MeshRenderer>()[0].sharedMaterial = green;
                    break;
            }
        }

        // 발사
        // Bullet 생성하면서 위치, 회전, 머테리얼 넣어줌
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject newBullet = Instantiate(bullet);

            newBullet.transform.position = transform.position;
            newBullet.transform.rotation = transform.rotation;

            MeshRenderer meshRenderer = newBullet.GetComponentsInChildren<MeshRenderer>()[0];

            switch(color)
            {
                case Color.Red:
                    meshRenderer.sharedMaterial = red;
                    break;
                case Color.Blue:
                    meshRenderer.sharedMaterial = blue;
                    break;
                case Color.Black:
                    meshRenderer.sharedMaterial = black;
                    break;
                case Color.Green:
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


        // 임시
        // 몬스터 방향 조준

        List<GameObject> monsters = GameObject.FindGameObjectsWithTag("Monster").ToList<GameObject>();

        DistanceComparer distanceComparer = new DistanceComparer();
        distanceComparer.setGun(gameObject);
        monsters.Sort(distanceComparer);

        if (monsters.Count > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(monsters.First().transform.position - transform.position);
            transform.rotation = targetRotation;
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
