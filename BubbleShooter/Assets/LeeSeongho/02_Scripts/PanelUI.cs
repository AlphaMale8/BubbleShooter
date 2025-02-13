using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelUI : MonoBehaviour
{
    /// <summary>
    /// �̹��� ������ ������ �迭
    /// </summary>
    public Sprite[] imageDatas;

    /// <summary>
    /// �г� �̹���
    /// </summary>
    private Image panelImage;

    /// <summary>
    /// ǥ�ÿ� Ÿ��Ʋ �ؽ�Ʈ
    /// </summary>
    private TextMeshProUGUI titleText;

    /// <summary>
    /// ���� �������� ���ھ� ǥ�ÿ� �ؽ�Ʈ
    /// </summary>
    private TextMeshProUGUI stageScoreText;

    /// <summary>
    /// ���� ���ھ� ǥ�ÿ� �ؽ�Ʈ
    /// </summary>
    private TextMeshProUGUI totalScoreText;

    /// <summary>
    /// ���� �޴��� �������� ��ư
    /// </summary>
    private Button button;

    /// <summary>
    /// ��ư �ؽ�Ʈ
    /// </summary>
    private TextMeshProUGUI buttonText;

    private void Awake()
    {
        panelImage = GetComponent<Image>(); 

        Transform child = transform.GetChild(0);
        titleText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(1);
        stageScoreText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        totalScoreText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(3);
        button = child.GetComponent<Button>();

        buttonText = child.GetChild(0).GetComponent<TextMeshProUGUI>(); 
    }

    private void Start()
    {
        //button.onClick.AddListener() -> TODO : ���߿� �ش� �����ʿ� ���θ޴� ������ ���ư��� ���� �ֱ�

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
    /// ����ִ� �ؽ�Ʈ ���� �Լ�
    /// </summary>
    public void SetTotalScoreText()
    {
        totalScoreText.text = "";
    }

    public void SetTotalScoreText(int value)
    {
        totalScoreText.text = $"Total Score : {value}";
    }

    /// <summary>
    /// ��ư �ؽ�Ʈ �⺻ ���� �Լ� (Exit)
    /// </summary>
    public void SetButtonText()
    {
        buttonText.text = $"Exit";
    }

    /// <summary>
    /// ��ư �ؽ�Ʈ �����Լ�
    /// </summary>
    /// <param name="str">��� ����</param>
    public void SetButtonText(string str)
    {
        buttonText.text = $"{str}";
    }

    /// <summary>
    /// �� ��ũ��Ʈ ������ ����� �����Ϳ��� �̹��� �����ϱ�
    /// </summary>
    public void SetPanelImage(int index)
    {
        panelImage.sprite = imageDatas[index];
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
