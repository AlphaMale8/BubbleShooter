using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HpGaugeUI : MonoBehaviour
{
    Slider hpSlider;

    private void Awake()
    {
        hpSlider = GetComponent<Slider>();
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
    /// value 값 0 ~ 1사이의 체력바 UI 함수
    /// </summary>
    /// <param name="value">0 ~ 1 사이의 float 값</param>
    public void SetSliderValue(float value)
    {
        StopAllCoroutines();
        StartCoroutine(ValueReduceProcess(value));
    }

    /// <summary>
    /// 플레이어 체력 변화 보간용 코루틴 ( 초당 체력이 줄어듬 )
    /// </summary>
    /// <param name="goalValue">도달할 체력</param>
    private IEnumerator ValueReduceProcess(float goalValue)
    {
        float timeElapsed = 0.0f;

        while(timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime;

            hpSlider.value = Mathf.Lerp(hpSlider.value, goalValue, timeElapsed);
            yield return null;
        }

        hpSlider.value = goalValue;
    }

#if UNITY_EDITOR

    float maxHealth = 1f;
    float currentHealth = 1f;
    float CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            Mathf.Clamp(currentHealth, 0f, 1f);
        }
    }

    protected virtual void testInit()
    {
        SetSliderValue(1);
        maxHealth = 1f;
        currentHealth = maxHealth;
    }
    protected virtual void TestInput()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("체력 감소");
            CurrentHealth -= 0.2f;
            SetSliderValue(CurrentHealth);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("체력 증가");
            CurrentHealth += 0.2f;
            SetSliderValue(CurrentHealth);
        }
    }
#endif
}
