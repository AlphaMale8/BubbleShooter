using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class GunController : MonoBehaviour
{
    public enum GunType
    {
        Pistol, SniperRifle, Shotgun, MAX
    }

    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject sniperRifle;
    [SerializeField] private GameObject shotgun;

    private List<GameObject> guns = new List<GameObject>();

    private GunType gunType = GunType.Pistol;
    
    /// <summary>
    /// 현재 총 타입 접근용 프로퍼티
    /// </summary>
    public GunType CurrnetGunType 
    { 
        get => gunType; 
        set
        {
            gunType = value;
            OnGunChange?.Invoke((int)gunType);
        }
    }

    public Action<int> OnGunChange;

    public GameObject GetTarget()
    {
        List<GameObject> monsterList = guns[(int)CurrnetGunType].GetComponent<Gun>().MonstersList;
        if (monsterList.Count > 0)
        {
            return monsterList[0];
        }

        return null;
    }

    public float GetGauge()
    {
        return guns[(int)CurrnetGunType].GetComponent<Gun>().getGauge();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        guns.Add(Instantiate(pistol, this.transform));
        guns.Add(Instantiate(sniperRifle, this.transform));
        guns.Add(Instantiate(shotgun, this.transform));

        for (int i = 0; i < (int)GunType.MAX; ++i)
        {
            guns[i].transform.localPosition = Vector3.zero;
            guns[i].SetActive(false);
        }

        guns[(int)GunType.Pistol].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < (int)GunType.MAX; ++i)
        {
            guns[i].GetComponent<Gun>().UpdateTime();
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            int num = (int)CurrnetGunType;

            print(num);

            Transform prevTransform = guns[num].transform;

            if (num == (int)GunType.Shotgun)
            {
                CurrnetGunType = GunType.Pistol;
            }
            else
            {
                ++num;
                CurrnetGunType = (GunType)num;
            }

            switch (CurrnetGunType)
            {
                case GunType.Pistol:
                    guns[(int)GunType.Pistol].SetActive(true);
                    guns[(int)GunType.Pistol].transform.rotation = prevTransform.rotation;
                    guns[(int)GunType.Shotgun].SetActive(false);
                    break;
                case GunType.SniperRifle:
                    guns[(int)GunType.Pistol].SetActive(false);
                    guns[(int)GunType.SniperRifle].SetActive(true);
                    guns[(int)GunType.SniperRifle].transform.rotation = prevTransform.rotation;
                    break;
                case GunType.Shotgun:
                    guns[(int)GunType.SniperRifle].SetActive(false);
                    guns[(int)GunType.Shotgun].SetActive(true);
                    guns[(int)GunType.Shotgun].transform.rotation = prevTransform.rotation;
                    break;
            }
        }
    }
}
