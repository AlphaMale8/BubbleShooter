using System;
using UnityEngine;
using UnityEngine.UI;
using static GunController;

public class ItemSlotUI : MonoBehaviour
{
    public Sprite[] imageDatas;

    Image itemImage;
    GunController gc;

    private void Awake()
    {
        itemImage = GetComponent<Image>();  
        gc = FindAnyObjectByType<GunController>();
    }

    private void Start()
    {
        gc.OnGunChange += (index) => 
        {
            SetItemSlotImage(index);
        };

#if UNITY_EDITOR
        testInit();
#endif
    }

    private void Update()
    {
#if UNITY_EDITOR
        TestInput();
#endif
    }

    public void SetItemSlotImage(int index)
    {
        itemImage.sprite = imageDatas[index];
    }

#if UNITY_EDITOR

    [Tooltip("아이템 테스트 이미지")]

    private void testInit()
    {
        SetItemSlotImage(0);
    }

    private void TestInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetItemSlotImage(0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SetItemSlotImage(1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SetItemSlotImage(0);
        }
    }
#endif
}
