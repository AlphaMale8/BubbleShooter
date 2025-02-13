using System.Collections;
using TMPro;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    CanvasGroup canvasGroup;
    TextMeshProUGUI text;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();  

        Transform child = transform.GetChild(0);
        text = child.GetComponent<TextMeshProUGUI>();
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

    public void SetDialogText(DialogDataSO data, float activeTime = 3f)
    {
        StopAllCoroutines();
        EnableCanvas();
        text.text = $"{data.content}";
        StartCoroutine(ClearTextProcess(activeTime));

    }

    private IEnumerator ClearTextProcess(float maxTime)
    {
        float timeElapsed = 0.0f;
        while(timeElapsed < maxTime)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        text.text = "";
        DisalbleCanvas();
    }

    private void DisalbleCanvas()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false; 
    }

    private void EnableCanvas()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }


#if UNITY_EDITOR
    public DialogDataSO[] datas;

    private int index = 0;

    private void testInit()
    {
    }
    private void TestInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetDialogText(datas[index], 1f); // active time = 1f
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SetDialogText(datas[index]); // default param
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            index++;
            index %= datas.Length;
        }
    }
#endif
}