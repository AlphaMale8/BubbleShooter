using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    enum GunType
    {
        AssaultRifle, SniperRifle, Shotgun, MAX
    }

    [SerializeField] private GameObject assaultRifle;
    [SerializeField] private GameObject sniperRifle;
    [SerializeField] private GameObject shotgun;

    private List<GameObject> guns = new List<GameObject>();

    private GunType gunType = GunType.AssaultRifle;

    public testimie[] asdf;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        guns.Add(Instantiate(assaultRifle, this.transform));
        guns.Add(Instantiate(sniperRifle, this.transform));
        guns.Add(Instantiate(shotgun, this.transform));

        foreach (var g in guns)
        {
            g.transform.localPosition = Vector3.zero;
        }

        guns[(int)GunType.SniperRifle].SetActive(false);
        guns[(int)GunType.Shotgun].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            int num = (int)gunType;

            Transform prevTransform = guns[num].transform;

            if (num == (int)GunType.Shotgun)
            {
                gunType = GunType.AssaultRifle;
            }
            else
            {
                ++num;
                gunType = (GunType)num;
            }

            switch (gunType)
            {
                case GunType.AssaultRifle:
                    guns[(int)GunType.AssaultRifle].SetActive(true);
                    guns[(int)GunType.AssaultRifle].transform.rotation = prevTransform.rotation;
                    guns[(int)GunType.Shotgun].SetActive(false);
                    break;
                case GunType.SniperRifle:
                    guns[(int)GunType.AssaultRifle].SetActive(false);
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
