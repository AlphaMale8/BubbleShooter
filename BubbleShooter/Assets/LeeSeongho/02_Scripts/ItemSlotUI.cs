using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    Image itemImage;

    private void Awake()
    {
        itemImage = GetComponent<Image>();  
    }

    private void Start()
    {
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

    public void SetItemSlotImage(Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
    }

#if UNITY_EDITOR

    [Tooltip("아이템 테스트 이미지")]
    public Sprite[] testImg;

    private void testInit()
    {
        SetItemSlotImage(null);
    }

    private void TestInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetItemSlotImage(testImg[0]);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SetItemSlotImage(testImg[1]);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SetItemSlotImage(null);
        }
    }
#endif
}
