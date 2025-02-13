using System;
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
    [SerializeField] protected int damage;
    [SerializeField] protected float maxGauge;
    protected float currentGauge;
    [SerializeField] protected float gaugeSpeed;
    [SerializeField] protected float useGauge;

    public List<GameObject> MonstersList
    {
        get => monstersList;
        private set => monstersList = value;
    }

    public float getGauge()
    {
        return currentGauge / maxGauge;
    }


    public float getMaxGauge()
    {
        return this.maxGauge;
    }

    public float getCurrentGauge()
    {
        return this.currentGauge;
    }

    private void Start()
    {
        currentGauge = maxGauge;
    }

    // Update is called once per frame
    protected void Update()
    {
        print("gauge: " + currentGauge + "/" + maxGauge);
        MonstersList = GameObject.FindGameObjectsWithTag("Monster").ToList<GameObject>();
    }
    public void UpdateTime()
    {
        if (maxGauge > currentGauge)
        {
            currentGauge += gaugeSpeed * Time.deltaTime;
        }
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
