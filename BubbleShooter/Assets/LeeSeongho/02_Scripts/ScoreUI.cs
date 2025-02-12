using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    /// <summary>
    /// 출력하는 스코어 값 저장용 변수
    /// </summary>
    int currentScore = 0;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
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
    /// 스코어 UI 설정 함수
    /// </summary>
    /// <param name="value">현재 스코어</param>
    public void SetScoreText(int value)
    {
        currentScore = value;
        scoreText.text = $"Score : {value}";
    }

#if UNITY_EDITOR

    private void testInit()
    {
        SetScoreText(0);
    }
    private void TestInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("스코어 증가");
            currentScore += 200;
            SetScoreText(currentScore);
        }
    }
#endif
}
