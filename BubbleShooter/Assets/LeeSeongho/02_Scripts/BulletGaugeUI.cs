using UnityEngine;

public class BulletGaugeUI : HpGaugeUI
{
    private GunController gc;

    private void Awake()
    {
        gc = FindAnyObjectByType<GunController>();
    }

    private void Update()
    {
        if (gc == null) return;

        SetSliderValue(gc.GetGauge());
    }

#if UNITY_EDITOR
    float maxBullet = 30;

    float currnetBullet = 0;
    float CurrentBullet
    {
        get => currnetBullet;
        set
        {
            currnetBullet = Mathf.Clamp(value, 0f, maxBullet);
        }
    }

    protected override void testInit()
    {
        CurrentBullet = maxBullet;
    }

    float recoverAmount = 2;
    float timer = 0;
    float maxTimer = 2f;
    protected override void TestInput()
    {
        Debug.Log(CurrentBullet);
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("총알 게이지 감소");
            CurrentBullet -= 2f;
            SetSliderValue(CurrentBullet / maxBullet);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("총알 채우기");
            CurrentBullet = maxBullet;
            SetSliderValue(CurrentBullet / maxBullet);
        }

        //if(timer > maxTimer) // 총알 게이지 회복
        //{
        //    timer = 0;
        //    CurrentBullet += recoverAmount;
        //    SetSliderValue(CurrentBullet / maxBullet);
        //    Debug.Log("총알 게이지 증가");
        //}
    }
#endif
}
