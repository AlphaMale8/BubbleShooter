using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    /// <summary>
    /// ����ϴ� ���ھ� �� ����� ����
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
    /// ���ھ� UI ���� �Լ�
    /// </summary>
    /// <param name="value">���� ���ھ�</param>
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
            Debug.Log("���ھ� ����");
            currentScore += 200;
            SetScoreText(currentScore);
        }
    }
#endif
}
