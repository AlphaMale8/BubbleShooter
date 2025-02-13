using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    /// <summary>
    /// 플레이어의 현재 총의 최대 총알량 저장용 변수 
    /// </summary>
    private int playerMaxBullet;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
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

    /// <summary>
    /// 총알 표시 텍스트 초기화 함수 ( 최대 탄약량 변수 설정용 )
    /// </summary>
    /// <param name="maxBullet">최대 탄약</param>
    public void Init(int maxBullet)
    {
        playerMaxBullet = maxBullet;
    }

    /// <summary>
    /// 현재 탄약 텍스트 설정 함수
    /// </summary>
    /// <param name="currentBullet">플레이어 현재 탄약 수</param>
    public void SetBulletText(int currentBullet)
    {
        text.text = $"{currentBullet} / {playerMaxBullet}";
    }

#if UNITY_EDITOR

    int currentBullet = 0;

    private void testInit()
    {
        playerMaxBullet = 30;
        currentBullet = playerMaxBullet;
        SetBulletText(currentBullet);
    }
    private void TestInput()
    {
        // if (Input.GetKeyDown(KeyCode.Q))
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentBullet == 0)
            {
                currentBullet = playerMaxBullet;
                SetBulletText(currentBullet);
                return;
            }
            currentBullet--;
            SetBulletText(currentBullet);
        }
    }
#endif
}
