using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelUI : MonoBehaviour
{
    /// <summary>
    /// 표시용 타이틀 텍스트
    /// </summary>
    private TextMeshProUGUI titleText;

    /// <summary>
    /// 현재 스테이지 스코어 표시용 텍스트
    /// </summary>
    private TextMeshProUGUI stageScoreText;

    /// <summary>
    /// 최종 스코어 표시용 텍스트
    /// </summary>
    private TextMeshProUGUI totalScoreText;

    /// <summary>
    /// 메인 메뉴로 가기위한 버튼
    /// </summary>
    private Button button;

    private void Awake()
    {       
        Transform child = transform.GetChild(0);
        titleText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(1);
        stageScoreText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        totalScoreText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(3);
        button = child.GetComponent<Button>();
    }

    private void Start()
    {
        //button.onClick.AddListener() -> TODO : 나중에 해당 리스너에 메인메뉴 씬으로 돌아가는 내용 넣기

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

    public void SetTitleText(string str)
    {
        titleText.text = $"{str}";
        titleText.color = Color.white;
    }

    public void SetTitleText(string str, Color color)
    {
        titleText.text = $"{str}";
        titleText.color = color;
    }

    public void SetStageScoreText(int value)
    {
        stageScoreText.text = $"Stage Score : {value}";
    }

    /// <summary>
    /// 비어있는 텍스트 설정 함수
    /// </summary>
    public void SetTotalScoreText()
    {
        totalScoreText.text = "";
    }

    public void SetTotalScoreText(int value)
    {
        totalScoreText.text = $"Total Score : {value}";
    }

#if UNITY_EDITOR

    private void testInit()
    {

    }
    private void TestInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetTitleText("Victory!!!", Color.green);
            SetTotalScoreText(20000);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SetTitleText("Defeat . . .", Color.red);
            SetTotalScoreText(0);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SetTitleText("NormalColorTitle");
            SetTotalScoreText(12312);
        }
    }
#endif
}
