using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public BallManager ballManager;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float rotateSpeed = 2.0f;
    [SerializeField] protected float bulletSpeed;

    [SerializeField] private GameObject monster;
    [SerializeField] protected List<GameObject> monstersList;
    [SerializeField] protected float minimumDistance;

    // private int bulletCount = 30;
    
    public List<GameObject> MonstersList
    {
        get => monstersList;
        private set => monstersList = value;
    }

    // Update is called once per frame
    protected void Update()
    {
        MonstersList = GameObject.FindGameObjectsWithTag("Monster").ToList<GameObject>();
    }

    protected class DistanceComparer : Comparer<GameObject>
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
